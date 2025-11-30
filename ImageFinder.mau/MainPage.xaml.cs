using ImageFinder.domain.Models;
using ImageFinder.mau.Helpers;
using ImageFinder.mau.Pages;

namespace ImageFinder.mau;

public partial class MainPage : ContentPage
{
    private readonly ImageDirectoryModel _imageDirectoryModel;
    private readonly DirectoryHelper _directoryHelper = new();

    public MainPage()
    {
        InitializeComponent();
        _imageDirectoryModel = ServiceHelper.GetService<ImageDirectoryModel>();
    }
    public MainPage(ImageDirectoryModel imageDirectoryModel)
    {
        InitializeComponent();
        _imageDirectoryModel = imageDirectoryModel ?? throw new ArgumentNullException(nameof(imageDirectoryModel));
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
        if (string.IsNullOrEmpty(_imageDirectoryModel.SourceDirectoryPath) ||
             string.IsNullOrEmpty(_imageDirectoryModel.DestinationDirectoryPath))
        {
            await DisplayAlertAsync("Input Error", "Please select both source and destination directories.", "OK");
            return;
        }

        await Shell.Current.GoToAsync(nameof(ProgressPage));
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