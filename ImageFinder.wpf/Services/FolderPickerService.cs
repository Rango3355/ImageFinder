namespace ImageFinder.wpf.Services;

using System.Windows.Forms;

public class FolderPickerService : IFolderPicker
{
    public string? PickFolder(string description)
    {
        using FolderBrowserDialog dialog = new()
        {
            Description = description,
            ShowNewFolderButton = true,
            UseDescriptionForTitle = true
        };

        DialogResult result = dialog.ShowDialog();
        return result == DialogResult.OK ? dialog.SelectedPath : null;
    }
}
