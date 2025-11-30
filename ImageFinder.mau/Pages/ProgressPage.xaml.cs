using ImageFinder.mau.ViewModels;

namespace ImageFinder.mau.Pages;

public partial class ProgressPage : ContentPage
{
    public ProgressPage(ProgressViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnStartOver_Clicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
