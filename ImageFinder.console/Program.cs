using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.Versioning;

class ImageSorter
{
    [SupportedOSPlatform("windows6.1")]
    static void Main()
    {
        Console.WriteLine("Enter the source directory containing images to sort (e.g., C:\\Users\\YourName\\Pictures):");
        string? sourceDir = Console.ReadLine();
        Console.WriteLine("Enter the target directory where sorted images will be placed (e.g., C:\\Users\\YourName\\SortedPictures):");
        string? targetDir = Console.ReadLine();

        string[] supportedExtensions = [".jpg", ".jpeg", ".png"];

        ArgumentException.ThrowIfNullOrEmpty(sourceDir, nameof(sourceDir));
        ArgumentException.ThrowIfNullOrEmpty(targetDir, nameof(targetDir));
        ArgumentNullException.ThrowIfNull(supportedExtensions, nameof(supportedExtensions));

        foreach (string filePath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
        {
            if (!Array.Exists(supportedExtensions, ext => filePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
                continue;

            try
            {
                DateTime dateTaken;

                using System.Drawing.Image image = System.Drawing.Image.FromFile(filePath);

                const int dateTakenPropertyId = 0x9003;

                if (image.PropertyIdList.Contains(dateTakenPropertyId))
                {
                    PropertyItem? propItem = image.GetPropertyItem(dateTakenPropertyId);

                    ArgumentNullException.ThrowIfNull(propItem?.Value);

                    string dateTakenStr = System.Text.Encoding.ASCII.GetString(propItem.Value).Trim('\0');

                    if (!DateTime.TryParseExact(dateTakenStr, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTaken))
                    {
                        Console.WriteLine($"Could not parse date for {filePath}. Skipping.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine($"No Date Taken found for {filePath}. Skipping.");
                    continue;
                }

                string year = dateTaken.Year.ToString();
                string month = dateTaken.ToString("MMMM", CultureInfo.InvariantCulture);

                string destDir = Path.Combine(targetDir, year, month);
                Directory.CreateDirectory(destDir);

                string destFilePath = Path.Combine(destDir, Path.GetFileName(filePath));

                // Avoid overwriting  
                if (File.Exists(destFilePath))
                {
                    string uniqueName = string.Concat(Path.GetFileNameWithoutExtension(filePath), "_", Guid.NewGuid().ToString().AsSpan(0, 8), Path.GetExtension(filePath));
                    destFilePath = Path.Combine(destDir, uniqueName);
                }

                File.Copy(filePath, destFilePath);

                Console.WriteLine($"Copied: {filePath} → {destFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        }

        Console.WriteLine("Sorting complete!");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
