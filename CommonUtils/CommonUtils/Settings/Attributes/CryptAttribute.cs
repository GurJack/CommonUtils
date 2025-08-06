using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtils.Settings.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CryptAttribute : Attribute
    {
        public CryptAttribute(bool isCrypt)
        {
            IsCrypt = isCrypt;
        }
        public bool IsCrypt { get; set; }

    }

}
