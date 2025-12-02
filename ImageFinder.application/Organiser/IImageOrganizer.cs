using ImageFinder.domain.Models;

namespace ImageFinder.application.Organiser;

public interface IImageOrganizer
{
    Task OrganizeAsync(
        string sourcePath,
        string destinationPath,
        IProgress<double>? progress = null,
        CancellationToken cancellationToken = default);
}
