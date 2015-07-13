using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class CorporateSettingsModel
    {
        [CountrySetting("CorporateCustomerServiceNumber")]
        public string CustomerServiceNumber { get; set; }

        [CountrySetting("CorporateHelpDeskNumber")]
        public string HelpDeskNumber { get; set; }

        [CountrySetting("CorporateARNumber")]
        public string AccountReceivableNumber { get; set; }

        [CountrySetting("CorporateComplianceNumber")]
        public string ComplianceNumber { get; set; }

        [CountrySetting("CorporateAgentSupportNumber")]
        public string AgentSupportNumber { get; set; }

        [CountrySetting("CorporateEmailAR")]
        public string EmailAccountReceivable { get; set; }

        [CountrySetting("CorporateEmailAgentSupport")]
        public string EmailAgentSupport { get; set; }

        [CountrySetting("CorporateEmailCheckCashing")]
        public string EmailCheckCashing { get; set; }

        [CountrySetting("CorporateEmailCompliance")]
        public string EmailCompliance { get; set; }

        [CountrySetting("CorporateEmailCustomerService")]
        public string EmailCustomerService { get; set; }

        [CountrySetting("CorporateEmailWebSupport")]
        public string EmailWebSupport { get; set; }

        [CountrySetting("CorporateCustomerServiceFaxNumber")]
        public string CustomerServiceFaxNumber { get; set; }

        [CountrySetting("CorporateCustomerServiceRefundEmail")]
        public string CustomerServiceRefundEmail { get; set; }
    }
}
