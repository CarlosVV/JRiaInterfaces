using CES.CoreApi.Settings.Service.Business.Contract.Attributes;
using CES.CoreApi.Settings.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class BeneficiarySettingsModel
    {
        public BeneficiarySettingsModel()
        {
            EnableConsolidation = BeneficiaryConsolidationSettingType.Disabled;
            EnableRecurrent = RecurrentBeneficiarySettingType.Disabled;
        }

        [CountrySettingCode("MTEnableRecurrentBeneficiary")]
        public RecurrentBeneficiarySettingType EnableRecurrent { get; set; }

        [CountrySettingCode("MTShowBeneficiariesTabFxOnline")]
        public RecurrentBeneficiarySettingType EnableRecurrentTab { get; set; }

        [CountrySettingCode("MTBeneficiaryConsolidation")]
        public BeneficiaryConsolidationSettingType EnableConsolidation { get; set; }
    }
}
