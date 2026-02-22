using System.Windows;
using ImageFinder.application.Collector;
using ImageFinder.application.Organiser;
using ImageFinder.wpf.Services;
using ImageFinder.wpf.ViewModels;

namespace ImageFinder.wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(
            new ImageOrganizer(new ImageCollector()),
            new FolderPickerService());
    }
}
