using System.Windows.Forms;

namespace ImageFinder.wpf.Services;

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

        return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
    }
}
