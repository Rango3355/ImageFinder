using ImageFinder.app.Models;

namespace ImageFinder;

public partial class ImageFinder : Form
{
    private ImageDirectoryModel _imageDirectoryModel = new();
    public ImageFinder()
    {
        InitializeComponent();
    }

    private void BtnSelectDirectory_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new();

        dialog.Description = "Select a directory to search for images";
        dialog.UseDescriptionForTitle = true;
        dialog.ShowNewFolderButton = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _imageDirectoryModel.DirectoryPath = dialog.SelectedPath;

            lblImageDirectory.Text = _imageDirectoryModel.DirectoryPath;
        }
    }
}
