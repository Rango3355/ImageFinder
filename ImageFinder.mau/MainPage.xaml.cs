using ImageFinder.application.Collector;
using ImageFinder.domain.Models;
using ImageFinder.mau.Helpers;

namespace ImageFinder.mau;

public partial class MainPage : ContentPage
{
    private readonly ImageDirectoryModel _imageDirectoryModel = new();
    private readonly DirectoryHelper _directoryHelper = new();
    private readonly IImageCollector _imageCollector;

    public MainPage()
    {
        InitializeComponent();
        _imageCollector = ServiceHelper.GetService<IImageCollector>();
    }
    public MainPage(IImageCollector imageCollector)
    {
        InitializeComponent();
        _imageCollector = imageCollector ?? throw new ArgumentNullException(nameof(imageCollector));
    }

    private async void OnSourcePath_Clicked(object? sender, EventArgs e)
    {
        await _directoryHelper.HandlePathSelectionAsync(lblSourcePath, path => _imageDirectoryModel.SourceDirectoryPath = path);
        await EnableControls(lblSourcePath, btnGetDestinationPath);
    }

    private async void OnDestinationPath_Clicked(object? sender, EventArgs e)
    {
        await _directoryHelper.HandlePathSelectionAsync(lblDestinationPath, path => _imageDirectoryModel.DestinationDirectoryPath = path);
        await EnableControls(lblDestinationPath);
    }

    private async void OnProcessImages_Clicked(object? sender, EventArgs e)
    {
        try
        {
            if (_imageDirectoryModel.SourceDirectoryPath == null || _imageDirectoryModel.DestinationDirectoryPath == null)
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

    private Task EnableControls(Label targetLabel, Button? targetButton = null)
    {
        if (targetLabel == null)
        {
            return Task.CompletedTask;
        }

        bool sourcePath = !string.IsNullOrEmpty(_imageDirectoryModel.SourceDirectoryPath);
        bool destinationPath = !string.IsNullOrEmpty(_imageDirectoryModel.DestinationDirectoryPath);

        bool isSourceLabel = ReferenceEquals(targetLabel, lblSourcePath);
        bool relatedPathSet = isSourceLabel ? sourcePath : destinationPath;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            targetButton?.SetValue(VisualElement.IsVisibleProperty, sourcePath);

            targetLabel.SetValue(VisualElement.IsVisibleProperty, relatedPathSet);

            btnStartProcess.SetValue(VisualElement.IsVisibleProperty, sourcePath && destinationPath);
        });

        return Task.CompletedTask;
    }
}