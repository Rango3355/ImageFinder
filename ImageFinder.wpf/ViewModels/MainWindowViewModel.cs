using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFinder.application.Organiser;
using ImageFinder.wpf.Services;

namespace ImageFinder.wpf.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IImageOrganizer _imageOrganizer;
    private readonly IFolderPicker _folderPicker;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanStart))]
    [NotifyPropertyChangedFor(nameof(CanPickDestination))]
    private string? sourcePath;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanStart))]
    private string? destinationPath;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ProgressPercent))]
    private double progress;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanStart))]
    [NotifyPropertyChangedFor(nameof(CanPickDestination))]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    private string statusMessage = "Choose your source and destination folders to begin.";

    public bool IsNotBusy => !IsBusy;
    public bool CanPickDestination => !string.IsNullOrWhiteSpace(SourcePath) && !IsBusy;
    public bool CanStart => !IsBusy && !string.IsNullOrWhiteSpace(SourcePath) && !string.IsNullOrWhiteSpace(DestinationPath);
    public string ProgressPercent => $"{Math.Round(Progress * 100):0}%";

    public IAsyncRelayCommand BrowseSourceCommand { get; }
    public IAsyncRelayCommand BrowseDestinationCommand { get; }
    public IAsyncRelayCommand StartProcessingCommand { get; }
    public IRelayCommand ResetCommand { get; }

    public MainWindowViewModel(
        IImageOrganizer imageOrganizer,
        IFolderPicker folderPicker)
    {
        _imageOrganizer = imageOrganizer;
        _folderPicker = folderPicker;

        BrowseSourceCommand = new AsyncRelayCommand(BrowseSourceAsync);
        BrowseDestinationCommand = new AsyncRelayCommand(BrowseDestinationAsync);
        StartProcessingCommand = new AsyncRelayCommand(StartProcessingAsync, () => CanStart);
        ResetCommand = new RelayCommand(Reset);
    }

    private Task BrowseSourceAsync()
    {
        string? pickedPath = _folderPicker.PickFolder("Select the folder that currently holds your photos");
        if (string.IsNullOrWhiteSpace(pickedPath))
        {
            return Task.CompletedTask;
        }

        SourcePath = pickedPath;
        StatusMessage = "Source selected. Now choose where the organised images should go.";
        StartProcessingCommand.NotifyCanExecuteChanged();
        return Task.CompletedTask;
    }

    private Task BrowseDestinationAsync()
    {
        string? pickedPath = _folderPicker.PickFolder("Select where you want the organised photos to be stored");
        if (string.IsNullOrWhiteSpace(pickedPath))
        {
            return Task.CompletedTask;
        }

        DestinationPath = pickedPath;
        StatusMessage = "Ready when you are. Hit start to organise your images.";
        StartProcessingCommand.NotifyCanExecuteChanged();
        return Task.CompletedTask;
    }

    private async Task StartProcessingAsync()
    {
        if (!CanStart)
        {
            StatusMessage = "Please choose both source and destination folders first.";
            return;
        }

        try
        {
            IsBusy = true;
            Progress = 0;
            StatusMessage = "Organising images...";
            StartProcessingCommand.NotifyCanExecuteChanged();

            Progress<double> progress = new(p =>
            {
                Progress = p;
                StatusMessage = $"Organising images... {ProgressPercent}";
            });

            await _imageOrganizer.OrganizeAsync(
                SourcePath!,
                DestinationPath!,
                progress);
            Progress = 1;
            StatusMessage = "Done! Images have been organised into Year/Month folders.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Something went wrong: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            StartProcessingCommand.NotifyCanExecuteChanged();
        }
    }

    private void Reset()
    {
        SourcePath = null;
        DestinationPath = null;
        Progress = 0;
        StatusMessage = "Choose your source and destination folders to begin.";
        StartProcessingCommand.NotifyCanExecuteChanged();
    }
}
