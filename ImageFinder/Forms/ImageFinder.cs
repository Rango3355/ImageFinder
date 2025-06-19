using ImageFinder.app.Helpers;
using ImageFinder.app.Models;

namespace ImageFinder;

public partial class ImageFinder : Form
{
    private ImageDirectoryModel _imageDirectoryModel = new();
    public ImageFinder()
    {
        InitializeComponent();
    }

    private void BtnSourceDirectory_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = DirectoryHelpers.GetImageDirectory();

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _imageDirectoryModel.DirectoryPath = dialog.SelectedPath;

            lblImageDirectory.Text = _imageDirectoryModel.DirectoryPath;
        }
    }

    private void BtnDestination_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = DirectoryHelpers.GetImageDirectory();

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _imageDirectoryModel.DestinationPath = dialog.SelectedPath;

            lblImageDestination.Text = _imageDirectoryModel.DestinationPath;
        }
    }

    private void BtmSubmit_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException("Submit functionality is not implemented yet.");
    }
}
