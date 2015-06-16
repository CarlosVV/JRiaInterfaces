using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class CustomerConfidentialSettingsModel
    {
        [CountrySettingCode("ComplUseCustomerValidation")]
        public bool UseCustomerValidation { get; set; }

        [CountrySettingCode("ComplValidationMethod")]
        public int ValidationMethod { get; set; }

        [CountrySettingCode("ComplAmountThresholdForValidation")]
        public decimal AmountThresholdForValidation { get; set; }

        [CountrySettingCode("ComplCustomerValidationLegalDeptEmailAddress")]
        public string ValidationLegalDepatmentEmailAddress { get; set; }

        [CountrySettingCode("ComplCustomerValidationMaximumOfRetries")]
        public int ValidationMaxDobRetries { get; set; }

        [CountrySettingCode("ComplCustomerValidationAlertWhenValidationFail")]
        public bool ValidationAlertOnFailedAttempts { get; set; }

        [CountrySettingCode("ComplUseCustomerValidationWhenSendFromThisState")]
        public bool ValidationForThisStateEnabled { get; set; }

        [CountrySettingCode("ComplUseCustomerValidationWhenSendToThisCountry")]
        public bool ValidationForThisCountryEnabled { get; set; }
    }
}
