using ImageFinder.mau.ViewModels;

namespace ImageFinder.mau.Pages;

public partial class ProgressPage : ContentPage
{
    public ProgressPage(ProgressViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
