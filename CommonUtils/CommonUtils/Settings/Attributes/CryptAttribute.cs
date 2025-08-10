using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils.Settings.Attributes
{
    /// <summary>
    /// Указывает, должно ли свойство/поле шифроваться при сохранении
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CryptAttribute : Attribute
    {
        /// <param name="isCrypt">Требуется ли шифрование</param>
        public CryptAttribute(bool isCrypt) => IsCrypt = isCrypt;

        /// <summary>
        /// Флаг необходимости шифрования
        /// </summary>
        public bool IsCrypt { get; set; }
    }

}
