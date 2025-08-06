using System;

namespace CommonUtils.Loggers
{
    /// <summary>
    /// The log single entry for reading and writing log information.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Constructor with log entry information.
        /// </summary>
        /// <param name="type">The log entry type.</param>
        /// <param name="message">The log text message.</param>
        /// <param name="date">The lod entry writing date/time.</param>
        /// <exception cref="ArgumentNullException">message is null.</exception>
        public LogEntry(LoggingLevel type, string message, DateTime date, Exception exception = null)
            : this(type, message, date, Information.UserName, Information.ProgramName, Information.ProductVersion,exception)
        {
        }

        /// <summary>
        /// Constructor with log entry information.
        /// </summary>
        /// <param name="type">The log entry type.</param>
        /// <param name="message">The log text message.</param>
        /// <param name="date">The lod entry writing date/time.</param>
        /// <exception cref="ArgumentNullException">message is null.</exception>
        public LogEntry(LoggingLevel type, string message, Exception exception = null)
            : this(type, message, DateTime.Now, Information.UserName, Information.ProgramName, Information.ProductVersion, exception)
        {
        }

        /// <summary>
        /// Constructor with log entry information.
        /// </summary>
        /// <param name="type">The log entry type.</param>
        /// <param name="message">The log text message.</param>
        /// <param name="date">The lod entry writing date/time.</param>
        /// <param name="version">The version.</param>
        /// <exception cref="ArgumentNullException">message is null.</exception>
        /// <exception cref="ArgumentNullException">version is null.</exception>
        public LogEntry(LoggingLevel type, string message, DateTime date, string userName, string applicationName, Version version, Exception exception)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));
            
            if (version == null)
                throw new ArgumentNullException(nameof(version));
            

            Level = type;
            Message = message;
            Date = date;
            Version = version;
            UserName = userName;
            ApplicationName = applicationName;
            Error = exception;
        }

        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public Exception Error { get; set; }
        /// <summary>
        /// Gets the log entry type.
        /// </summary>
        public LoggingLevel Level { get; set; }

        /// <summary>
        /// Gets the log text message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets the log entry writing date/time.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the program version.
        /// </summary>
        public Version Version { get; set; }
        public string Metod { get; set; }
        public string Operation { get; set; }
    }
}