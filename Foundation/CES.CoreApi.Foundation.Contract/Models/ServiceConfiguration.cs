using System.Collections.Generic;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ServiceConfiguration
    {
        public List<EndpointConfiguration> Endpoints { get; set; }
        public List<BindingConfiguration> Bindings { get; set; }
        public BehaviorConfiguration Behavior { get; set; }
        public List<ServiceEndpoint> ConfiguredEndpoints { get; set; }
        public List<string> BaseAddresses { get; set; }
    }
}
