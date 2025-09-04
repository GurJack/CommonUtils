//using System;
//using System.Data;
//using Microsoft.Data.SqlClient;
//using System.IO;
//using CommonUtils.Extensions;
//using log4net.Appender;
//using log4net.Core;

//namespace CommonUtils.Loggers.Appenders
//{
//    public sealed class FastDbAppender : AppenderSkeleton, IAppender, IOptionHandler, IAppenderLogInfo
//    {
//        private SqlConnection _dbConnection;
//        private bool _activate=false;

//        //public string Name { get; set; }

//        public string ConnectionString { get; set; }

//        public int MonitorId { get; set; }
//        public SqlConnection DbConnection { get => _dbConnection; set => _dbConnection = value; }

//        public bool TryActivateOptions()
//        {
//            if (_activate)
//                return _activate;
//            try
//            {
//                _dbConnection = new SqlConnection(ConnectionString);
//                _dbConnection.Open();
//                _activate=true;
//            }
//            catch (Exception)
//            {
//                _activate = false;
//            }
//            return _activate;
//        }

//        public override void ActivateOptions()
//        {

//            TryActivateOptions();

//        }

//        public new void Close()
//        {
//            _dbConnection?.Close();
//            _activate = false;
//            base.Close();
//        }

//        protected override void Append(LoggingEvent loggingEvent)
//        {
//            throw new NotImplementedException();
//        }

//        private string _logPath = null;
//        public void SetLogPath(string logPath)
//        {
//            _logPath = logPath;
//        }
//    }
//}
