using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class Geolocation : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public double Longitude { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Latitude { get; set; }
    }
}