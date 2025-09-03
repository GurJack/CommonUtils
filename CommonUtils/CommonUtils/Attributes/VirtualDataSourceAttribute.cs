//using System;

//namespace CommonUtils.Attributes
//{
//    /// <summary>
//    /// Attribute for custom collections combobox
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property)]
//    public class VirtualDataSourceAttribute :AttributeBase
//    {

//        private string _methodName;
//        private bool _allowEdit;

//        /// <summary>
//        /// Constructor for method, returns the coolection
//        /// </summary>
//        /// <param name="methodName">Method name, returns the collection</param>
//        /// <param name="allowEdit">Allow edit or not</param>
//        public VirtualDataSourceAttribute(string methodName, bool allowEdit = false) : base(null,false)
//        {
//            _methodName = methodName;
//            _allowEdit = allowEdit;
//        }

//        /// <summary>
//        /// Method returns collection
//        /// </summary>
//        public string MethodName {
//            get { return _methodName;}
//            set { _methodName = value; }
//        }

//        /// <summary>
//        /// Allow inline editing into control
//        /// </summary>
//        public bool AllowEdit
//        {
//            get { return _allowEdit; }
//            set { _allowEdit = value; }
//        }
//    }
//}