using System;

namespace CommonUtils.Event
{
    public class ProgressEventArgs : EventArgs
    {
        public int CurrPos { get; private set; }
        public int MaxPos { get; private set; }
        public StatusTypes Status { get; private set; }
        public Exception Error { get; private set; }
        public string Operation { get; private set; }
        public string OperationSpec { get; private set; }
        public string LoggerName { get; private set; }
        
        public ProgressEventArgs(int currPos, int maxPos, string operation, string operationSpec, StatusTypes status, string loggerName, Exception error)
        {
            CurrPos = currPos;
            MaxPos = maxPos;
            Error = error;
            Status = status;
            Operation = operation;
            OperationSpec = operationSpec;
            LoggerName = loggerName;
            
        }
    }
}
