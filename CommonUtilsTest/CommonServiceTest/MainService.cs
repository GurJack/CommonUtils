using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace CommonService
{
    [ErrorHandlerBehavior(typeof(MainErrorHandler))]
    public class MainService
    {
        //private readonly Dictionary<string, ServiceLog> _loggers;
        //private string _logPath;
        //protected readonly WebSessionInfo SessionInfo;

        
        //public MainService()
        //{
        //    _logPath = null;
        //    SessionInfo = new WebSessionInfo();
        //    SetSessionInfo();
        //    _loggers =new Dictionary<string, ServiceLog>();
        //    if (Attribute.IsDefined(this.GetType(), typeof(LogPathAttribute)))
        //    {
        //        var attributeValue = Attribute.GetCustomAttribute(this.GetType(), typeof(LogPathAttribute)) as LogPathAttribute;

        //        _logPath = attributeValue == null
        //            ? null
        //            : (String.IsNullOrWhiteSpace(attributeValue.LogPath) ? null : attributeValue.LogPath.Trim());
        //        if (_logPath == null)
        //            return;
        //        if (OperationContext.Current != null)
        //        foreach (var item in OperationContext.Current.Host.ChannelDispatchers)
        //        {
        //            var chDisp = item as ChannelDispatcher;
        //            if (chDisp == null)
        //                continue;
        //            foreach (IErrorHandler chDispErrorHandler in chDisp.ErrorHandlers)
        //            {
        //                var handler = chDispErrorHandler as MainErrorHandler;
        //                handler?.CreateLog(_logPath, SessionInfo);
        //            }

        //        }
        //    }


        //}

        //public ServiceLog this[string index]
        //{
        //    get
        //    {
        //        return _loggers.ContainsKey(index) ? _loggers[index] : null;
        //    }
            
        //}

        //protected void SetSessionInfo()
        //{
        //    SessionInfo.ServiceName = OperationContext.Current?.Host.Description.ConfigurationName;
        //    SessionInfo.ServiceSession = OperationContext.Current?.SessionId;
        //    if (ServiceSecurityContext.Current == null || ServiceSecurityContext.Current.PrimaryIdentity == null)
        //        SessionInfo.UserName = "Неизвестный пользователь";
        //    else
        //    {
        //        SessionInfo.UserName = !ServiceSecurityContext.Current.PrimaryIdentity.IsAuthenticated
        //            ? "Неизвестный пользователь"
        //            : $"{ServiceSecurityContext.Current.PrimaryIdentity.Name}";
                
        //    }
        //}

        //protected void SetSessionInfo(string applicationName, string userName="")
        //{
        //    SessionInfo.ApplicationName = applicationName;
        //    if (String.IsNullOrWhiteSpace(userName))
        //        SessionInfo.UserName = userName;
        //    SessionInfo.ServiceSession = OperationContext.Current?.SessionId;
        //}
      
        //public ServiceLog GetLogger(string loggerName, string label, IEnumerable<LogParam> logParams)
        //{
        //    loggerName = $"{SessionInfo.ServiceName}.{loggerName}";
        //    ServiceLog result;
        //    if (_loggers.TryGetValue(loggerName, out result))
        //        result.Label = label;
        //    else
        //    {
        //        SetSessionInfo();
        //        result = new ServiceLog(_logPath, loggerName, label, SessionInfo,logParams);
        //        _loggers.Add(loggerName,result);
        //    }
        //    return result;
        //}

        //public ServiceLog GetNotInitLogger(string loggerName, string label, IEnumerable<LogParam> logParams)
        //{
        //    loggerName = $"{SessionInfo.ServiceName}.{loggerName}";
        //    ServiceLog result;
        //    if (_loggers.TryGetValue(loggerName, out result))
        //        throw new ApplicationException($"Логер {loggerName} уже существует");
            
        //        SetSessionInfo();
        //        result = ServiceLog.SetServiceLogConfig(_logPath, loggerName, label, SessionInfo, logParams);
        //        _loggers.Add(loggerName, result);
            
        //    return result;
        //}


    }
}