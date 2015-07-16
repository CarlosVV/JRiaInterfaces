using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class Geolocation : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public double Longitude { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Latitude { get; set; }
    }
}