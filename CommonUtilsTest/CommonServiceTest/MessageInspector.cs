using System;
using System.Globalization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace CommonService
{
    /// <summary>
    /// Расширение MessageInspector для передачи локали клиента серверу 
    /// </summary>
    public class CultureMessageInspector : IClientMessageInspector, IDispatchMessageInspector
    {
        private const string HeaderKey = "culture";

        #region IDispatchMessageInspector Members
         /// <summary>
        ///  Called after an inbound message has been received but before the message is dispatched to the intended operation. 
         /// </summary>
         /// <param name="request"></param>
         /// <param name="channel"></param>
         /// <param name="instanceContext"></param>
         /// <returns></returns>
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request,
            System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            int headerIndex = request.Headers.FindHeader(HeaderKey, string.Empty);
            if (headerIndex != -1)
            {
                var culture = new CultureInfo(request.Headers.GetHeader<String>(headerIndex));
                if (culture.Name!=Thread.CurrentThread.CurrentUICulture.Name)
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(request.Headers.GetHeader<String>(headerIndex));
            }
            return null;
        }
        /// <summary>
        ///  Called after the operation has returned but before the reply message is sent. 
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }

        #endregion

        #region IClientMessageInspector Members
        /// <summary>
        ///  Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application. 
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }
        /// <summary>
        ///  Enables inspection or modification of a message before a request message is sent to a service. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request,
            System.ServiceModel.IClientChannel channel)
        {
            request.Headers.Add(MessageHeader.CreateHeader(HeaderKey, string.Empty,
                Thread.CurrentThread.CurrentUICulture.Name));
            return null;
        }

        #endregion
    }
    /// <summary>
    ///  EndpointBehavior, подключающий CultureMessageInspector
    /// </summary>
    public class CultureBehaviour : IEndpointBehavior
    {
        #region IEndpointBehavior Members
        /// <summary>
        ///  AddBindingParameters
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }
        /// <summary>
        /// Implements a modification or extension of the client across an endpoint. 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            CultureMessageInspector inspector = new CultureMessageInspector();
            clientRuntime.MessageInspectors.Add(inspector);
        }
       /// <summary>
        ///  Implements a modification or extension of the service across an endpoint. 
       /// </summary>
       /// <param name="endpoint"></param>
       /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            CultureMessageInspector inspector = new CultureMessageInspector();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }
       /// <summary>
        /// Validate service endpoint
       /// </summary>
       /// <param name="endpoint"></param>
        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}