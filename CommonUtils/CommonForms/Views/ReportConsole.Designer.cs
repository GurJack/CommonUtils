//namespace CommonForms.Views
//{
//    partial class ReportConsole
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

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            components = new System.ComponentModel.Container();
//            btnClose = new DevExpress.XtraEditors.SimpleButton();
//            reReport = new DevExpress.XtraRichEdit.RichEditControl();
//            timer1 = new System.Windows.Forms.Timer(components);
//            btnClear = new DevExpress.XtraEditors.SimpleButton();
//            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
//            SuspendLayout();
//            // 
//            // btnClose
//            // 
//            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
//            btnClose.Location = new Point(701, 395);
//            btnClose.Name = "btnClose";
//            btnClose.Size = new Size(75, 23);
//            btnClose.TabIndex = 0;
//            btnClose.Text = "Закрыть";
//            btnClose.Click += btnClose_Click;
//            // 
//            // reReport
//            // 
//            reReport.AcceptsEscape = false;
//            reReport.AcceptsReturn = false;
//            reReport.AcceptsTab = false;
//            reReport.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
//            reReport.Location = new Point(0, 0);
//            reReport.Name = "reReport";
//            reReport.Options.DocumentCapabilities.ActiveX = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Bookmarks = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Comments = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Fields = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.HeadersFooters = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.InlineShapes = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Macros = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Numbering.Bulleted = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Numbering.MultiLevel = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Numbering.Simple = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.OleObjects = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.ParagraphBorders = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.ParagraphFrames = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.ParagraphStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.ParagraphTabs = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Sections = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.Tables = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentCapabilities.TableStyle = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
//            reReport.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
//            reReport.Options.HorizontalRuler.ShowLeftIndent = false;
//            reReport.Options.HorizontalRuler.ShowRightIndent = false;
//            reReport.Options.HorizontalRuler.ShowTabs = false;
//            reReport.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
//            reReport.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
//            reReport.ReadOnly = true;
//            reReport.Size = new Size(786, 385);
//            reReport.TabIndex = 1;
//            reReport.Unit = DevExpress.Office.DocumentUnit.Point;
//            reReport.Views.PrintLayoutView.AllowDisplayLineNumbers = false;
//            // 
//            // timer1
//            // 
//            timer1.Interval = 1000;
//            timer1.Tick += timer1_Tick;
//            // 
//            // btnClear
//            // 
//            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
//            btnClear.Location = new Point(620, 395);
//            btnClear.Name = "btnClear";
//            btnClear.Size = new Size(75, 23);
//            btnClear.TabIndex = 2;
//            btnClear.Text = "Очистить";
//            btnClear.Click += btnClear_Click;
//            // 
//            // backgroundWorker1
//            // 
//            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
//            // 
//            // ReportConsole
//            // 
//            AcceptButton = btnClose;
//            AutoScaleDimensions = new SizeF(6F, 13F);
//            AutoScaleMode = AutoScaleMode.Font;
//            CancelButton = btnClose;
//            ClientSize = new Size(788, 430);
//            Controls.Add(btnClear);
//            Controls.Add(reReport);
//            Controls.Add(btnClose);
//            Name = "ReportConsole";
//            StartPosition = FormStartPosition.CenterParent;
//            Text = "Консоль";
//            Shown += ReportConsole_Shown;
//            ResumeLayout(false);
//        }

//        #endregion

//        private DevExpress.XtraEditors.SimpleButton btnClose;
//        private DevExpress.XtraRichEdit.RichEditControl reReport;
//        private System.Windows.Forms.Timer timer1;
//        private DevExpress.XtraEditors.SimpleButton btnClear;
//        private System.ComponentModel.BackgroundWorker backgroundWorker1;
//    }
//}