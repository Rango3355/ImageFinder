namespace ImageFinder.app.Helpers;

public static class DirectoryHelpers
{
    public static FolderBrowserDialog GetImageDirectory()
    {
        FolderBrowserDialog dialog = new()
        {
            Description = "Select a directory to search for images",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = true
        };
        return dialog;
    }
}