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
using BaseData;
using CommonUtils;
using System.Runtime.Serialization.DataContracts;
using System.Reflection;
using CommonUtils.Settings.Attributes;
using CommonUtils.Attributes;
using Microsoft.SqlServer.Management.Smo;
using System.Windows.Controls;


namespace CommonForms.Views
{
    public partial class BaseDataControl : BaseUserControl//DevExpress.XtraEditors.XtraUserControl
    {
        protected IBaseDataContract _dataContract = null;
        protected Type _curentDataType;
        protected int? _insertedRow = null;
        public BaseDataControl()
        {
            InitializeComponent();
        }
        public BaseDataControl(IBaseDataContract dataContract)
        {
            InitializeComponent();
            InitControl(dataContract);
        }

        public virtual void InitControl(IBaseDataContract dataContract)
        {
            _dataContract = dataContract;
            _curentDataType = _dataContract.GetDataType().GetProperty("Item").PropertyType;

        }

        protected void CheckDataContract()
        {
            if (_dataContract == null)
                throw new Exception("Не инициализированы данные. Воспользуйтесь public BaseDataControl(IBaseDataContract dataContract)");
        }

        private void bInsert_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDataContract();
                InsertClick();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerMessage(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        protected virtual void InsertClick()
        {

        }

        protected virtual void DeleteClick()
        {

        }

        protected virtual void CopyClick()
        {

        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDataContract();
                DeleteClick();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerMessage(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDataContract();
                CopyClick();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerMessage(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
