using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Settings
{
    public class ParamItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
        //public string ModuleName { get; set; }
        public string TypeValue { get; set; }
        public bool IsCrypt { get; set; }
    }
}
