namespace ImageFinder.domain.Models;

public class ImageDirectoryModel
{
    public string SourceDirectoryPath { get; set; } = default!;
    public string DestinationDirectoryPath { get; set; } = default!;
    public string[] ImageFiles { get; set; } = default!;
}
