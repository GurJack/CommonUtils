using System;

namespace CommonUtils.Attributes
{
    /// <summary>
    /// Base class for custorm attributes
    /// </summary>
    public class AttributeBase : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="storeInDB"></param>
        public AttributeBase(object value, bool storeInDB = true)
        {
            Value = value;
            StoreInDB = storeInDB;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Признак того, что этот атрибут может храниться дополнительно в БД, для переопределения значения.
        /// </summary>
        public bool StoreInDB { get; }
    }
}