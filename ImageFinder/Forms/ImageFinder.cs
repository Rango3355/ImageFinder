namespace ImageFinder;

public partial class ImageFinder : Form
{
    public ImageFinder()
    {
        InitializeComponent();
    }

    private void btnSelectDirectory_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog dialog = new();

        dialog.Description = "Select a directory to search for images";
        dialog.UseDescriptionForTitle = true;
        dialog.ShowNewFolderButton = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            string selectedPath = dialog.SelectedPath;

            lblImageDirectory.Text = selectedPath;
        }
    }
}
