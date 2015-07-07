using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class CustomerConfidentialSettings : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public bool UseCustomerValidation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ValidationMethod { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal AmountThresholdForValidation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValidationLegalDepatmentEmailAddress { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ValidationMaxDobRetries { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool ValidationAlertOnFailedAttempts { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool ValidationForThisStateEnabled { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool ValidationForThisCountryEnabled { get; set; }
    }
}
