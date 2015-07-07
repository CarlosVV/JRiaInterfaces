using CES.CoreApi.Settings.Service.Business.Contract.Attributes;
using CES.CoreApi.Settings.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class PossibleDuplicationSettingsModel
    {
        public PossibleDuplicationSettingsModel()
        {
            ByNames = PossibleDuplicateSettingType.Disabled;
            ByNamesDob = PossibleDuplicateSettingType.Disabled;
            ByNamesDobId = PossibleDuplicateSettingType.Disabled;
            ByNamesId = PossibleDuplicateSettingType.Disabled;
            ById = PossibleDuplicateSettingType.Disabled;
            CustomerCreateByNames = PossibleDuplicateCustomerCreationSettingType.Create;
            CustomerCreateByNamesDob = PossibleDuplicateCustomerCreationSettingType.Create;
            CustomerCreateByNamesDobId = PossibleDuplicateCustomerCreationSettingType.DoNotCreate;
            CustomerCreateByNamesId = PossibleDuplicateCustomerCreationSettingType.Create;
            CustomerCreateById = PossibleDuplicateCustomerCreationSettingType.Create;
            CustomerActionByNames = PossibleDuplicateCustomerActionSettingType.LogOnly;
            CustomerActionByNamesDob = PossibleDuplicateCustomerActionSettingType.OnHold;
            CustomerActionByNamesDobId = PossibleDuplicateCustomerActionSettingType.Reject;
            CustomerActionByNamesId = PossibleDuplicateCustomerActionSettingType.OnHold;
            CustomerActionById = PossibleDuplicateCustomerActionSettingType.OnHold;
        }

        [CountrySetting("ComplCheckPossibleDuplicationByNames")]
        public PossibleDuplicateSettingType ByNames { get; set; }

        [CountrySetting("ComplCheckPossibleDuplicationByNamesDOB")]
        public PossibleDuplicateSettingType ByNamesDob { get; set; }

        [CountrySetting("ComplCheckPossibleDuplicationByNamesDOBID")]
        public PossibleDuplicateSettingType ByNamesDobId { get; set; }

        [CountrySetting("ComplCheckPossibleDuplicationByNamesID")]
        public PossibleDuplicateSettingType ByNamesId { get; set; }

        [CountrySetting("ComplCheckPossibleDuplicationByID")]
        public PossibleDuplicateSettingType ById { get; set; }

        [CountrySetting("ComplCreateCustomerWhenPossibleDuplicationByNames")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNames { get; set; }

        [CountrySetting("ComplCreateCustomerWhenPossibleDuplicationByNamesDOB")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNamesDob { get; set; }

        [CountrySetting("ComplCreateCustomerWhenPossibleDuplicationByNamesDOBID")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNamesDobId { get; set; }

        [CountrySetting("ComplCreateCustomerWhenPossibleDuplicationByNamesID")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNamesId { get; set; }

        [CountrySetting("ComplCreateCustomerWhenPossibleDuplicationByID")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateById { get; set; }

        [CountrySetting("ComplActionWhenPossibleDuplicationByNames")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNames { get; set; }

        [CountrySetting("ComplActionWhenPossibleDuplicationByNamesDOB")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNamesDob { get; set; }

        [CountrySetting("ComplActionWhenPossibleDuplicationByNamesDOBID")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNamesDobId { get; set; }

        [CountrySetting("ComplActionWhenPossibleDuplicationByNamesID")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNamesId { get; set; }

        [CountrySetting("ComplActionWhenPossibleDuplicationByID")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionById { get; set; }
    }
}
