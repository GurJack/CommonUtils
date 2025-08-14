//using BaseData;
//using CommonUtils;
//using DevExpress.XtraEditors;
//using DevExpress.XtraGrid;
//using DevExpress.XtraGrid.Views.Grid;
//using DevExpress.XtraTreeList;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace CommonForms.Views
//{
//    public partial class BaseGridControl : BaseDataControl
//    {
//        private GridControl gridControl1 = new GridControl();
//        private GridView gridView1 = new GridView();
//        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
//        public BaseGridControl():base()
//        {
            
//        }
//        public BaseGridControl(IBaseDataContract dataContract):base(dataContract) 
//        {
            
//        }

//        public override void InitControl(IBaseDataContract dataContract)
//        {
//            base.InitControl(dataContract);
//            layoutControlItem1.Control = gridControl1;
//            gridControl1.MainView = gridView1;
//            gridControl1.Name = "gridControl1";
//            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
//            gridView1.GridControl = gridControl1;
//            gridView1.Name = "gridView1";
//            gridView1.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
//            gridView1.RowUpdated += gridView1_RowUpdated;
//            gridView1.InitNewRow += gridView1_InitNewRow;
//            Root.Add(layoutControlItem1);
//            Root.Remove(emptySpaceDel);

//            foreach (var propertyInfo in _curentDataType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
//            {
//                string attributeValue = (Attribute.GetCustomAttribute(propertyInfo,
//                    typeof(CommonUtils.Attributes.DisplayNameAttribute)) as CommonUtils.Attributes.DisplayNameAttribute)?.Name;
//                bool isParentId = Attribute.IsDefined(propertyInfo, typeof(CommonUtils.Attributes.ParentIdAttribute));
//                var col = gridView1.Columns.Add();
//                col.Name = $"col{propertyInfo.Name}";
//                col.FieldName = propertyInfo.Name;
//                col.Caption = attributeValue ?? propertyInfo.Name;
//                col.Visible = attributeValue != null;
                


//            }

//            gridControl1.DataSource = Convert.ChangeType(_dataContract.GetData(), _dataContract.GetDataType());
//        }

        
//        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
//        {
//            CheckDataContract();
//            var rec = (BaseModel)Convert.ChangeType(e.Row, _curentDataType);
//            try
//            {

//                if (_insertedRow != null)
//                    _dataContract.Insert(rec);
//                else
//                    _dataContract.Update(rec);
//            }
//            catch (Exception ex)
//            {

//                MessageBox.Show(ex.InnerMessage(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                _dataContract.GetRecord((Guid)rec.Id);
//            }
//            _insertedRow = null;
//            MessageBox.Show("RowUpdated");
//        }

//        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
//        {
//            CheckDataContract();
//            _insertedRow = gridView1.FocusedRowHandle;
//            MessageBox.Show("InitNewRow");
//        }

//        protected override void InsertClick()
//        {
            
//            gridView1.AddNewRow();
//            int newRowHandle = gridView1.FocusedRowHandle;
//            object newRow = gridView1.GetRow(newRowHandle);
//            //gridView1.UpdateCurrentRow();
//        }

//        protected override void DeleteClick()
//        {
            
            
//                _dataContract.Delete((BaseModel)Convert.ChangeType(gridView1.GetRow(gridView1.FocusedRowHandle), _curentDataType));
//                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            

//        }
//        protected override void CopyClick()
//        {

//        }
//    }
//}
