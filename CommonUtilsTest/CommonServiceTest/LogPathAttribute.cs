using System;

namespace CommonService
{
    public class LogPathAttribute : Attribute
    {
        public LogPathAttribute(string logPath)
        {
            LogPath = logPath;
        }
        public string LogPath { get;  } 
        
    }
}
