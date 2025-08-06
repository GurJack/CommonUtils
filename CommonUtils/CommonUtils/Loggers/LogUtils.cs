using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtils.Event;
using CommonUtils.Extensions;
using CommonUtils.Helpers;
using CommonUtils.Loggers.Appenders;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace CommonUtils.Loggers
{
    public class LogUtils 
    {
        
        private readonly ILogger _log;
        private readonly Hierarchy _repository;
        private bool _isConfigure;
        private bool _inWork;
        private readonly Dictionary<string, string> _params=new Dictionary<string, string>();
        public HashSet<AppenderCarrier> AppenderList { get; }
        public Dictionary<LoggingLevel, Level> Levels;

        //private FastDbAppender _monitorAppender;
        public Dictionary<LoggingLevel, bool> LevelAction;

        private void SetLogParams(IEnumerable<LogParam> logParams)
        {
            if (logParams == null)
                return;
            foreach (var logParam in logParams)
            {
                if (_params.ContainsKey(logParam.Name))
                    _params[logParam.Name] = logParam.Value;
                else
                    _params.Add(logParam.Name, logParam.Value);

            }
        }

        public string UserName
        {
            get => ThreadContext.Properties["User"] as string;
            set => ThreadContext.Properties["User"] = value;
        }
        

        public string Operation
        {
            get => ThreadContext.Properties["Operation"] as string;
            set => ThreadContext.Properties["Operation"] = value;
        }

        public string Metod
        {
            get => ThreadContext.Properties["Metod"] as string;
            set => ThreadContext.Properties["Metod"] = value;
        }

        public string ApplicatinName {
            get => ThreadContext.Properties["ApplicationName"] as string;
            private set => ThreadContext.Properties["ApplicationName"] = value;
        }

        public LogUtils(Type logType)
        {
            if (logType == null)
                throw new ArgumentNullException(nameof(logType), "Не задано название логера");
            _repository = (Hierarchy)LogManager.CreateRepository(logType.FullName);
            var levels = Enum.GetValues(typeof(LoggingLevel)).Cast<LoggingLevel>().ToList();
            Levels = new Dictionary<LoggingLevel, Level>();
            foreach (var pos in _repository.LevelMap.AllLevels)
            {
                var level = (LoggingLevel)pos.Value;
                if (levels.All(p => p != level))
                    continue;
                Levels.Add((LoggingLevel)pos.Value, pos);
                levels.Remove((LoggingLevel)pos.Value);
            }
            foreach (var pos in levels)
            {
                var newLevel = new Level((int)pos, pos.ToString());
                _repository.LevelMap.Add(newLevel);
                Levels.Add(pos, newLevel);
            }
            LevelAction = Enum.GetValues(typeof(LoggingLevel)).Cast<LoggingLevel>().ToDictionary(k => k, t => false);
            _log = LoggerManager.GetLogger(logType.FullName, logType.FullName);
            UserName = Information.UserName;
            ApplicatinName = Information.ProgramName;
            AppenderList = new HashSet<AppenderCarrier>();
            //Configure = new LogConfigure();
            _isConfigure = false;
            _inWork = false;

            ClearLogInfo();
            //_mailBody = new StringBuilder();
            //_currTime = DateTime.Now;
        }



        private ILogger Log
        {
            get
            {
                if (!_isConfigure)
                    throw new ApplicationException("Логгер не сконфигурирован.");
                if (!_inWork)
                    Start();
                return _log;
            }
        }

        public void InitLogger()
        {
            ConfigureRunTime();
            _isConfigure = true;
        }

        private void ConfigureRunTime()
        {

            var appList = new List<IAppender>();
            var appenders = _repository.GetAppenders();
            if (!AppenderList.Any())
                AppenderList.Add(new ConfigureFile());
            foreach (var logTypese in AppenderList)
            {
                if (logTypese.Active && logTypese.Appender != null)
                    continue;
                logTypese.InitAppender();
                if (appenders.FirstOrDefault(p => p == logTypese.Appender) == null)
                    appList.Add(logTypese.Appender);
            }
            if (appList.Any())
                BasicConfigurator.Configure(_repository, appList.ToArray());

        }


        internal void Close()
        {
            if (_inWork)
                Stop();
            _repository.Shutdown();
        }

        private void ClearLogInfo()
        {
            foreach (var action in Enum.GetValues(typeof(LoggingLevel)).Cast<LoggingLevel>())
                LevelAction[action] = false;
            
        }

        #region WriteLog

        private void Start()
        {
            if (_inWork)
                return;
            _inWork = true;
            WriteLog(new LogEntry(LoggingLevel.LogInfo, "Start"));
        }

        private void Stop()
        {
            WriteLog(new LogEntry(LoggingLevel.LogInfo,"Stop"));
            ClearAppenders();
            _inWork = false;
            
        }

        public void ClearAppenders()
        {
            foreach (var appenderCarrier in AppenderList)
                appenderCarrier.Appender.Close();
            
        }

        public void WriteLog(LogEntry log)
        {
            Metod = log.Metod;
            Operation = log.Operation;
            LevelAction[log.Level] = true;
            Log.Log(this.GetType(),Levels[log.Level], log.Message, log.Error);
        }

        public void WriteLog(string operation, object operationMessage, Exception exception,
            LoggingLevel entityType, string metodName)
        {
            string message = operationMessage?.ToString() ?? "Объект operationMessage = null";
           WriteLog(new LogEntry(entityType,message,exception){Operation = operation,Metod = metodName});
        }

        //public void ConsoleInfo(object operationMessage, string metodName = "")
        //{
        //    WriteLog(0,0,"",operationMessage,null,StatusTypes.ConsoleInfo,metodName);
        //}

        //public void ConsoleInfo(object operationMessage, Exception exception, string metodName = "")
        //{
        //    WriteLog(0, 0, "", operationMessage, exception, StatusTypes.ConsoleInfo,metodName);
        //}

        //public void ConsoleInfo(int currPos, int maxPos, string operation, object operationMessage, string metodName = "")
        //{
        //    WriteLog(currPos, maxPos, operation, operationMessage, null, StatusTypes.ConsoleInfo,metodName);
        //}

        //public void ConsoleInfo(int currPos, int maxPos, string operation, object operationMessage, Exception exception, string metodName = "")
        //{
        //    WriteLog(currPos, maxPos, operation, operationMessage, exception, StatusTypes.ConsoleInfo,metodName);
        //}
        //public void Info(object operationMessage, string metodName = "")
        //{
        //    WriteLog(0, 0, "", operationMessage, null, StatusTypes.Information,metodName);
        //}

        public void Info(object operationMessage, Exception exception, string metodName = "")
        {
            WriteLog("", operationMessage, exception, LoggingLevel.Information, metodName);
        }

        public void Info(string operation, object operationMessage, string metodName = "")
        {
            WriteLog(operation, operationMessage, null, LoggingLevel.Information, metodName);
        }

        public void Info(string operation, object operationMessage, Exception exception, string metodName = "")
        {
            WriteLog(operation, operationMessage, exception, LoggingLevel.Information, metodName);
        }

        public void Warn(object operationMessage, Exception exception, string metodName = "")
        {
            WriteLog("", operationMessage, exception, LoggingLevel.Warning, metodName);
        }

        public void Warn(string operation, object operationMessage, string metodName = "")
        {
            WriteLog(operation, operationMessage, null, LoggingLevel.Warning, metodName);
        }

        public void Warn(string operation, object operationMessage, Exception exception, string metodName = "")
        {
            WriteLog(operation, operationMessage, exception, LoggingLevel.Warning, metodName);
        }
        public void Error(object operationMessage, string metodName = "")
        {
            WriteLog("", operationMessage, null, LoggingLevel.Error, metodName);
        }

        public void Error(object operationMessage, Exception exception, string metodName = "")
        {
            WriteLog("", operationMessage, exception, LoggingLevel.Error, metodName);
        }

        public void Error(string operation, object operationMessage, string metodName = "")
        {
            WriteLog(operation, operationMessage, null, LoggingLevel.Error, metodName);
        }

        public void Error(string operation, object operationMessage, Exception exception, string metodName = "")
        {
            WriteLog(operation, operationMessage, exception, LoggingLevel.Error, metodName);
        }

        public void UserAction(object operationMessage, Exception exception, string metodName = "")
        {
            WriteLog("", operationMessage, exception, LoggingLevel.UserAction, metodName);
        }

        #endregion

        


    }
}
