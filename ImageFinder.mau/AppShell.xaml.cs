namespace ImageFinder.mau;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Pages.ProgressPage), typeof(Pages.ProgressPage));
    }
}
