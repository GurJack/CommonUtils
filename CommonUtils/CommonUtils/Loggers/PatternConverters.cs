using System;
using System.IO;
using log4net;
using log4net.Core;
using log4net.Layout.Pattern;

namespace CommonUtils.Loggers
{
    public class MachinePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            writer.Write(Environment.MachineName);
        }
    }

    public abstract class PropertyPatternConverter : PatternLayoutConverter
    {
        protected void Convert(TextWriter writer, LoggingEvent loggingEvent, string propertyName)
        {

            writer.Write(ThreadContext.Properties[propertyName]);
        }
        
    }

    public class OperationPatternConverter : PropertyPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            
            Convert(writer,loggingEvent, "Operation");
        }
    }


    public class UserNamePatternConverter : PropertyPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {

            Convert(writer, loggingEvent, "User");
        }
    }

    public class MetodPatternConverter : PropertyPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {

            Convert(writer, loggingEvent, "Metod");
        }
    }

    public class ApplicationNamePatternConverter : PropertyPatternConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {

            Convert(writer, loggingEvent, "ApplicationName");
        }
    }

    //public class LoggerNamePatternConverter : PropertyPatternConverter
    //{
    //    protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
    //    {

    //        Convert(writer, loggingEvent, "LoggerName");
    //    }
    //}
}