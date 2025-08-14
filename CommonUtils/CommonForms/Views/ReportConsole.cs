//using CommonUtils.Event;
//using DevExpress.XtraEditors;
//using DevExpress.XtraRichEdit.API.Native;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.ServiceModel.Channels;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace CommonForms.Views
//{
//    public partial class ReportConsole : DevExpress.XtraEditors.XtraForm
//    {
//        private string _statusMessage;
//        private DocumentRange _statusRange;
//        private int _count = 0;
//        private DevExpress.XtraRichEdit.API.Native.Document _document;

//        public ReportConsole()
//        {
//            InitializeComponent();
//            _document = reReport.Document;
//            _document.DefaultCharacterProperties.FontName = "Courier New";
//            _document.DefaultCharacterProperties.FontSize = 11;
//            _document.DefaultCharacterProperties.ForeColor = Color.Black;
//            timer1.Interval = 1000;
//            timer1.Start();
//            CommonUtils.Event.Events.FormInfo += FormInfoAdd;
//            Clear();

//        }

//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            StopTimer();
//            Close();
//        }

//        public void Clear()
//        {
//            _document.Text = "";
//            _statusRange = _document.CreateRange(_document.Length, 0);
//            _statusMessage = String.Empty;


//        }

//        private Color GetColor(ReportMessageState state)
//        {
//            switch (state)
//            {
//                case ReportMessageState.Info:
//                    return Color.Black;
//                    break;
//                case ReportMessageState.Worning:
//                    return Color.Brown;
//                    break;
//                case ReportMessageState.Error:
//                    return Color.Red;
//                    break;
//                case ReportMessageState.Good:
//                    return Color.Green;
//                    break;
//                default:
//                    throw new NotImplementedException();
//                    break;
//            }
//        }


//        void FormInfoAdd(global::CommonUtils.Event.FormInfoEventArgs e)
//        {
//            if (_document == null)
//                return;
//            if (!String.IsNullOrEmpty(e.Report.StatusLine))
//                _statusMessage = e.Report.StatusLine.Trim();
//            if (!String.IsNullOrEmpty(e.Report.Message))
//            {
//                var range = _document.InsertText(_document.CreatePosition(_statusRange.Start.ToInt() - 1), e.Report.Message + "\n");
//                var titleFormatting = _document.BeginUpdateCharacters(range);
//                titleFormatting.ForeColor = GetColor(e.Report.State);
//                _document.EndUpdateCharacters(titleFormatting);

//            }
//        }

//        private void btnClear_Click(object sender, EventArgs e)
//        {
//            Clear();
//        }
//        private void Refresh()
//        {
//            _document.Delete(_statusRange);
//            _statusRange = _document.AppendText($"{_count} -" + _statusMessage);




//        }

//        private void StopTimer()
//        {
//            timer1.Stop();
//            Refresh();
//            //_document.EndUpdate();
//            Application.DoEvents();
//        }
//        private void StartTimer()
//        {
//            //_document.BeginUpdate();
//            timer1.Start();
//        }
//        private void timer1_Tick(object sender, EventArgs e)
//        {
//            _count++;
//            StopTimer();
//            StartTimer();
//        }

//        private void ReportConsole_Shown(object sender, EventArgs e)
//        {
//            StartTimer();
//        }

//        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
//        {

//        }
//    }
//}