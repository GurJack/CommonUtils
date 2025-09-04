//using System;
//using System.Collections.Generic;
//using System.Configuration.Provider;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using CommonUtils.Loggers;
//using log4net.Repository.Hierarchy;

//namespace BaseData
//{
//    public static class DataController
//    {
//        private static Dictionary<string, DataProvider> _providers;
//        private static bool _isInit = false;
//        private static string _programPath;
//        private static LogUtils _logger;

//        public static void Init(string programPath,LogUtils loger)
//        {
//            _programPath = programPath;
//            _logger = loger;
//            _isInit = true;
//            FindAllDataProviders();
//        }
//        public static void FindAllDataProviders()
//        {
//            if (!_isInit)
//                throw new ApplicationException($"{nameof(DataController)} не инициализирован.");
//            _providers = new Dictionary<string, DataProvider>();
//            string[] providers =Directory.GetFiles(_programPath, "*Data.dll");
//            DataProvider result = null;
//            foreach (string provider in providers) 
//            {
//                Assembly asm;
//                try
//                {
//                    asm = Assembly.LoadFrom(provider);
//                }
//                catch (Exception e)
//                {
//                    continue;
//                }
//                var types = asm.GetTypes();
//                result = null;
//                foreach (var type in types) 
//                {
//                    if (type.BaseType == typeof(DataProvider))
//                    {
//                        try
//                        {
//                            result = (DataProvider)Activator.CreateInstance(type, _logger);
//                            _providers.Add(result.ProviderName, result);
                            
//                        }
//                        catch (Exception)
//                        {

                            
//                        }
                        
                        
//                    }
                    
//                }

                
//            }

//        }
//        /// <summary>
//        /// Инициализирует DataProvider
//        /// </summary>
//        /// <param name="nameProvider"></param>
//        /// <param name="connectionString">
//        /// Параметр содержит всю необходимую информацию для подключения к БД. Параметры отделяются друг от друга точкой с запятой (;),
//        /// название от значения знаком равно (=).
//        /// В значениях можно использовать следующие переменные:
//        /// %AppPath% - путь к исполняемому файлу
//        /// %AppName% - название исполняемого файла без расширения
//        /// Параметр "DataBase Path" 
//        /// </param>
//        /// <returns></returns>
//        /// <exception cref="ApplicationException"></exception>
//        public static DataProvider GetProvider(string nameProvider, Dictionary<string, string> connectionParams)
//        {
//            if (!_isInit)
//                throw new ApplicationException($"{nameof(DataController)} не инициализирован.");
//            if (String.IsNullOrWhiteSpace(nameProvider))
//                throw new ApplicationException("Не указан провайдер.");
//            if (_providers == null || !_providers.TryGetValue(nameProvider, out var result))
//                throw new ApplicationException($"Не найден провайдер базы данных: '{nameProvider}'.");
//            result.Connect(connectionParams);
//            return result;
//        }

//        public static List<string> GetProvidersList()
//        {
//            if (!_isInit)
//                throw new ApplicationException($"{nameof(DataController)} не инициализирован.");
//            return _providers.Keys.ToList();
//        }
//    }
//}
