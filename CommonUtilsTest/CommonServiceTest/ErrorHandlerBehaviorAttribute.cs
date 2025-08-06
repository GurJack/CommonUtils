using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace CommonService
{
    public class ErrorHandlerBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _handlerType;

        public ErrorHandlerBehaviorAttribute(Type handlerType)
        {
            _handlerType = handlerType;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var handler = (IErrorHandler)Activator.CreateInstance(_handlerType);
            foreach (var item in serviceHostBase.ChannelDispatchers)
            {
                var chDisp = item as ChannelDispatcher;
                chDisp?.ErrorHandlers.Add(handler);
            }
        }
    }
}