//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CommonUtils.Settings.Providers
//{

//    [ConfigurationCollection(typeof(ApplicationSettingElement))]
//    public class ApplicationSettingsCollection : ConfigurationElementCollection
//    {
//        protected override ConfigurationElement CreateNewElement()
//        {
//            return new ApplicationSettingElement();
//        }

//        protected override object GetElementKey(ConfigurationElement element)
//        {
//            return ((ApplicationSettingElement)(element)).Name;
//        }

//        public ApplicationSettingElement this[int idx]
//        {
//            get { return (ApplicationSettingElement)BaseGet(idx); }

//        }

//    }
//}
