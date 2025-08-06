//using System;
//using System.Collections.Generic;
//using System.IO;
//using NLog;
//using NLog.Targets;
//using NLog.Targets.Wrappers;

//namespace CommonUtils.Loggers
//{
//    /// <summary>
//    /// Class for writing the log text base on <see cref="NLog.Logger"/>
//    /// </summary>
//    public sealed class NLogLogger : LoggerAbstract
//    {
//        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

//        /// <summary>
//        /// Default constructor.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        public NLogLogger(LoggingLevel level) : base(level)
//        {
//        }


//        /// <summary>
//        /// Clears the log information.
//        /// </summary>
//        public override void Clear()
//        {
//            //For NLog doesn't clear anything!
//            //var fileName = GetLogFileName("f_allf");
//            //if (FileProvider.ExistsFile(fileName))
//            //{
//            //    FileProvider.ClearFile(fileName);
//            //}
//        }

//        /// <summary>
//        /// Appends specified entry to the log information.
//        /// </summary>
//        /// <param name="entry">The entry for logging.</param>
//        public override void Append(LogEntry entry)
//        {
//            if (entry == null)
//            {
//                throw new ArgumentNullException(nameof(entry));
//            }

//            var text = $"[Version: {entry.Version}] - {entry.Message}";

//            switch (entry.Type)
//            {
//                case LogEntryType.Error:
//                    _logger.Error(text);
//                    break;
//                case LogEntryType.Warning:
//                    _logger.Warn(text);
//                    break;
//                case LogEntryType.UserAction:
//                    text = Logger.PrefixUserActionLog + " " + text;
//                    _logger.Info(text);
//                    break;
//                case LogEntryType.Trace:
//                    _logger.Trace(text);
//                    break;
//                default:
//                    _logger.Info(text);
//                    break;
//            }
//        }

//        /// <summary>
//        /// Gets the content of the log file as a log entry list.
//        /// </summary>
//        /// <returns>The log entry list.</returns>
//        public override List<LogEntry> Read()
//        {
//            throw new System.NotImplementedException();
//        }

//        /// <summary>
//        /// Gets the target's file name
//        /// </summary>
//        /// <param name="targetName"></param>
//        /// <returns></returns>
//        private string GetLogFileName(string targetName)
//        {
//            string fileName = null;

//            if (LogManager.Configuration != null && LogManager.Configuration.ConfiguredNamedTargets.Count != 0)
//            {
//                var target = LogManager.Configuration.FindTargetByName(targetName);
//                if (target == null)
//                {
//                    throw new Exception("Could not find target named: " + targetName);
//                }

//                FileTarget fileTarget = null;
//                var wrapperTarget = target as WrapperTargetBase;

//                // Unwrap the target if necessary.
//                if (wrapperTarget == null)
//                {
//                    fileTarget = target as FileTarget;
//                }
//                else
//                {
//                    fileTarget = wrapperTarget.WrappedTarget as FileTarget;
//                }

//                if (fileTarget == null)
//                {
//                    throw new Exception("Could not get a FileTarget from " + target.GetType());
//                }

//                var logEventInfo = new LogEventInfo { TimeStamp = DateTime.Now };
//                fileName = fileTarget.FileName.Render(logEventInfo);
//            }
//            else
//            {
//                throw new Exception("LogManager contains no Configuration or there are no named targets");
//            }

//            if (!File.Exists(fileName))
//            {
//                throw new Exception("File " + fileName + " does not exist");
//            }

//            return fileName;
//        }
//    }
//}