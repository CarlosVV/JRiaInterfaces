using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class AddressValidationSettings : ExtensibleObject
    {
        [DataMember]
        public bool OnWeb { get; set; }

        [DataMember]
        public bool FromOn { get; set; }

        [DataMember]
        public bool FromOnStores { get; set; }

        [DataMember]
        public bool FromOnAgents { get; set; }

        [DataMember]
        public bool FromNewCustomerOnly { get; set; }

        [DataMember]
        public bool FromOnlyWhenIdRequired { get; set; }

        [DataMember]
        public bool FromRejectInvalid { get; set; }

        [DataMember]
        public bool FromOnHoldInvalid { get; set; }
    }
}
