using ImageFinder.app.Helpers;
using FluentAssertions;

namespace ImageFinder.App.Tests.Helpers;

public class DirectoryHelpersTests
{
    [Fact]
    public void GetImageDirectory_ShouldReturnConfiguredFolderBrowserDialog()
    {
        // Act
        var dialog = DirectoryHelpers.GetImageDirectory();

        // Assert
        dialog.Should().NotBeNull();
        dialog.Description.Should().Be("Select a directory to search for images");
        dialog.UseDescriptionForTitle.Should().BeTrue();
        dialog.ShowNewFolderButton.Should().BeTrue();
    }
  
}