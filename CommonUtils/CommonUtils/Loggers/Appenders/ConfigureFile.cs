//using System;
//using System.IO;
//using log4net.Appender;
//using log4net.Layout;

//namespace CommonUtils.Loggers.Appenders
//{
//    public class ConfigureFile:AppenderCarrier
//    {
//        private RollingFileAppender _app;
//        public string FileName { get=> _app.File;
//            set
//            {
//                _app.File = value;
//                Active = false;
//            }
//        }

//        public long MaxFileSize
//        {
//            get => _app.MaxFileSize;
//            set
//            {
//                _app.MaxFileSize = value;
//                Active = false;
//            }
//        }

//        public bool AppendToFile
//        {
//            get => _app.AppendToFile;
//            set
//            {
//                _app.AppendToFile = value;
//                Active = false;
//            }
//        }

//        public ConfigureFile() : base(new RollingFileAppender
//        {
//            RollingStyle = RollingFileAppender.RollingMode.Size,
//            MaxSizeRollBackups = 10,
//            LockingModel = new FileAppender.MinimalLock(),
//            StaticLogFileName = true,
//        })
//        {
//            _app = (RollingFileAppender) Appender;
//            var fi = AppDomain.CurrentDomain.FriendlyName;
//            FileName = $@".\AppData\Logs\{Path.GetFileNameWithoutExtension(fi)}.log";
//            AppendToFile = true;
//            MaxFileSize = 20000000;

//            ((RollingFileAppender)Appender).ActivateOptions();
//        }

        
//    }
//}
