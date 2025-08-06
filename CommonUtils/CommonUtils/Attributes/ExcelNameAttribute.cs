using System;

namespace CommonUtils.Attributes
{

    /// <summary>
    /// Имя для показа
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field)]
    public class ExcelNameAttribute : AttributeBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public ExcelNameAttribute(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get => (string) Value;
            set => Value = value;
        }
    }
}