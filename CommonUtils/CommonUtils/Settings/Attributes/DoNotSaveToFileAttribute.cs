using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Settings.Attributes
{
    /// <summary>
    /// Указывает, что свойство/поле не записываться в файл настроек
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DoNotSaveToFileAttribute : Attribute
    {
        public DoNotSaveToFileAttribute()
        {
            
        }
        

    }
}
