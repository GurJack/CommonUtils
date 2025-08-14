//using System;

//namespace CommonUtils.Attributes
//{
//    /// <summary>
//    /// Атрибут для ссылки для BackLink
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property)]
//    public class DependentListAttribute : AttributeBase
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public DependentListAttribute(params string[] depAttrs)
//            : base(null, false)
//        {
//        }

//        /// <summary>
//        /// Name
//        /// </summary>
//        public string Name
//        {
//            get => (string) Value;
//            set => Value = value;
//        }
//    }

//}