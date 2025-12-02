using ImageFinder.application.Collector;
using ImageFinder.domain.Models;

namespace ImageFinder.application.Organiser;

public class ImageOrganizer : IImageOrganizer
{
    private readonly IImageCollector _imageCollector;

    public ImageOrganizer(IImageCollector imageCollector)
    {
        _imageCollector = imageCollector;
    }

    public Task OrganizeAsync(
        string sourcePath,
        string destinationPath,
        IProgress<double>? progress = null,
        CancellationToken cancellationToken = default)
    {
        ImageDirectoryModel model = new()
        {
            SourceDirectoryPath = sourcePath,
            DestinationDirectoryPath = destinationPath
        };

        return _imageCollector.CollectAndOrganiseImagesAsync(
            model,
            progress,
            cancellationToken);
    }
}
