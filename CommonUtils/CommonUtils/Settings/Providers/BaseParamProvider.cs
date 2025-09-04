//using CommonUtils.Settings.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace CommonUtils.Settings.Providers
//{
//    public abstract class BaseParamProvider<T>:IParamProvider where T:BaseSettings
//    {
//        public string ModuleName { get; }
//        private T _settingClass { get; }
//        public BaseParamProvider(string moduleName, T settingClass) 
//        {
//            ModuleName = moduleName;
//            _settingClass = settingClass;
//        }
//        public virtual ParamItem GetParamInfo(string paramName)
//        {
//            ParamItem result = null;
//            var type = _settingClass.GetType();
//            MemberInfo prop =type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == paramName);
//            if (prop == null) 
//                prop = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == paramName);
//            if (prop == null || !Attribute.IsDefined(prop, typeof(ParamAttribute)))
//                return result;
//            var paramAttributeValue = Attribute.GetCustomAttribute(prop, typeof(ParamAttribute)) as ParamAttribute;
//            if(paramAttributeValue.ModuleName != _moduleName)
//                return result;
//            result = new ParamItem { Name=paramName,DisplayName=paramAttributeValue.DisplayName,TypeValue=paramAttributeValue.TypeValue};

//            if (Attribute.IsDefined(prop, typeof(CryptAttribute)))
//            { 
//                var cryptAttributeValue = Attribute.GetCustomAttribute(prop, typeof(CryptAttribute)) as CryptAttribute;
//                result.IsCrypt = cryptAttributeValue.IsCrypt;
//            }
                

//            return result;
//        }
//        public abstract void SetParamInfo(string paramName, ParamItem paramItem, bool insertIfAbsent = true);
//        public abstract string GetParamValue(string paramName, bool isCrypt = false, string cryptoKey = null);
//        public abstract void SetParamValue(string paramName, string paramItem, bool isCrypt = false, string cryptoKey = null);

//        public void SetAllModulePatams(List<ParamItem> paramList, bool insertIfAbsent = true)
//        {
//            throw new NotImplementedException();
//        }

//        public List<ParamItem> LoadAllModulePatams()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
