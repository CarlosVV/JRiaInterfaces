using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class CustomerConfidentialSettingsModel
    {
        [CountrySetting("ComplUseCustomerValidation")]
        public bool UseCustomerValidation { get; set; }

        [CountrySetting("ComplValidationMethod")]
        public int ValidationMethod { get; set; }

        [CountrySetting("ComplAmountThresholdForValidation")]
        public decimal AmountThresholdForValidation { get; set; }

        [CountrySetting("ComplCustomerValidationLegalDeptEmailAddress")]
        public string ValidationLegalDepatmentEmailAddress { get; set; }

        [CountrySetting("ComplCustomerValidationMaximumOfRetries")]
        public int ValidationMaxDobRetries { get; set; }

        [CountrySetting("ComplCustomerValidationAlertWhenValidationFail")]
        public bool ValidationAlertOnFailedAttempts { get; set; }

        [CountrySetting("ComplUseCustomerValidationWhenSendFromThisState")]
        public bool ValidationForThisStateEnabled { get; set; }

        [CountrySetting("ComplUseCustomerValidationWhenSendToThisCountry")]
        public bool ValidationForThisCountryEnabled { get; set; }
    }
}
