using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Settings.Providers
{
    public class ApplicationSettingElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["name"])); }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("value", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Value
        {
            get { return ((string)(base["value"])); }
            set { base["value"] = value; }
        }
        [ConfigurationProperty("displayName", DefaultValue = "", IsKey = false, IsRequired = false)]

        public string DisplayName
        {
            get { return ((string)(base["displayName"])); }
            set { base["displayName"] = value; }
        }
        [ConfigurationProperty("serializeAs", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string TypeValue
        {
            get { return ((string)(base["serializeAs"])); }
            set { base["serializeAs"] = value; }
        }
        [ConfigurationProperty("isCrypt", DefaultValue = "false", IsKey = false, IsRequired = false)]
        public bool IsCrypt
        {
            get { return ((bool)(base["isCrypt"])); }
            set { base["isCrypt"] = value; }
        }

    }
     
}
