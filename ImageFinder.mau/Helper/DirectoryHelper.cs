
using CommunityToolkit.Maui.Storage;

namespace ImageFinder.mau.Helper;

public class DirectoryHelper
{
    public async Task<bool> HandlePathSelectionAsync(Label targetLabel, Action<string> setModelPath)
    {
        FolderPickerResult? result = await GetDirectory();

        if (result == null || !result.IsSuccessful)
        {
            return false;
        }

        string path = result.Folder?.Path ?? string.Empty;
        targetLabel.Text = path;
        setModelPath(path);
        targetLabel.IsVisible = true;

        return true;
    }

    private static async Task<FolderPickerResult?> GetDirectory()
    {
        return await FolderPicker.PickAsync(default);
    }
}