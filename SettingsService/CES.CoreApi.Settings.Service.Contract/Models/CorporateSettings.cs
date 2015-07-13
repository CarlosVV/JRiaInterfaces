using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class CorporateSettings : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string CustomerServiceNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string HelpDeskNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AccountReceivableNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ComplianceNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AgentSupportNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmailAccountReceivable { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmailAgentSupport { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmailCheckCashing { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmailCompliance { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmailCustomerService { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmailWebSupport { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CustomerServiceFaxNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CustomerServiceRefundEmail { get; set; }
    }
}