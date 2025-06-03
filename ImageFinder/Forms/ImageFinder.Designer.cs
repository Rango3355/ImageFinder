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
            lblDescription = new Label();
            SuspendLayout();
            // 
            // btnSelectDirectory
            // 
            btnSelectDirectory.Location = new Point(40, 101);
            btnSelectDirectory.Name = "btnSelectDirectory";
            btnSelectDirectory.Size = new Size(94, 29);
            btnSelectDirectory.TabIndex = 0;
            btnSelectDirectory.Text = "Select";
            btnSelectDirectory.UseVisualStyleBackColor = true;
            btnSelectDirectory.Click += BtnSelectDirectory_Click;
            // 
            // lblImageDirectory
            // 
            lblImageDirectory.AutoSize = true;
            lblImageDirectory.Location = new Point(152, 105);
            lblImageDirectory.Name = "lblImageDirectory";
            lblImageDirectory.Size = new Size(70, 20);
            lblImageDirectory.TabIndex = 1;
            lblImageDirectory.Text = "Directory";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(40, 31);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(85, 20);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description";
            // 
            // ImageFinder
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblDescription);
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
        private Label lblDescription;
    }
}
