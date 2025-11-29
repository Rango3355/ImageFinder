using ImageFinder.domain.Models;
using ImageFinder.application.Collector;
using ImageFinder.mau.Helper;

namespace ImageFinder.mau;

public partial class MainPage : ContentPage
{
    private readonly ImageDirectoryModel _imageDirectoryModel = new();
    private readonly DirectoryHelper _directoryHelper = new();
    private readonly IImageCollector _imageCollector;

    public MainPage(IImageCollector imageCollector)
    {
        InitializeComponent();
        _imageCollector = imageCollector ?? throw new ArgumentNullException(nameof(imageCollector));
    }

    private async void OnSourcePathClicked(object? sender, EventArgs e)
    {
        await _directoryHelper.HandlePathSelectionAsync(lblSourcePath, path => _imageDirectoryModel.SourceDirectoryPath = path);
    }

    private async void OnDestinationPathClicked(object? sender, EventArgs e)
    {
        await _directoryHelper.HandlePathSelectionAsync(lblDestinationPath, path => _imageDirectoryModel.DestinationDirectoryPath = path);
    }

    private async void OnProcessImages_Clicked(object? sender, EventArgs e)
    {
        try
        {
            if(_imageDirectoryModel.SourceDirectoryPath == null || _imageDirectoryModel.DestinationDirectoryPath == null)
            {
                throw new NullReferenceException("Please select both source and destination directories.");
            }

            await _imageCollector.CollectAndOrganiseImagesAsync(_imageDirectoryModel);
        }
        catch (NullReferenceException ex)
        {
            await DisplayAlertAsync("Input Error", ex.Message, "OK");
        }
        catch (Exception)
        {
            await DisplayAlertAsync("Error", "Unexpected error ", "OK");
        }
    }
}