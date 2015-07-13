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

        [CountrySetting("MTEnableRecurrentBeneficiary")]
        public RecurrentBeneficiarySettingType EnableRecurrent { get; set; }

        [CountrySetting("MTShowBeneficiariesTabFxOnline")]
        public RecurrentBeneficiarySettingType EnableRecurrentTab { get; set; }

        [CountrySetting("MTBeneficiaryConsolidation")]
        public BeneficiaryConsolidationSettingType EnableConsolidation { get; set; }
    }
}
