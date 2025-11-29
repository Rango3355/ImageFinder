using ImageFinder.domain.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace ImageFinder.application.Collector;
public class ImageCollector
{
    [Obsolete("This method uses System.Drawing which is not cross-platform. Consider using a cross-platform library for image processing.", true)]
    public static void CollectAndOrganizeImages(ImageDirectoryModel imageDirectoryModel)
    {

       
        //foreach (string filePath in Directory.GetFiles(imageDirectoryModel.SourceDirectoryPath, "*.*", SearchOption.AllDirectories))
        //{
        //    if (!Array.Exists(ImageModel.SupportedExtensions, ext => filePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
        //        continue;

        //    try
        //    {
        //        DateTime dateTaken;

        //        using Image image = Image.FromFile(filePath);

        //        const int dateTakenPropertyId = 0x9003;

        //        if (image.PropertyIdList.Contains(dateTakenPropertyId))
        //        {
        //            PropertyItem? propItem = image.GetPropertyItem(dateTakenPropertyId);

        //            ArgumentNullException.ThrowIfNull(propItem?.Value);

        //            string dateTakenStr = System.Text.Encoding.ASCII.GetString(propItem.Value).Trim('\0');

        //            if (!DateTime.TryParseExact(dateTakenStr, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTaken))
        //            {
        //                //TODO: Log this message instead of writing to console
        //                //TODO: Move to a unsorted directory
        //                Console.WriteLine($"Could not parse date for {filePath}. Skipping.");
        //                continue;
        //            }
        //        }
        //        else
        //        {
        //            //TODO: Log this message instead of writing to console
        //            //TODO: Move to a unsorted directory
        //            Console.WriteLine($"No Date Taken found for {filePath}. Skipping.");
        //            continue;
        //        }

        //        string year = dateTaken.Year.ToString();
        //        string month = dateTaken.ToString("MMMM", CultureInfo.InvariantCulture);

        //        string destDir = Path.Combine(imageDirectoryModel.DestinationDirectoryPath, year, month);
        //        Directory.CreateDirectory(destDir);

        //        string destFilePath = Path.Combine(destDir, Path.GetFileName(filePath));

        //        if (File.Exists(destFilePath))
        //        {
        //            string uniqueName = string.Concat(Path.GetFileNameWithoutExtension(filePath), "_", Guid.NewGuid().ToString().AsSpan(0, 8), Path.GetExtension(filePath));
        //            destFilePath = Path.Combine(destDir, uniqueName);
        //        }

        //        File.Copy(filePath, destFilePath);

        //        //TODO: Log this message instead of writing to console
        //        Console.WriteLine($"Copied: {filePath} → {destFilePath}");
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: Log this message instead of writing to console
        //        Console.WriteLine($"Error processing {filePath}: {ex.Message}");
        //    }
        //}
    }
}