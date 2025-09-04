//namespace CommonForms.Views
//{
//    partial class BaseDataControl
//    {
//        /// <summary> 
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        /// <summary> 
//        /// Required method for Designer support - do not modify 
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseDataControl));
//            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
//            bDelete = new DevExpress.XtraEditors.SimpleButton();
//            bCopy = new DevExpress.XtraEditors.SimpleButton();
//            bInsert = new DevExpress.XtraEditors.SimpleButton();
//            Root = new DevExpress.XtraLayout.LayoutControlGroup();
//            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
//            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
//            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
//            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
//            emptySpaceDel = new DevExpress.XtraLayout.EmptySpaceItem();
//            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
//            layoutControl1.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)emptySpaceDel).BeginInit();
//            SuspendLayout();
//            // 
//            // layoutControl1
//            // 
//            layoutControl1.Controls.Add(bDelete);
//            layoutControl1.Controls.Add(bCopy);
//            layoutControl1.Controls.Add(bInsert);
//            layoutControl1.Dock = DockStyle.Fill;
//            layoutControl1.Location = new Point(0, 0);
//            layoutControl1.Name = "layoutControl1";
//            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1111, 160, 650, 400);
//            layoutControl1.Root = Root;
//            layoutControl1.Size = new Size(432, 287);
//            layoutControl1.TabIndex = 0;
//            layoutControl1.Text = "";
//            // 
//            // bDelete
//            // 
//            bDelete.ImageOptions.Image = (Image)resources.GetObject("bDelete.ImageOptions.Image");
//            bDelete.Location = new Point(92, 12);
//            bDelete.Name = "bDelete";
//            bDelete.Size = new Size(36, 36);
//            bDelete.StyleController = layoutControl1;
//            bDelete.TabIndex = 3;
//            bDelete.Click += bDelete_Click;
//            // 
//            // bCopy
//            // 
//            bCopy.ImageOptions.Image = (Image)resources.GetObject("bCopy.ImageOptions.Image");
//            bCopy.Location = new Point(52, 12);
//            bCopy.Name = "bCopy";
//            bCopy.Size = new Size(36, 36);
//            bCopy.StyleController = layoutControl1;
//            bCopy.TabIndex = 2;
//            bCopy.Click += bCopy_Click;
//            // 
//            // bInsert
//            // 
//            bInsert.ImageOptions.Image = (Image)resources.GetObject("bInsert.ImageOptions.Image");
//            bInsert.Location = new Point(12, 12);
//            bInsert.Name = "bInsert";
//            bInsert.Size = new Size(36, 36);
//            bInsert.StyleController = layoutControl1;
//            bInsert.TabIndex = 1;
//            bInsert.Click += bInsert_Click;
//            // 
//            // Root
//            // 
//            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
//            Root.GroupBordersVisible = false;
//            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3, layoutControlItem4, emptySpaceItem1, emptySpaceDel });
//            Root.Name = "Root";
//            Root.Size = new Size(432, 287);
//            Root.TextVisible = false;
//            // 
//            // layoutControlItem2
//            // 
//            layoutControlItem2.Control = bInsert;
//            layoutControlItem2.Location = new Point(0, 0);
//            layoutControlItem2.MaxSize = new Size(40, 40);
//            layoutControlItem2.MinSize = new Size(40, 40);
//            layoutControlItem2.Name = "layoutControlItem2";
//            layoutControlItem2.Size = new Size(40, 40);
//            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
//            layoutControlItem2.TextSize = new Size(0, 0);
//            layoutControlItem2.TextVisible = false;
//            // 
//            // layoutControlItem3
//            // 
//            layoutControlItem3.Control = bCopy;
//            layoutControlItem3.Location = new Point(40, 0);
//            layoutControlItem3.MaxSize = new Size(40, 40);
//            layoutControlItem3.MinSize = new Size(40, 40);
//            layoutControlItem3.Name = "layoutControlItem3";
//            layoutControlItem3.Size = new Size(40, 40);
//            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
//            layoutControlItem3.TextSize = new Size(0, 0);
//            layoutControlItem3.TextVisible = false;
//            // 
//            // layoutControlItem4
//            // 
//            layoutControlItem4.Control = bDelete;
//            layoutControlItem4.Location = new Point(80, 0);
//            layoutControlItem4.MaxSize = new Size(40, 40);
//            layoutControlItem4.MinSize = new Size(40, 40);
//            layoutControlItem4.Name = "layoutControlItem4";
//            layoutControlItem4.Size = new Size(40, 40);
//            layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
//            layoutControlItem4.TextSize = new Size(0, 0);
//            layoutControlItem4.TextVisible = false;
//            // 
//            // emptySpaceItem1
//            // 
//            emptySpaceItem1.AllowHotTrack = false;
//            emptySpaceItem1.Location = new Point(120, 0);
//            emptySpaceItem1.Name = "emptySpaceItem1";
//            emptySpaceItem1.Size = new Size(292, 40);
//            emptySpaceItem1.TextSize = new Size(0, 0);
//            // 
//            // emptySpaceDel
//            // 
//            emptySpaceDel.AllowHotTrack = false;
//            emptySpaceDel.Location = new Point(0, 40);
//            emptySpaceDel.Name = "emptySpaceDel";
//            emptySpaceDel.Size = new Size(412, 227);
//            emptySpaceDel.TextSize = new Size(0, 0);
//            // 
//            // BaseDataControl
//            // 
//            AutoScaleDimensions = new SizeF(6F, 13F);
//            AutoScaleMode = AutoScaleMode.Font;
//            Controls.Add(layoutControl1);
//            Name = "BaseDataControl";
//            Size = new Size(432, 287);
//            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
//            layoutControl1.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
//            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
//            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
//            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
//            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
//            ((System.ComponentModel.ISupportInitialize)emptySpaceDel).EndInit();
//            ResumeLayout(false);
//        }

//        #endregion

//        private DevExpress.XtraLayout.LayoutControl layoutControl1;
//        private DevExpress.XtraEditors.SimpleButton bDelete;
//        private DevExpress.XtraEditors.SimpleButton bCopy;
//        private DevExpress.XtraEditors.SimpleButton bInsert;
//        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
//        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
//        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
//        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
//        protected DevExpress.XtraLayout.EmptySpaceItem emptySpaceDel;
//        protected DevExpress.XtraLayout.LayoutControlGroup Root;
//    }
//}
