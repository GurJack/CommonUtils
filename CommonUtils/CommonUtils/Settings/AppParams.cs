//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommonUtils.Settings.Providers;


//namespace CommonUtils.Settings
//{
//    /// <summary>
//    /// Статический класс управления параметрами приложения
//    /// </summary>
//    public static class AppParams
//    {
//        private static Dictionary<string, Dictionary<string, ParamItem>> _params;
//        private static Dictionary<string, IParamProvider> _paramProviders;
//        private static string _cryptoKey;
//        /// <summary>
//        /// 
//        /// </summary>
//        static AppParams()
//        {
//            _paramProviders = new Dictionary<string, IParamProvider>();
//            _params = new Dictionary<string, Dictionary<string, ParamItem>>();
//            SetParamProvider("None",new NoneParamProvider());
//        }
//        //public static ParamItem ReloadParamItem(string moduleName, string paramName)
//        //{
//        //            if (!_paramProviders.TryGetValue(moduleName, out var paramProvider))
//        //                throw new Exception($"Для модуля {moduleName} нет провайдера");
//        //            if(!_params.TryGetValue(moduleName,out var moduleParams))
//        //            {
//        //                moduleParams = new Dictionary<string, ParamItem>();
//        //                _params.Add(moduleName, moduleParams);
//        //            }
//        //    bool findParam = moduleParams.TryGetValue(paramName, out var param);
//        //    bool isCrypt = findParam ? param.IsCrypt : false;
//        //    var newParam = paramProvider.Get(paramName, isCrypt, _cryptoKey);
//        //            if (findParam)
//        //            { 
//        //                moduleParams[paramName] = paramProvider.Get(paramName, param.IsCrypt, _cryptoKey);
//        //            }
//        //            else
//        //    moduleParams.Add(paramName, newParam);
//        //return newParam;
//        //}
//        //public static ParamItem GetParamItem(string moduleName, string paramName) 
//        //{
//        //    var param = FindParamFromList(moduleName, paramName);
//        //    if(param == null)
//        //        param = ReloadParamItem(moduleName, paramName);


//        //    return param;
//        //}
//        public static string ReloadParamValue(string moduleName, string paramName)
//        {
//            if (!_paramProviders.TryGetValue(moduleName, out var paramProvider))
//                throw new Exception($"Для модуля {moduleName} нет провайдера");
//            var param = FindParamFromList(moduleName, paramName);
//            if(param == null)
//                throw new Exception($"Для модуля {moduleName} не найден параметр {paramName}");

            
//                param.Value = paramProvider.GetParamValue(paramName, param.IsCrypt, _cryptoKey);
            

//            return param.Value;
//        }
//        public static string GetParamValue(string moduleName, string paramName)
//        {
//            if (!_paramProviders.TryGetValue(moduleName, out var paramProvider))
//                throw new Exception($"Для модуля {moduleName} нет провайдера");
//            var param = FindParamFromList(moduleName, paramName);
//            if (param == null)
//                throw new Exception($"Для модуля {moduleName} не найден параметр {paramName}");

//            if(String.IsNullOrEmpty(param.Value))
//                param.Value = paramProvider.GetParamValue(paramName, param.IsCrypt, _cryptoKey);
//            return param.Value;
//        }
//        public static ParamItem FindParamFromList(string moduleName, string paramName)
//        {
//            ParamItem param = null;
//            if (_params.TryGetValue(moduleName, out var moduleParams))
//                moduleParams.TryGetValue(paramName, out param);

//            return param;
//        }
//        public static void SetParamValue(string moduleName, string paramName, string value)
//        {
//            if (!_paramProviders.TryGetValue(moduleName, out var paramProvider))
//                throw new Exception($"Для модуля {moduleName} нет провайдера");
//            var param = FindParamFromList(moduleName, paramName);
//            if (param == null)
//                throw new Exception($"Для модуля {moduleName} пареметра {paramName} не существует");
//            param.Value = value;
//            paramProvider.SetParamValue(paramName, param.Value, param.IsCrypt, _cryptoKey);

//        }
//        public static void AddParam(string moduleName, ParamItem param)
//        {
//            if (!_paramProviders.TryGetValue(moduleName, out var paramProvider))
//                throw new Exception($"Для модуля {moduleName} нет провайдера");
//            var paramInBase = FindParamFromList(moduleName, param.Name);
//            if (paramInBase != null)
//                throw new Exception($"Уже существует параметр {param.Name} для модуля {moduleName}");
//            if (!_params.TryGetValue(moduleName, out var moduleParams))
//            {
//                moduleParams = new Dictionary<string, ParamItem>();
//                _params.Add(moduleName, moduleParams);
//            }
//            moduleParams.Add(param.Name, param);

//        }
//        public static void AddParam(string moduleName, string paramName)
//        {
//            if (!_paramProviders.TryGetValue(moduleName, out var paramProvider))
//                throw new Exception($"Для модуля {moduleName} нет провайдера");
//            var paramInBase = FindParamFromList(moduleName, paramName);
//            if (paramInBase != null)
//                throw new Exception($"Уже существует параметр {paramName} для модуля {moduleName}");
//            if (!_params.TryGetValue(moduleName, out var moduleParams))
//            {
//                moduleParams = new Dictionary<string, ParamItem>();
//                _params.Add(moduleName, moduleParams);
//            }
//            moduleParams.Add(paramName, paramProvider.GetParamInfo(paramName));

//        }

//        public static void SetParamProvider(string moduleName, IParamProvider paramProvider)
//        {
//            try
//            {
//                _paramProviders.Add(moduleName, paramProvider);
//            }
//            catch (Exception e)
//            {

//                throw new Exception($"Провайдер для модуля {moduleName} уже добавлен", e);
//            }
//        }
//        public static void SetPassword(string password)
//        {
//            Crypter.SetPassword(password);
//        }
//    }
//}
