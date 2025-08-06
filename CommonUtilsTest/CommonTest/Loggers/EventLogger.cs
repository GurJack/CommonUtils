//using System;
//using System.Collections.Generic;
//using System.Diagnostics;

//namespace CommonUtils.Loggers
//{
//    /// <summary>
//    /// Class for writing the log text to the event log.
//    /// Default event log name is "Application".
//    /// Base on <see cref="System.Diagnostics.EventLog"/>
//    /// </summary>
//    public class EventLogger : LoggerAbstract,
//        IDisposable
//    {
//        private EventLog _log;

//        /// <summary>
//        /// Default log name.
//        /// </summary>
//        public const string DefaultLogName = "Application";

//        /// <summary>
//        /// Default constructor.
//        /// </summary>
//        /// <param name="level">The log level.</param>
//        public EventLogger(LoggingLevel level)
//            : this(DefaultLogName, level)
//        {
//        }

//        /// <summary>
//        /// Constructor with an event log name.
//        /// </summary>
//        /// <param name="logName">The specified event log name.</param>
//        /// <param name="level">The log level.</param>
//        public EventLogger(string logName, LoggingLevel level)
//            : this(logName, Information.CompanyName + " " + Information.ProductName, level)
//        {
//        }

//        /// <summary>
//        /// Constructor with an event log name and application name.
//        /// </summary>
//        /// <param name="logName">The specified event log name.</param>
//        /// <param name="applicationName">The specified event log application name.</param>
//        /// <param name="level">The log level.</param>
//        public EventLogger(string logName, string applicationName, LoggingLevel level)
//            : this(logName, applicationName, ".", level)
//        {
//        }

//        /// <summary>
//        /// Constructor with an event log name and application name.
//        /// </summary>
//        /// <param name="logName">The specified event log name.</param>
//        /// <param name="applicationName">The specified event log application name.</param>
//        /// <param name="computerName">The specified computer name.</param>
//        /// <param name="level">The log level.</param>
//        public EventLogger(string logName, string applicationName, string computerName, LoggingLevel level) : base(level)
//        {
//            if (logName == null)
//            {
//                throw new ArgumentNullException(nameof(logName));
//            }
//            if (applicationName == null)
//            {
//                throw new ArgumentNullException(nameof(applicationName));
//            }
//            if (computerName == null)
//            {
//                throw new ArgumentNullException(nameof(computerName));
//            }

//            if (!logName.Equals(EventLog.LogNameFromSourceName(applicationName, computerName)))
//            {
//                if (EventLog.SourceExists(applicationName, computerName))
//                {
//                    EventLog.DeleteEventSource(applicationName, computerName);
//                }
//                var data = new EventSourceCreationData(applicationName, logName) {MachineName = computerName};
//                EventLog.CreateEventSource(data);
//            }

//            _log = new EventLog(logName, computerName, applicationName);
//        }

//        /// <summary>
//        /// Disposing all resources.
//        /// </summary>
//        ~EventLogger()
//        {
//            Dispose();
//        }


//        /// <summary>
//        /// Gets the event log name.
//        /// </summary>
//        public string LogName => Log.Log;

//        /// <summary>
//        /// Gets the event log application name.
//        /// </summary>
//        public string ApplicationName => Log.Source;

//        /// <summary>
//        /// Gets the event log computer name.
//        /// </summary>
//        public string ComputerName => Log.MachineName;


//        /// <summary>
//        /// Gets the internal event log object.
//        /// </summary>
//        protected virtual EventLog Log => _log;


//        /// <summary>
//        /// Clears the log information.
//        /// </summary>
//        public override void Clear()
//        {
//            Log.Clear();
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

//            var entryType = EventLogEntryType.Information;
//            if (entry.Type == LogEntryType.Error)
//            {
//                entryType = EventLogEntryType.Error;
//            }
//            else if (entry.Type == LogEntryType.Warning)
//            {
//                entryType = EventLogEntryType.Warning;
//            }

//            var text = entry.Message +
//                       Environment.NewLine + Environment.NewLine +
//                       "UserName: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + ".";

//            Log.WriteEntry(text, entryType);
//        }

//        /// <summary>
//        /// Gets the content of the log file as a log entry list.
//        /// </summary>
//        /// <returns>The log entry list.</returns>
//        public override List<LogEntry> Read()
//        {
//            var list = new List<LogEntry>();

//            var entries = Log.Entries;
//            foreach (EventLogEntry entry in entries)
//            {
//                if (entry.Source != _log.Source)
//                {
//                    continue;
//                }

//                var entryType = LogEntryType.Information;
//                if (entry.EntryType == EventLogEntryType.Error)
//                {
//                    entryType = LogEntryType.Error;
//                }
//                else if (entry.EntryType == EventLogEntryType.Warning)
//                {
//                    entryType = LogEntryType.Warning;
//                }

//                list.Add(new LogEntry(entryType, entry.Message, entry.TimeWritten));
//            }

//            list.Reverse();

//            return list;
//        }


//        /// <summary>
//        /// Performs application-defined tasks associated with freeing,
//        /// releasing, or resetting unmanaged resources.
//        /// </summary>
//        public void Dispose()
//        {
//            if (_log != null)
//            {
//                _log.Dispose();
//                _log = null;
//            }
//        }
//    }
//}