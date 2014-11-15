using System;
using System.ServiceModel.Channels;
using CES.CoreApi.Foundation.Contract.Enumerations;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ServiceEndpoint
    {
        public string EndpointName { get; set; }
        public Binding ServiceBinding { get; set; }
        public Uri Address { get; set; }
        public string Contract { get; set; }
        public bool IsHttps { get; set; }
        public ServiceSecurityMode SecurityMode { get; set; }
    }
}
