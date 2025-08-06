using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Settings.Providers
{
    public class NoneParamProvider : IParamProvider
    {
        //    public NoneParamProvider(string moduleName, T settingClass) : base(moduleName, settingClass)
        //    {

        //    }

        //    public override ParamItem GetParamInfo(string paramName)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public override string GetParamValue(string paramName, bool isCrypt = false, string cryptoKey = null)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void Set(string paramName, ParamItem paramItem, bool isCrypt = false, string cryptoKey = null)
        //    {

        //    }

        //    public override void SetParamInfo(string paramName, ParamItem paramItem)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public override void SetParamValue(string paramName, string paramItem, bool isCrypt = false, string cryptoKey = null)
        //    {
        //        throw new NotImplementedException();
        //    }
        public ParamItem GetParamInfo(string paramName)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void SetParamValue(string paramName, string paramItem, bool isCrypt = false, string cryptoKey = null)
        {
            throw new NotImplementedException();
        }
    }
}
