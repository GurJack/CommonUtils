using CommonUtils;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using IErrorHandler = System.ServiceModel.Dispatcher.IErrorHandler;

namespace CommonService
{
    public class MainErrorHandler : IErrorHandler
    {
        //private ServiceLog _logger=null;

        public MainErrorHandler()
        {
            
            
        }

        public void CreateLog(string logPath, WebSessionInfo sessionInfo)
        {
            //_logger = new ServiceLog(logPath, $"{sessionInfo.ServiceName}Error", "", sessionInfo, null);
        }
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
                return;
            var fit = new FaultException(error.FullMessage(), new FaultCode("InnerServiceException"));
            var msg = fit.CreateMessageFault();
            fault = Message.CreateMessage(version, msg, "null");
        }

        public bool HandleError(Exception error)
        {
            //_logger?.Error(error.FullMessage());
            return true;
        }
    }
}