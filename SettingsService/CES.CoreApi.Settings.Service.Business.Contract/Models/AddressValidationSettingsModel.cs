using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class AddressValidationSettingsModel
    {
        [CountrySetting("AddressValidationOn_Web")]
        public bool OnWeb { get; set; }

        [CountrySetting("AddressValidation_From_On")]
        public bool FromOn { get; set; }

        [CountrySetting("AddressValidation_From_On_Stores")]
        public bool FromOnStores { get; set; }

        [CountrySetting("AddressValidation_From_On_Agents")]
        public bool FromOnAgents { get; set; }

        [CountrySetting("AddressValidation_From_NewCustsOnly")]
        public bool FromNewCustomerOnly { get; set; }

        [CountrySetting("AddressValidation_From_OnlyWhenIDRequired")]
        public bool FromOnlyWhenIdRequired { get; set; }

        [CountrySetting("AddressValidation_From_RejectInvalid")]
        public bool FromRejectInvalid { get; set; }

        [CountrySetting("AddressValidation_From_OnHoldInvalid")]
        public bool FromOnHoldInvalid { get; set; }
    }
}
