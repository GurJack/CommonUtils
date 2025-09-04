//using System;
//using System.Collections.Generic;
//using System.Linq;
//using CommonUtils.Extensions;
//using log4net;
//using log4net.Core;
//using log4net.Repository.Hierarchy;

//namespace CommonUtils.Loggers
//{
//    public static class Logger
//    {
//        private static readonly Dictionary<Type, LogUtils> _loggerList = new Dictionary<Type, LogUtils>();
//        public static string MainLayoutPattern = "[%date{dd.MM.yyyy HH:mm:ss.ffff}] [%thread] [%MachineName] [%User] [%ApplicationName] [%Metod] [%Operation] [%-5level] [%logger] [%message] [%exception]%newline";
        
//        static Logger()
//        {
//            ActiveLogger = null;
//        }

//        public static LogUtils GetLogger<T>(Type logType) where  T:LogUtils
//        {
//            return _loggerList.GetValueOrDefault(logType,null) ?? AddLogger<T>(logType);
//        }

//        public static LogUtils AddLogger<T>(Type logType) where T : LogUtils
//        {
//            var result = ClassFactory.CreateInstance<T>(logType);
//            _loggerList.Add(logType, result);
//            if (ActiveLogger == null)
//                ActiveLogger = result;
//            return result;
//        }

//        public static void RemoveLogger(Type logType)
//        {
//            var res = _loggerList.GetValueOrDefault(logType,null);
//            if (res == null)
//                return;
//            res.Close();

//            _loggerList.Remove(logType);
//        }
//        public static LogUtils ActiveLogger { get; set; }
//    }
//}