
namespace DisplacementAlphaTools {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.PreviewLabel = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadVMFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ResizeImageCheckbox = new System.Windows.Forms.CheckBox();
            this.PixelWidthLabel = new System.Windows.Forms.Label();
            this.PixelHeightLabel = new System.Windows.Forms.Label();
            this.Divider = new System.Windows.Forms.Label();
            this.DisplacementCountHorizontalLabel = new System.Windows.Forms.Label();
            this.DisplacementCountVerticalLabel = new System.Windows.Forms.Label();
            this.DisplacementPowerNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DisplacementPowerControlLabel = new System.Windows.Forms.Label();
            this.PixelWidthRequiredLabel = new System.Windows.Forms.Label();
            this.PixelHeightRequiredLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplacementPowerNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewPictureBox.Location = new System.Drawing.Point(12, 40);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(375, 360);
            this.PreviewPictureBox.TabIndex = 0;
            this.PreviewPictureBox.TabStop = false;
            // 
            // PreviewLabel
            // 
            this.PreviewLabel.AutoSize = true;
            this.PreviewLabel.Location = new System.Drawing.Point(12, 24);
            this.PreviewLabel.Name = "PreviewLabel";
            this.PreviewLabel.Size = new System.Drawing.Size(54, 13);
            this.PreviewLabel.TabIndex = 2;
            this.PreviewLabel.Text = "- Preview:";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(399, 24);
            this.MenuStrip.TabIndex = 3;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadImageToolStripMenuItem,
            this.LoadVMFToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // LoadImageToolStripMenuItem
            // 
            this.LoadImageToolStripMenuItem.Name = "LoadImageToolStripMenuItem";
            this.LoadImageToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.LoadImageToolStripMenuItem.Text = "Load Image";
            this.LoadImageToolStripMenuItem.Click += new System.EventHandler(this.LoadImageToolStripMenuItem_Click);
            // 
            // LoadVMFToolStripMenuItem
            // 
            this.LoadVMFToolStripMenuItem.Name = "LoadVMFToolStripMenuItem";
            this.LoadVMFToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.LoadVMFToolStripMenuItem.Text = "Load VMF";
            this.LoadVMFToolStripMenuItem.Click += new System.EventHandler(this.LoadVMFToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // SaveButton
            // 
            this.SaveButton.AutoSize = true;
            this.SaveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveButton.Location = new System.Drawing.Point(12, 404);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(79, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save as .vmf";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ResizeImageCheckbox
            // 
            this.ResizeImageCheckbox.AutoSize = true;
            this.ResizeImageCheckbox.Location = new System.Drawing.Point(275, 408);
            this.ResizeImageCheckbox.Name = "ResizeImageCheckbox";
            this.ResizeImageCheckbox.Size = new System.Drawing.Size(112, 17);
            this.ResizeImageCheckbox.TabIndex = 5;
            this.ResizeImageCheckbox.Text = "Resize image to fit";
            this.ResizeImageCheckbox.UseVisualStyleBackColor = true;
            this.ResizeImageCheckbox.CheckedChanged += new System.EventHandler(this.ResizeImageCheckbox_CheckedChanged);
            // 
            // PixelWidthLabel
            // 
            this.PixelWidthLabel.AutoSize = true;
            this.PixelWidthLabel.Enabled = false;
            this.PixelWidthLabel.Location = new System.Drawing.Point(9, 445);
            this.PixelWidthLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.PixelWidthLabel.Name = "PixelWidthLabel";
            this.PixelWidthLabel.Size = new System.Drawing.Size(38, 13);
            this.PixelWidthLabel.TabIndex = 7;
            this.PixelWidthLabel.Text = "Width:";
            // 
            // PixelHeightLabel
            // 
            this.PixelHeightLabel.AutoSize = true;
            this.PixelHeightLabel.Enabled = false;
            this.PixelHeightLabel.Location = new System.Drawing.Point(9, 460);
            this.PixelHeightLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.PixelHeightLabel.Name = "PixelHeightLabel";
            this.PixelHeightLabel.Size = new System.Drawing.Size(41, 13);
            this.PixelHeightLabel.TabIndex = 8;
            this.PixelHeightLabel.Text = "Height:";
            // 
            // Divider
            // 
            this.Divider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Divider.Location = new System.Drawing.Point(12, 437);
            this.Divider.Margin = new System.Windows.Forms.Padding(5);
            this.Divider.Name = "Divider";
            this.Divider.Size = new System.Drawing.Size(375, 2);
            this.Divider.TabIndex = 9;
            // 
            // DisplacementCountHorizontalLabel
            // 
            this.DisplacementCountHorizontalLabel.AutoSize = true;
            this.DisplacementCountHorizontalLabel.Enabled = false;
            this.DisplacementCountHorizontalLabel.Location = new System.Drawing.Point(9, 475);
            this.DisplacementCountHorizontalLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.DisplacementCountHorizontalLabel.Name = "DisplacementCountHorizontalLabel";
            this.DisplacementCountHorizontalLabel.Size = new System.Drawing.Size(100, 13);
            this.DisplacementCountHorizontalLabel.TabIndex = 10;
            this.DisplacementCountHorizontalLabel.Text = "Horizontal Disp. Ct.:";
            // 
            // DisplacementCountVerticalLabel
            // 
            this.DisplacementCountVerticalLabel.AutoSize = true;
            this.DisplacementCountVerticalLabel.Enabled = false;
            this.DisplacementCountVerticalLabel.Location = new System.Drawing.Point(9, 490);
            this.DisplacementCountVerticalLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.DisplacementCountVerticalLabel.Name = "DisplacementCountVerticalLabel";
            this.DisplacementCountVerticalLabel.Size = new System.Drawing.Size(88, 13);
            this.DisplacementCountVerticalLabel.TabIndex = 11;
            this.DisplacementCountVerticalLabel.Text = "Vertical Disp. Ct.:";
            // 
            // DisplacementPowerNumericUpDown
            // 
            this.DisplacementPowerNumericUpDown.Enabled = false;
            this.DisplacementPowerNumericUpDown.Location = new System.Drawing.Point(217, 407);
            this.DisplacementPowerNumericUpDown.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.DisplacementPowerNumericUpDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.DisplacementPowerNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DisplacementPowerNumericUpDown.Name = "DisplacementPowerNumericUpDown";
            this.DisplacementPowerNumericUpDown.Size = new System.Drawing.Size(31, 20);
            this.DisplacementPowerNumericUpDown.TabIndex = 12;
            this.DisplacementPowerNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // DisplacementPowerControlLabel
            // 
            this.DisplacementPowerControlLabel.AutoSize = true;
            this.DisplacementPowerControlLabel.Enabled = false;
            this.DisplacementPowerControlLabel.Location = new System.Drawing.Point(150, 409);
            this.DisplacementPowerControlLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.DisplacementPowerControlLabel.Name = "DisplacementPowerControlLabel";
            this.DisplacementPowerControlLabel.Size = new System.Drawing.Size(67, 13);
            this.DisplacementPowerControlLabel.TabIndex = 13;
            this.DisplacementPowerControlLabel.Text = "Disp. Power:";
            // 
            // PixelWidthRequiredLabel
            // 
            this.PixelWidthRequiredLabel.AutoSize = true;
            this.PixelWidthRequiredLabel.Enabled = false;
            this.PixelWidthRequiredLabel.Location = new System.Drawing.Point(158, 445);
            this.PixelWidthRequiredLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.PixelWidthRequiredLabel.Name = "PixelWidthRequiredLabel";
            this.PixelWidthRequiredLabel.Size = new System.Drawing.Size(84, 13);
            this.PixelWidthRequiredLabel.TabIndex = 14;
            this.PixelWidthRequiredLabel.Text = "Required Width:";
            // 
            // PixelHeightRequiredLabel
            // 
            this.PixelHeightRequiredLabel.AutoSize = true;
            this.PixelHeightRequiredLabel.Enabled = false;
            this.PixelHeightRequiredLabel.Location = new System.Drawing.Point(156, 460);
            this.PixelHeightRequiredLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.PixelHeightRequiredLabel.Name = "PixelHeightRequiredLabel";
            this.PixelHeightRequiredLabel.Size = new System.Drawing.Size(87, 13);
            this.PixelHeightRequiredLabel.TabIndex = 15;
            this.PixelHeightRequiredLabel.Text = "Required Height:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 515);
            this.Controls.Add(this.PixelHeightRequiredLabel);
            this.Controls.Add(this.PixelWidthRequiredLabel);
            this.Controls.Add(this.DisplacementPowerControlLabel);
            this.Controls.Add(this.DisplacementPowerNumericUpDown);
            this.Controls.Add(this.DisplacementCountVerticalLabel);
            this.Controls.Add(this.DisplacementCountHorizontalLabel);
            this.Controls.Add(this.Divider);
            this.Controls.Add(this.PixelHeightLabel);
            this.Controls.Add(this.PixelWidthLabel);
            this.Controls.Add(this.ResizeImageCheckbox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.PreviewLabel);
            this.Controls.Add(this.PreviewPictureBox);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Displacement Alpha Tools";
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplacementPowerNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.Label PreviewLabel;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ToolStripMenuItem LoadVMFToolStripMenuItem;
        private System.Windows.Forms.CheckBox ResizeImageCheckbox;
        private System.Windows.Forms.Label PixelWidthLabel;
        private System.Windows.Forms.Label PixelHeightLabel;
        private System.Windows.Forms.Label Divider;
        private System.Windows.Forms.Label DisplacementCountHorizontalLabel;
        private System.Windows.Forms.Label DisplacementCountVerticalLabel;
        private System.Windows.Forms.NumericUpDown DisplacementPowerNumericUpDown;
        private System.Windows.Forms.Label DisplacementPowerControlLabel;
        private System.Windows.Forms.Label PixelWidthRequiredLabel;
        private System.Windows.Forms.Label PixelHeightRequiredLabel;
    }
}

