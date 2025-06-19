using ImageFinder.app.Collector;
using ImageFinder.app.Helpers;
using ImageFinder.app.Models;

namespace ImageFinder;

public partial class ImageFinder : Form
{
    private readonly ImageDirectoryModel _imageDirectoryModel = new();
    public ImageFinder()
    {
        InitializeComponent();
    }

    private void BtnSourceDirectory_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = DirectoryHelpers.GetImageDirectory();

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _imageDirectoryModel.SourceDirectoryPath = dialog.SelectedPath;

            lblImageDirectory.Text = _imageDirectoryModel.SourceDirectoryPath;
        }
    }

    private void BtnDestination_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = DirectoryHelpers.GetImageDirectory();

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            _imageDirectoryModel.DestinationDirectoryPath = dialog.SelectedPath;

            lblImageDestination.Text = _imageDirectoryModel.DestinationDirectoryPath;
        }
    }

    private void BtmSubmit_Click(object sender, EventArgs e)
    {
        ImageCollector.CollectAndOrganizeImages(_imageDirectoryModel);
    }
}
