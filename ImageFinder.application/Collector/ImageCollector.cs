using ImageFinder.domain.Models;
using System.Globalization;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace ImageFinder.application.Collector;
public class ImageCollector : IImageCollector
{
    public async Task CollectAndOrganiseImagesAsync(ImageDirectoryModel imageDirectoryModel)
    {
        foreach (string filePath in System.IO.Directory.GetFiles(imageDirectoryModel.SourceDirectoryPath, "*.*", SearchOption.AllDirectories))
        {
            if (!Array.Exists(ImageModel.SupportedExtensions, ext => filePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                continue;

            try
            {
                IReadOnlyList<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(filePath);

                if (!TryGetExifDateTaken(directories, out DateTime dateTaken))
                {
                    Console.WriteLine($"No Date Taken found for {filePath}. Skipping.");
                    continue;
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

                File.Copy(filePath, destFilePath);

                //TODO: Log this message instead of writing to console
                Console.WriteLine($"Copied: {filePath} → {destFilePath}");
            }
            catch (Exception ex)
            {
                //TODO: Log this message instead of writing to console
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
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