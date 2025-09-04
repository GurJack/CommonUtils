//using System;

//namespace CommonUtils.Attributes
//{
//    /// <summary>
//    /// Специфичный Alias для вставки/обновления/удаления объектов.
//    /// Обычно это название таблицы.
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Class)]
//    public class AliasRealAttribute : AttributeBase
//    {
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="value"></param>
//        /// <param name="storeInDB"></param>
//        public AliasRealAttribute(object value, bool storeInDB = false) : base(value, storeInDB)
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