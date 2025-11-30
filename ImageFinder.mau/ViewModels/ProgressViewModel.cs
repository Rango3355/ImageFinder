using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageFinder.application.Collector;
using ImageFinder.domain.Models;

namespace ImageFinder.mau.ViewModels;

public partial class ProgressViewModel : ObservableObject
{

    private readonly IImageCollector _imageCollector;
    private readonly ImageDirectoryModel _imageDirectoryModel;

    private double _progress;
    public double Progress
    {
        get => _progress;
        set => SetProperty(ref _progress, value);
    }

    public IAsyncRelayCommand StartProcessingCommand { get; } = default!;

    public ProgressViewModel(
            IImageCollector imageCollector,
            ImageDirectoryModel imageDirectoryModel)
    {
        _imageCollector = imageCollector;
        _imageDirectoryModel = imageDirectoryModel;
        StartProcessingCommand = new AsyncRelayCommand(StartProcessingAsync);
    }

    public async Task StartProcessingAsync()
    {
        Progress<double> progress = new(p => Progress = p);

        await _imageCollector.CollectAndOrganiseImagesAsync(
            _imageDirectoryModel,
            progress);
    }
}
