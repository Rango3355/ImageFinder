namespace ImageFinder.app.Models;

public class ImageDirectoryModel
{
    public string DirectoryPath { get; set; } = default!;
    public string DestinationPath { get; set; } = default!;
    public string[] ImageFiles { get; set; } = default!;
}
