using ImageFinder.domain.Models;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System.Globalization;

namespace ImageFinder.application.Collector;

public class ImageCollector : IImageCollector
{
    public async Task CollectAndOrganiseImagesAsync(ImageDirectoryModel imageDirectoryModel)
    {
        ArgumentNullException.ThrowIfNull(imageDirectoryModel);
        ArgumentException.ThrowIfNullOrEmpty(imageDirectoryModel.SourceDirectoryPath);
        ArgumentException.ThrowIfNullOrEmpty(imageDirectoryModel.DestinationDirectoryPath);

        IEnumerable<string> files = System.IO.Directory.EnumerateFiles(imageDirectoryModel.SourceDirectoryPath, "*.*", SearchOption.AllDirectories);

        int maxConcurrency = Math.Max(1, Environment.ProcessorCount * 2); // IO-bound: allow a bit more concurrency
        using SemaphoreSlim semaphore = new(maxConcurrency);

        List<Task> workers = [];

        foreach (string filePath in files)
        {
            // quick filter before queuing work
            if (!Array.Exists(ImageModel.SupportedExtensions, ext => filePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                continue;

            workers.Add(ProcessFileAsync(filePath, imageDirectoryModel, semaphore));
        }

        await Task.WhenAll(workers).ConfigureAwait(false);
    }

    private async Task ProcessFileAsync(string filePath, ImageDirectoryModel imageDirectoryModel, SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            IReadOnlyList<MetadataExtractor.Directory> directories = await Task.Run(() => ImageMetadataReader.ReadMetadata(filePath)).ConfigureAwait(false);

            if (!TryGetExifDateTaken(directories, out DateTime dateTaken))
            {
                Console.WriteLine($"No Date Taken found for {filePath}. Skipping.");
                return;
            }

            string year = dateTaken.Year.ToString();
            string month = dateTaken.ToString("MMMM", CultureInfo.InvariantCulture);

            string destDir = Path.Combine(imageDirectoryModel.DestinationDirectoryPath, year, month);
            System.IO.Directory.CreateDirectory(destDir);

            string destFilePath = Path.Combine(destDir, Path.GetFileName(filePath));
            if (File.Exists(destFilePath))
            {
                string uniqueName = string.Concat(Path.GetFileNameWithoutExtension(filePath), "_", Guid.NewGuid().ToString().AsSpan(0, 8), Path.GetExtension(filePath));
                destFilePath = Path.Combine(destDir, uniqueName);
            }

            const int bufferSize = 81920;
            await using FileStream sourceStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.Asynchronous);
            await using FileStream destinationStream = new(destFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize, FileOptions.Asynchronous);
            await sourceStream.CopyToAsync(destinationStream).ConfigureAwait(false);

            Console.WriteLine($"Copied: {filePath} → {destFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing {filePath}: {ex.Message}");
        }
        finally
        {
            semaphore.Release();
        }
    }

    private static bool TryGetExifDateTaken(IReadOnlyList<MetadataExtractor.Directory> directories, out DateTime dateTaken)
    {
        dateTaken = default;

        ExifSubIfdDirectory? subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        if (subIfdDirectory != null && subIfdDirectory.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out dateTaken))
            return true;

        ExifIfd0Directory? ifd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
        if (ifd0 != null && ifd0.TryGetDateTime(ExifDirectoryBase.TagDateTime, out dateTaken))
            return true;

        return false;
    }
}