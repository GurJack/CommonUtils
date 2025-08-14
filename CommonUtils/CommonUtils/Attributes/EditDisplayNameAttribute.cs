//using System;

//namespace CommonUtils.Attributes
//{
//    /// <summary>
//    /// Атрибут для выбора полей при редактировании GTIN во время загрузки
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property)]
//    public class EditDisplayNameAttribute : AttributeBase
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="value"></param>
//        public EditDisplayNameAttribute(object value, bool storeInDB = false) : base(value, storeInDB)
//        {

//        }


//        public string Name
//        {
//            get => (string)Value;
//            set => Value = value;
//        }

//    }
//}