using System;
using System.ServiceModel.Channels;
using CES.CoreApi.Foundation.Contract.Enumerations;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ServiceEndpointConfiguration
    {
        public Uri Address { get; set; }

        public string Binding { get; set; }

        public string BindingConfigurationName { get; set; }

        public string EndpointName { get; set; }
        
        public string Contract { get; set; }

        public ServiceSecurityMode SecurityMode { get; set; }

        public Binding ConfiguredBinding { get; set; }
    }
}
