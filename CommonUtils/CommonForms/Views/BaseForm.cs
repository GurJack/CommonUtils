#if !CI_BUILD
using DevExpress.XtraEditors;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonForms.Views
{
#if !CI_BUILD
    public partial class BaseForm : DevExpress.XtraEditors.XtraForm
#else
    public partial class BaseForm : Form
#endif
    {
        public object Result = null;
        public BaseForm()
        {
            InitializeComponent();
        }

        public static BaseForm CreateForm<T>(string formName, bool visibleCancel = true, FormBorderStyle stale = FormBorderStyle.Sizable) where T : BaseUserControl, new()
        {
            var result = new BaseForm();
            var control = new T();
            control.SetBaseForm(result);
            result.Width = control.Width+30;
            result.Height = control.Height+80;
#if !CI_BUILD
            result.panelControl1.Controls.Add(control);
#else
            // В CI режиме используем обычный Panel
            var panel = new Panel();
            panel.Controls.Add(control);
            result.Controls.Add(panel);
#endif
            control.Dock = DockStyle.Fill;
            result.Text = formName;
            result.FormBorderStyle = stale;
#if !CI_BUILD
            result.bCancel.Visible = visibleCancel;
#endif
            return result;
        }
    }
}
