using CommonForms.ViewModels;
using DevExpress.XtraEditors;
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
    public partial class BaseUserControl : DevExpress.XtraEditors.XtraUserControl 
    {
        protected BaseForm BaseForm;
        
        public BaseUserControl()
        {
            InitializeComponent();
            
        }

        public void SetBaseForm(BaseForm form)
        {
            
            BaseForm = form;

        }

        public virtual void LoadData(object data) 
        { }

    }
}
