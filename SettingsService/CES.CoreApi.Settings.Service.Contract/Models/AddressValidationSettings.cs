using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class AddressValidationSettings : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public bool OnWeb { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool FromOn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool FromOnStores { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool FromOnAgents { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool FromNewCustomerOnly { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool FromOnlyWhenIdRequired { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool FromRejectInvalid { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool FromOnHoldInvalid { get; set; }
    }
}
