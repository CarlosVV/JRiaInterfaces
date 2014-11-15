using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IServiceExceptionHandler
    {
        /// <summary>
        /// Provides a fault. The MessageFault fault parameter can be replaced or set to null to suppress reporting a fault. 
        /// </summary>
        /// <param name="error">The Exception object thrown in the course of the service operation.</param>
        /// <param name="version">The SOAP version of the message</param>
        /// <param name="fault">The System.ServiceModel.Channels.Message object that is returned to the client, or service, in the duplex case.</param>
        void ProvideFault(Exception error, MessageVersion version, ref Message fault);

        /// <summary>
        /// Logs an error, then allow the error to be handled as usual.
        /// </summary>
        /// <param name="exception">The exception thrown during processing</param>
        /// <returns></returns>
        bool HandleError(Exception exception);

        void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase);

        void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters);

        void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase);
    }
}