namespace ImageFinder
{
    partial class ImageFinder
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectDirectory = new Button();
            lblImageDirectory = new Label();
            SuspendLayout();
            // 
            // btnSelectDirectory
            // 
            btnSelectDirectory.Location = new Point(25, 101);
            btnSelectDirectory.Name = "btnSelectDirectory";
            btnSelectDirectory.Size = new Size(94, 29);
            btnSelectDirectory.TabIndex = 0;
            btnSelectDirectory.Text = "Select";
            btnSelectDirectory.UseVisualStyleBackColor = true;
            btnSelectDirectory.Click += btnSelectDirectory_Click;
            // 
            // lblImageDirectory
            // 
            lblImageDirectory.AutoSize = true;
            lblImageDirectory.Location = new Point(143, 105);
            lblImageDirectory.Name = "lblImageDirectory";
            lblImageDirectory.Size = new Size(0, 20);
            lblImageDirectory.TabIndex = 1;
            // 
            // ImageFinder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblImageDirectory);
            Controls.Add(btnSelectDirectory);
            Name = "ImageFinder";
            Text = "ImageFinder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectDirectory;
        private Label lblImageDirectory;
    }
}
