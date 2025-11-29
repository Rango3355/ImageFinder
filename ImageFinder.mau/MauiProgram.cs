using CommunityToolkit.Maui;
using ImageFinder.application.Collector;
using Microsoft.Extensions.Logging;

namespace ImageFinder.mau;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<IImageCollector, ImageCollector>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
