using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Settings.Providers
{
    public class ConfigParamProvider : IParamProvider
    {

        //public override string GetParamValue(string paramName, bool isCrypt = false, string cryptoKey = null)
        //{

        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    return config.AppSettings.Settings[paramName].Value;
        //}

        //public override void SetParamInfo(string paramName, ParamItem paramItem)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void SetParamValue(string paramName, string paramItem, bool isCrypt = false, string cryptoKey = null)
        //{
        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    config.AppSettings.Settings[paramName].Value = paramItem;
        //    config.Save();
        //    ConfigurationManager.RefreshSection("appSettings");
        //}
        public ParamItem GetParamInfo(string paramName)
        {
            var result = new ParamItem { Name = paramName};
           Configuration config =
           ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings[paramName] != null)
                result.Value = ConfigurationManager.AppSettings[paramName] ;
            else
                throw new Exception($"Неизвестный параметр '{paramName}'");
            return result ;
        }

        public string GetParamValue(string paramName, bool isCrypt = false, string cryptoKey = null)
        {
            throw new NotImplementedException();
        }

        public List<ParamItem> LoadAllModulePatams()
        {
            throw new NotImplementedException();
        }

        public void SetAllModulePatams(List<ParamItem> paramList, bool insertIfAbsent = true)
        {
            throw new NotImplementedException();
        }

        public void SetParamInfo(string paramName, ParamItem paramItem, bool insertIfAbsent = true)
        {
            Configuration config =
           ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings[paramName] != null)
                ConfigurationManager.AppSettings[paramName] = paramItem.IsCrypt ? Crypter.Encrypt1(paramItem.Value) : paramItem.Value;
            else
            {
                if (insertIfAbsent)
                    config.AppSettings.Settings.Add(paramName, paramItem.IsCrypt? Crypter.Encrypt1(paramItem.Value): paramItem.Value);
                else
                    throw new Exception($"Неизвестный параметр '{paramName}'");
            }
                

            config.Save(ConfigurationSaveMode.Modified);
        }

        public void SetParamValue(string paramName, string paramItem, bool isCrypt = false, string cryptoKey = null)
        {
            Configuration config =
           ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings[paramName] != null)
                ConfigurationManager.AppSettings[paramName] = isCrypt ? Crypter.Encrypt1(paramItem) : paramItem;
            else
            {
                
                    throw new Exception($"Неизвестный параметр '{paramName}'");
            }


            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
