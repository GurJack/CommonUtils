using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using CommonUtils.FileProviders;
using CommonUtils.Helpers;

namespace CommonUtils.Loggers
{
    /// <summary>
    /// Class for writing the log text to the file.
    /// Default log file is Information.ProgramDataPath + "/" + Information.ProgramName + ".log".
    /// </summary>
    public class FileLogger : LoggerAbstract
    {
        private readonly string _fileName = null;

        /// <summary>
        /// Default constructor.
        /// <param name="level">The log level.</param>
        /// </summary>
        public FileLogger(LoggingLevel level)
            : this(Path.Combine(Information.ProgramDataPath, Information.ProgramName + ".log"), level)
        {
        }

        /// <summary>
        /// Constructor with the log file name.
        /// </summary>
        /// <param name="fileName">Specified file name.</param>
        /// <param name="level">The log level.</param>
        public FileLogger(string fileName, LoggingLevel level) : base(level)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            _fileName = fileName.Replace(Path.DirectorySeparatorChar, UriHelper.PathSeparator);
        }


        /// <summary>
        /// Gets the log file name.
        /// </summary>
        public string FileName => _fileName;


        /// <summary>
        /// Clears the log file.
        /// </summary>
        public override void Clear()
        {
            FileProvider.ClearFile(_fileName);
        }

        /// <summary>
        /// Appends specified entry to the log information.
        /// </summary>
        /// <param name="entry">The entry for logging.</param>
        public override void Append(LogEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            var text = $"[Version: {entry.Version}] - [Date: {XmlConvert.ToString(entry.Date, XmlDateTimeSerializationMode.RoundtripKind)}] - [Type: {entry.Type}] - {entry.Message}"
                       + Environment.NewLine;

            FileProvider.AppendText(_fileName, text);
        }

        /// <summary>
        /// Gets the content of the log file as a log entry list.
        /// </summary>
        /// <returns>The log entry list.</returns>
        public override List<LogEntry> Read()
        {
            throw new System.NotImplementedException();
        }
    }
}