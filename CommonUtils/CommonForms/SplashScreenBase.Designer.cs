namespace CommonForms
{
    partial class SplashScreenBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
		private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenBase));
            progressBarControl = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            labelCopyright = new DevExpress.XtraEditors.LabelControl();
            labelStatus = new DevExpress.XtraEditors.LabelControl();
            peImage = new DevExpress.XtraEditors.PictureEdit();
            peLogo = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)peLogo.Properties).BeginInit();
            SuspendLayout();
            // 
            // progressBarControl
            // 
            progressBarControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarControl.EditValue = 0;
            progressBarControl.Location = new Point(24, 232);
            progressBarControl.Name = "progressBarControl";
            progressBarControl.Size = new Size(402, 12);
            progressBarControl.TabIndex = 5;
            // 
            // labelCopyright
            // 
            labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            labelCopyright.Location = new Point(24, 287);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(47, 13);
            labelCopyright.TabIndex = 6;
            labelCopyright.Text = "Copyright";
            // 
            // labelStatus
            // 
            labelStatus.Location = new Point(24, 215);
            labelStatus.Margin = new Padding(3, 3, 3, 1);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(50, 13);
            labelStatus.TabIndex = 7;
            labelStatus.Text = "Starting...";
            // 
            // peImage
            // 
            peImage.Dock = DockStyle.Top;
            peImage.EditValue = resources.GetObject("peImage.EditValue");
            peImage.Location = new Point(1, 1);
            peImage.Name = "peImage";
            peImage.Properties.AllowFocused = false;
            peImage.Properties.Appearance.BackColor = Color.Transparent;
            peImage.Properties.Appearance.Options.UseBackColor = true;
            peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peImage.Properties.ShowMenu = false;
            peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            peImage.Properties.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            peImage.Size = new Size(448, 200);
            peImage.TabIndex = 9;
            // 
            // peLogo
            // 
            peLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            peLogo.EditValue = resources.GetObject("peLogo.EditValue");
            peLogo.Location = new Point(279, 267);
            peLogo.Name = "peLogo";
            peLogo.Properties.AllowFocused = false;
            peLogo.Properties.Appearance.BackColor = Color.Transparent;
            peLogo.Properties.Appearance.Options.UseBackColor = true;
            peLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peLogo.Properties.ShowMenu = false;
            peLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            peLogo.Size = new Size(158, 42);
            peLogo.TabIndex = 8;
            // 
            // SplashScreenBase
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 320);
            Controls.Add(peImage);
            Controls.Add(peLogo);
            Controls.Add(labelStatus);
            Controls.Add(labelCopyright);
            Controls.Add(progressBarControl);
            Name = "SplashScreenBase";
            Padding = new Padding(1);
            Text = "SplashScreenBase";
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)peLogo.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelCopyright;
        private DevExpress.XtraEditors.PictureEdit peLogo;
        private DevExpress.XtraEditors.PictureEdit peImage;
        private DevExpress.XtraEditors.LabelControl labelStatus;
    }
}
