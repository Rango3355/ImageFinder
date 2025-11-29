using ImageFinder.domain.Models;
using ImageFinder.application.Collector;
using ImageFinder.form.Helpers;

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
        throw new NotImplementedException("This method is obsolete and needs to be re-implemented using a cross-platform image processing library.");
        //ImageCollector.CollectAndOrganizeImages(_imageDirectoryModel);
    }
}
