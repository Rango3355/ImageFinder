namespace ImageFinder.mau.Helpers;

public static class ServiceHelper
{
    public static T GetService<T>() where T : class =>
        Current.GetService<T>()!;

    public static IServiceProvider Current =>
        IPlatformApplication.Current?.Services
        ?? throw new InvalidOperationException("Platform application services are not available.");
}
