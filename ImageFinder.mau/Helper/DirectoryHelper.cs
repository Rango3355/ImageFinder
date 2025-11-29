
using CommunityToolkit.Maui.Storage;

namespace ImageFinder.mau.Helper;

public class DirectoryHelper
{
    public async Task HandlePathSelectionAsync(Label targetLabel, Action<string> setModelPath)
    {
        FolderPickerResult? result = await GetDirectory();

        if (result == null || !result.IsSuccessful)
        {
            throw new InvalidOperationException("No folder was selected.");
        }

        string path = result.Folder?.Path ?? string.Empty;
        targetLabel.Text = path;
        setModelPath(path);
        targetLabel.IsVisible = true;
    }

    private static async Task<FolderPickerResult?> GetDirectory()
    {
        FolderPickerResult? directory = await FolderPicker.PickAsync(default);

        return directory;
    }
}