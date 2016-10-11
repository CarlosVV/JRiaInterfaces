using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Utilities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageInspectorBehavior : Attribute, IEndpointBehavior
    {
        public MessageInspectorBehavior() : base()
        {

        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            IClientMessageInspector inspector = null;
            inspector = new MessageInspector();
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            //no es necesaria su implementación en cliente.
       
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            //no es necesaria su implementación en cliente.
        }

        #endregion
    }
}