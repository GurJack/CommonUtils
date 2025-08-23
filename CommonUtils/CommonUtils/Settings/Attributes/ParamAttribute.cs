//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CommonUtils.Settings.Attributes
//{
//    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
//    public class ParamAttribute : Attribute
//    {
//        public ParamAttribute(string moduleName, string paramName, string displayName = null, string typeValue = null)
//        {
//            ModuleName = moduleName;
//            ParamName = paramName;
//            DisplayName = displayName ?? paramName;
//            TypeValue = typeValue;
//        }
//        public string ModuleName { get; set; }
//        public string ParamName { get; set; }
//        public string DisplayName { get; set; }
//        public string TypeValue { get; set; }
//    }
//}
