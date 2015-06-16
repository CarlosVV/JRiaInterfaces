using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class CorporateSettings : ExtensibleObject
    {
        [DataMember]
        public string CustomerServiceNumber { get; set; }

        [DataMember]
        public string HelpDeskNumber { get; set; }

        [DataMember]
        public string AccountReceivableNumber { get; set; }

        [DataMember]
        public string ComplianceNumber { get; set; }

        [DataMember]
        public string AgentSupportNumber { get; set; }

        [DataMember]
        public string EmailAccountReceivable { get; set; }

        [DataMember]
        public string EmailAgentSupport { get; set; }

        [DataMember]
        public string EmailCheckCashing { get; set; }

        [DataMember]
        public string EmailCompliance { get; set; }

        [DataMember]
        public string EmailCustomerService { get; set; }

        [DataMember]
        public string EmailWebSupport { get; set; }

        [DataMember]
        public string CustomerServiceFaxNumber { get; set; }

        [DataMember]
        public string CustomerServiceRefundEmail { get; set; }
    }
}