//using BaseData;
//using DevExpress.XtraEditors;
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
//    public partial class BaseTreeListControl : BaseDataControl
//    {
//        private TreeList treeList1 = new TreeList();
//        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
//        public BaseTreeListControl():base()
//        {
            
//        }
//        public BaseTreeListControl(IBaseDataContract dataContract):base(dataContract)
//        {
            
//        }
//        public override void InitControl(IBaseDataContract dataContract)
//        {
//            base.InitControl(dataContract);
//            treeList1.Name = "treeList1";
//            treeList1.OptionsBehavior.EditingMode = TreeListEditingMode.EditForm;
//            layoutControlItem1.Control = treeList1;
//            Root.Add(layoutControlItem1);
//            Root.Remove(emptySpaceDel);

            
//            foreach (var propertyInfo in _curentDataType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
//            {
//                string attributeValue = (Attribute.GetCustomAttribute(propertyInfo,
//                    typeof(CommonUtils.Attributes.DisplayNameAttribute)) as CommonUtils.Attributes.DisplayNameAttribute)?.Name;
//                if( Attribute.IsDefined(propertyInfo, typeof(CommonUtils.Attributes.ParentIdAttribute)))
//                {
//                    treeList1.KeyFieldName = "Id";
//                    treeList1.ParentFieldName = propertyInfo.Name;
//                }
//                var col = treeList1.Columns.Add();
//                col.Name = $"col{propertyInfo.Name}";
//                col.FieldName = propertyInfo.Name;
//                col.Caption = attributeValue ?? propertyInfo.Name;
//                col.Visible = attributeValue != null;
                
//            }

//            treeList1.DataSource = Convert.ChangeType(_dataContract.GetData(), _dataContract.GetDataType());
//        }
//    }
//}
