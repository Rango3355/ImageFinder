using ImageFinder.domain.Models;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System.Globalization;

namespace ImageFinder.application.Collector;

public class ImageCollector : IImageCollector
{
    private const int FileCopyBufferSize = 81920;

    public async Task CollectAndOrganiseImagesAsync(
        ImageDirectoryModel imageDirectoryModel,
        IProgress<double>? progress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(imageDirectoryModel);
        ArgumentException.ThrowIfNullOrEmpty(imageDirectoryModel.SourceDirectoryPath);
        ArgumentException.ThrowIfNullOrEmpty(imageDirectoryModel.DestinationDirectoryPath);

        List<string> files = GetSupportedImageFiles(imageDirectoryModel.SourceDirectoryPath).ToList();

        int totalFiles = files.Count;
        if (totalFiles == 0)
        {
            progress?.Report(1.0);
            return;
        }

        int completed = 0;
        int maxConcurrency = Math.Max(1, Environment.ProcessorCount * 2); // IO-bound: allow a bit more concurrency
        using SemaphoreSlim semaphore = new(maxConcurrency);

        List<Task> workers = [];
        foreach (string filePath in files)
        {
            workers.Add(ProcessFileAsync(
                filePath,
                imageDirectoryModel,
                semaphore,
                onCompleted: () =>
                {
                    int done = Interlocked.Increment(ref completed);
                    double value = (double)done / totalFiles;
                    progress?.Report(value);
                },
                cancellationToken));
        }

        await Task.WhenAll(workers).ConfigureAwait(false);
    }

    private async Task ProcessFileAsync(
        string filePath,
        ImageDirectoryModel imageDirectoryModel,
        SemaphoreSlim semaphore,
        Action onCompleted,
        CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            IReadOnlyList<MetadataExtractor.Directory> directories =
                await Task.Run(() => ImageMetadataReader.ReadMetadata(filePath), cancellationToken)
                    .ConfigureAwait(false);

            if (!TryGetExifDateTaken(directories, out DateTime dateTaken))
            {
                Console.WriteLine($"No Date Taken found for {filePath}. Skipping.");
                return;
            }

            string year = dateTaken.Year.ToString();
            string month = dateTaken.ToString("MMMM", CultureInfo.InvariantCulture);

            string destinationDirectory = Path.Combine(imageDirectoryModel.DestinationDirectoryPath, year, month);
            Directory.CreateDirectory(destinationDirectory);

            string destinationFilePath = BuildDestinationFilePath(filePath, destinationDirectory);

            await using FileStream sourceStream = new(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                FileCopyBufferSize,
                FileOptions.Asynchronous);

            await using FileStream destinationStream = new(
                destinationFilePath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                FileCopyBufferSize,
                FileOptions.Asynchronous);

            await sourceStream.CopyToAsync(destinationStream, cancellationToken)
                .ConfigureAwait(false);

            Console.WriteLine($"Copied: {filePath} -> {destinationFilePath}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation cancelled.");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing {filePath}: {ex.Message}");
        }
        finally
        {
            onCompleted();
            semaphore.Release();
        }
    }

    private static IEnumerable<string> GetSupportedImageFiles(string sourceDirectoryPath)
    {
        return Directory.EnumerateFiles(sourceDirectoryPath, "*.*", SearchOption.AllDirectories)
            .Where(filePath =>
                Array.Exists(
                    ImageModel.SupportedExtensions,
                    extension => filePath.EndsWith(extension, StringComparison.OrdinalIgnoreCase)));
    }

    private static string BuildDestinationFilePath(string sourceFilePath, string destinationDirectory)
    {
        string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(sourceFilePath));
        if (!File.Exists(destinationFilePath))
        {
            return destinationFilePath;
        }

        string uniqueName = string.Concat(
            Path.GetFileNameWithoutExtension(sourceFilePath),
            "_",
            Guid.NewGuid().ToString().AsSpan(0, 8),
            Path.GetExtension(sourceFilePath));

        return Path.Combine(destinationDirectory, uniqueName);
    }

    private static bool TryGetExifDateTaken(IReadOnlyList<MetadataExtractor.Directory> directories, out DateTime dateTaken)
    {
        dateTaken = default;

        ExifSubIfdDirectory? subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
        if (subIfdDirectory is not null &&
            subIfdDirectory.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out dateTaken))
            return true;

        ExifIfd0Directory? ifd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
        if (ifd0 is not null && ifd0.TryGetDateTime(ExifDirectoryBase.TagDateTime, out dateTaken))
            return true;

        return false;
    }
}
