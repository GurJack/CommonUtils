//using System;
//using System.Collections.Generic;
//using CommonUtils.Loggers;

//namespace CommonUtils.Event
//{
//    public class FormInfoEventArgs : EventArgs
//    {
//        public ReportInfo Report { get; private set; }



//        public FormInfoEventArgs(ReportInfo report)
//        {
//            Report = report;

//        }
//    }



//    public delegate void FormInfoEventHandler(FormInfoEventArgs e);

//    public static class Events
//    {
//        public static List<string> StatusTypeNames = new List<string>
//        {
//            "",
//            "Start",
//            "ConsoleInfo",
//            "Information",
//            "Warning",
//            "Error",
//            "Fatal",
//            "Stop"
//        };

//        public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
//        public static event ProgressEventHandler Progress;

//        public static void OnProgress(object sender, ProgressEventArgs e)
//        {
//            Progress?.Invoke(sender, e);
//        }

//        public static void OnProgress(ProgressEventArgs e)
//        {
//            OnProgress(null,e);
//        }

//        public static void OnProgress(int currPos, int maxPos, string operation, string operationSpec, StatusTypes status, string loggerName, Exception error)
//        {
//            OnProgress(null, new ProgressEventArgs(currPos,maxPos,operation,operationSpec,status, loggerName,error));
//        }

//        public delegate void LogEventHandler(object sender, LogEventArgs e);
//        public static event LogEventHandler Log;

//        public static void OnLog(object sender, LogEventArgs e)
//        {
//            Log?.Invoke(sender, e);
//        }

//        public static void OnLog(LogEventArgs e)
//        {
//            OnLog(null, e);
//        }

//        public static void OnLog(string operation, string report, StatusTypes status, LogUtils logger)
//        {
//            OnLog(null, new LogEventArgs(operation,report,status, logger));
//        }

//        public static event FormInfoEventHandler FormInfo;
//        public static void OnFormInfo(FormInfoEventArgs e)
//        {
//            FormInfo?.Invoke(e);
//        }
//        public static void OnFormInfo(ReportInfo report)
//        {
//            OnFormInfo(new FormInfoEventArgs(report));

//        }

//    }
//}
