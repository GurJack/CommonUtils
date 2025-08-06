using System;

namespace CommonUtils.Attributes
{
    /// <summary>
    /// Специфичный аттрибут указывающий в какое поле в реестре писать
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Interface)]
    public class RegistryKeyAttribute : AttributeBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public RegistryKeyAttribute(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Name
        /// </summary>
        public string PathKey
        {
            get => (string) Value;
            set => Value = value;
        }
    }
}
