using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class CorporateSettingsModel
    {
        [CountrySettingCode("CorporateCustomerServiceNumber")]
        public string CustomerServiceNumber { get; set; }

        [CountrySettingCode("CorporateHelpDeskNumber")]
        public string HelpDeskNumber { get; set; }

        [CountrySettingCode("CorporateARNumber")]
        public string AccountReceivableNumber { get; set; }

        [CountrySettingCode("CorporateComplianceNumber")]
        public string ComplianceNumber { get; set; }

        [CountrySettingCode("CorporateAgentSupportNumber")]
        public string AgentSupportNumber { get; set; }

        [CountrySettingCode("CorporateEmailAR")]
        public string EmailAccountReceivable { get; set; }

        [CountrySettingCode("CorporateEmailAgentSupport")]
        public string EmailAgentSupport { get; set; }

        [CountrySettingCode("CorporateEmailCheckCashing")]
        public string EmailCheckCashing { get; set; }

        [CountrySettingCode("CorporateEmailCompliance")]
        public string EmailCompliance { get; set; }

        [CountrySettingCode("CorporateEmailCustomerService")]
        public string EmailCustomerService { get; set; }

        [CountrySettingCode("CorporateEmailWebSupport")]
        public string EmailWebSupport { get; set; }

        [CountrySettingCode("CorporateCustomerServiceFaxNumber")]
        public string CustomerServiceFaxNumber { get; set; }

        [CountrySettingCode("CorporateCustomerServiceRefundEmail")]
        public string CustomerServiceRefundEmail { get; set; }
    }
}
