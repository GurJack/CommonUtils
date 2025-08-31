//using DevExpress.XtraEditors;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace CommonForms.Views
//{
//    public partial class BaseForm : DevExpress.XtraEditors.XtraForm 
//    {
//        public object Result = null;
//        public BaseForm()
//        {
//            InitializeComponent();
//        }
//        public static BaseForm CreateForm<T>(string formName, bool visibleCancel = true, FormBorderStyle stale = FormBorderStyle.Sizable) where T : BaseUserControl, new()
//        {
//            var result = new BaseForm();
//            var control = new T();
//            control.SetBaseForm(result);
//            result.Width = control.Width+30;
//            result.Height = control.Height+80;
//            result.panelControl1.Controls.Add(control);
//            control.Dock = DockStyle.Fill;
//            result.Text = formName;
//            result.FormBorderStyle = stale;
//            result.bCancel.Visible = visibleCancel;


//            return result;
//        }

//    }
//}