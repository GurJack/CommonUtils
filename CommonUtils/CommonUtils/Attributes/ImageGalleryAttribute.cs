//using System;

//namespace CommonUtils.Attributes
//{
//    /// <summary>
//    /// Атрибут для галереи картинок
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property)]
//    public class ImageGalleryAttribute : AttributeBase
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public ImageGalleryAttribute(string objectClassName, string propertyName = "Identity", bool isUseCode = false)
//            : base(propertyName, false)
//        {
//            if (string.IsNullOrEmpty(objectClassName))
//                throw new ArgumentNullException(nameof(objectClassName));
//            if (string.IsNullOrEmpty(propertyName))
//                throw new ArgumentNullException(nameof(propertyName));

//            ObjectClassName = objectClassName;
//            IsUseCode = isUseCode;
//        }

//        /// <summary>
//        /// IsUseCode
//        /// </summary>
//        public bool IsUseCode { get; set; }

//        /// <summary>
//        /// ObjectClass
//        /// </summary>
//        public string ObjectClassName { get; set; }

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