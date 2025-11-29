using ImageFinder.domain.Models;

namespace ImageFinder.application.Collector;

public interface IImageCollector
{
    /// <summary>
    /// Asynchronously collects image files from the specified directory and organizes them according to the provided
    /// model.
    /// </summary>
    /// <param name="imageDirectoryModel">An object that specifies the source directory and organization rules for image collection. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task completes when all images have been collected and
    /// organized.</returns>
    Task CollectAndOrganiseImagesAsync(ImageDirectoryModel imageDirectoryModel);
}
