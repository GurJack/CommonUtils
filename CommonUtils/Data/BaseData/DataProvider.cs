using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommonUtils.Loggers;
using CommonUtils.Settings;

namespace BaseData
{
    public abstract class DataProvider
    {
        private LogUtils _logger;
        protected LogUtils Logger => _logger;
        public bool Connected { get; protected set; }
        //public string DataBaseName { get; protected set; }
        //public string DataBasePath { get; protected set; }
        //public string ServerName { get; protected set; }
        public DataProvider(LogUtils logger)
        {
            _logger = logger;
        }

        public string ProviderName { get; protected set; }

        public void Connect(Dictionary<string, string> connectionParams)
        {
            Connected = false;
            //var param=new Dictionary<string, string>();
            //var comandList = connectionString.Split(new[] {";"}, StringSplitOptions.None);
            //foreach (string command in comandList)
            //{
            //    var posList = command.Split(new[] {";"}, StringSplitOptions.None);
            //    if(posList.Length != 2)
            //        continue;
            //    try
            //    {
            //        param.Add(posList[0].Trim().ToLower(), posList[1].Trim());

            //    }
            //    catch (Exception e)
            //    {
            //        throw new ApplicationException($"Дублируется команда: {posList[0].Trim()}");
            //    }
                
            //}

            //foreach (var command in param)
            //{
            //    param[command.Key] = command.Value
            //        .Replace("%AppPath%", Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))
            //        .Replace("%AppName%", AppDomain.CurrentDomain.FriendlyName);
            //    if (command.Key == "database path")
            //        DataBasePath = param[command.Key];
            //    else if (command.Key == "data source")
            //        ServerName = param[command.Key];
            //    else if (command.Key == "initial catalog")
            //        DataBaseName = param[command.Key];
                
            //}

            //if (param.ContainsKey("database path") && !File.Exists(param["database path"]))
            //{
            //    Logger.Info("Подключение к базе данных",$"Создание базы данных: {param["database path"]}",nameof(Connect));
            //    CreateDataBase(param["database path"]);
            //}
            //Logger.Info("Подключение к базе данных", $"Подключение к базе данных", nameof(Connect));
            ConnectToDataBase(connectionParams);

        }

        protected abstract void ConnectToDataBase(Dictionary<string, string> connectionParams);

        protected abstract void CreateDataBase(string dataBasePath);

        public abstract List<ParamItem> GetParamList();
    }
}
