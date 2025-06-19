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
            btnSourceDirectory = new Button();
            lblImageDirectory = new Label();
            lblDescription = new Label();
            btnDestination = new Button();
            lblImageDestination = new Label();
            btmSubmit = new Button();
            SuspendLayout();
            // 
            // btnSourceDirectory
            // 
            btnSourceDirectory.Location = new Point(35, 76);
            btnSourceDirectory.Margin = new Padding(3, 2, 3, 2);
            btnSourceDirectory.Name = "btnSourceDirectory";
            btnSourceDirectory.Size = new Size(82, 22);
            btnSourceDirectory.TabIndex = 0;
            btnSourceDirectory.Text = "Source";
            btnSourceDirectory.UseVisualStyleBackColor = true;
            btnSourceDirectory.Click += BtnSourceDirectory_Click;
            // 
            // lblImageDirectory
            // 
            lblImageDirectory.AutoSize = true;
            lblImageDirectory.Location = new Point(123, 80);
            lblImageDirectory.Name = "lblImageDirectory";
            lblImageDirectory.Size = new Size(43, 15);
            lblImageDirectory.TabIndex = 1;
            lblImageDirectory.Text = "Source";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(35, 30);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description";
            // 
            // btnDestination
            // 
            btnDestination.Location = new Point(35, 102);
            btnDestination.Margin = new Padding(3, 2, 3, 2);
            btnDestination.Name = "btnDestination";
            btnDestination.Size = new Size(82, 22);
            btnDestination.TabIndex = 3;
            btnDestination.Text = "Destination";
            btnDestination.UseVisualStyleBackColor = true;
            btnDestination.Click += BtnDestination_Click;
            // 
            // lblImageDestination
            // 
            lblImageDestination.AutoSize = true;
            lblImageDestination.Location = new Point(123, 106);
            lblImageDestination.Name = "lblImageDestination";
            lblImageDestination.Size = new Size(67, 15);
            lblImageDestination.TabIndex = 4;
            lblImageDestination.Text = "Destination";
            // 
            // btmSubmit
            // 
            btmSubmit.Location = new Point(35, 152);
            btmSubmit.Name = "btmSubmit";
            btmSubmit.Size = new Size(82, 23);
            btmSubmit.TabIndex = 5;
            btmSubmit.Text = "Submit";
            btmSubmit.UseVisualStyleBackColor = true;
            btmSubmit.Click += BtmSubmit_Click;
            // 
            // ImageFinder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(699, 221);
            Controls.Add(btmSubmit);
            Controls.Add(lblImageDestination);
            Controls.Add(btnDestination);
            Controls.Add(lblDescription);
            Controls.Add(lblImageDirectory);
            Controls.Add(btnSourceDirectory);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ImageFinder";
            Text = "ImageFinder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSourceDirectory;
        private Label lblImageDirectory;
        private Label lblDescription;
        private Button btnDestination;
        private Label lblImageDestination;
        private Button btmSubmit;
    }
}
