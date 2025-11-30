using CommunityToolkit.Maui;
using ImageFinder.application.Collector;
using ImageFinder.domain.Models;
using ImageFinder.mau.Pages;
using ImageFinder.mau.ViewModels;
using Microsoft.Extensions.Logging;

namespace ImageFinder.mau;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<ImageDirectoryModel>();
        builder.Services.AddSingleton<IImageCollector, ImageCollector>();
        builder.Services.AddTransient<ProgressViewModel>();
        builder.Services.AddTransient<ProgressPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
