using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class CustomerConfidentialSettings : ExtensibleObject
    {
        [DataMember]
        public bool UseCustomerValidation { get; set; }

        [DataMember]
        public int ValidationMethod { get; set; }

        [DataMember]
        public decimal AmountThresholdForValidation { get; set; }

        [DataMember]
        public string ValidationLegalDepatmentEmailAddress { get; set; }

        [DataMember]
        public int ValidationMaxDobRetries { get; set; }

        [DataMember]
        public bool ValidationAlertOnFailedAttempts { get; set; }

        [DataMember]
        public bool ValidationForThisStateEnabled { get; set; }

        [DataMember]
        public bool ValidationForThisCountryEnabled { get; set; }
    }
}
