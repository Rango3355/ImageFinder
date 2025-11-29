using ImageFinder.domain.Models;
using ImageFinder.mau.Helper;

namespace ImageFinder.mau;

public partial class MainPage : ContentPage
{
    private readonly ImageDirectoryModel _imageDirectoryModel = new();
    private readonly DirectoryHelper _directoryHelper = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSourcePathClicked(object? sender, EventArgs e)
    {
        await _directoryHelper.HandlePathSelectionAsync(lblSourcePath, path => _imageDirectoryModel.SourceDirectoryPath = path);
    }

    private async void OnDestinationPathClicked(object? sender, EventArgs e)
    {
        await _directoryHelper.HandlePathSelectionAsync(lblDestinationPath, path => _imageDirectoryModel.DestinationDirectoryPath = path);
    }
}