using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFinder.application.Organiser;
using ImageFinder.wpf.Services;

namespace ImageFinder.wpf.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private const string InitialStatusMessage = "Choose your source and destination folders to begin.";
    private const string OrganizingStatusMessage = "Organising images...";

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
    private string statusMessage = InitialStatusMessage;

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
        TrySetPathFromPicker(
            "Select the folder that currently holds your photos",
            path => SourcePath = path,
            "Source selected. Now choose where the organised images should go.");
        return Task.CompletedTask;
    }

    private Task BrowseDestinationAsync()
    {
        TrySetPathFromPicker(
            "Select where you want the organised photos to be stored",
            path => DestinationPath = path,
            "Ready when you are. Hit start to organise your images.");
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
            StatusMessage = OrganizingStatusMessage;
            StartProcessingCommand.NotifyCanExecuteChanged();

            Progress<double> progress = new(p =>
            {
                Progress = p;
                StatusMessage = $"{OrganizingStatusMessage} {ProgressPercent}";
            });

            await _imageOrganizer.OrganizeAsync(
                SourcePath!,
                DestinationPath!,
                progress);
            Progress = 1;
            StatusMessage = $"All done!";
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
        StatusMessage = InitialStatusMessage;
        StartProcessingCommand.NotifyCanExecuteChanged();
    }

    private void TrySetPathFromPicker(string prompt, Action<string> setPath, string successMessage)
    {
        string? pickedPath = _folderPicker.PickFolder(prompt);
        if (string.IsNullOrWhiteSpace(pickedPath))
        {
            return;
        }

        setPath(pickedPath);
        StatusMessage = successMessage;
        StartProcessingCommand.NotifyCanExecuteChanged();
    }
}
