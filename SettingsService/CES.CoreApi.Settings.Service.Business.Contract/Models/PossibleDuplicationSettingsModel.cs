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

        [CountrySettingCode("ComplCheckPossibleDuplicationByNames")]
        public PossibleDuplicateSettingType ByNames { get; set; }

        [CountrySettingCode("ComplCheckPossibleDuplicationByNamesDOB")]
        public PossibleDuplicateSettingType ByNamesDob { get; set; }

        [CountrySettingCode("ComplCheckPossibleDuplicationByNamesDOBID")]
        public PossibleDuplicateSettingType ByNamesDobId { get; set; }

        [CountrySettingCode("ComplCheckPossibleDuplicationByNamesID")]
        public PossibleDuplicateSettingType ByNamesId { get; set; }

        [CountrySettingCode("ComplCheckPossibleDuplicationByID")]
        public PossibleDuplicateSettingType ById { get; set; }

        [CountrySettingCode("ComplCreateCustomerWhenPossibleDuplicationByNames")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNames { get; set; }

        [CountrySettingCode("ComplCreateCustomerWhenPossibleDuplicationByNamesDOB")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNamesDob { get; set; }

        [CountrySettingCode("ComplCreateCustomerWhenPossibleDuplicationByNamesDOBID")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNamesDobId { get; set; }

        [CountrySettingCode("ComplCreateCustomerWhenPossibleDuplicationByNamesID")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateByNamesId { get; set; }

        [CountrySettingCode("ComplCreateCustomerWhenPossibleDuplicationByID")]
        public PossibleDuplicateCustomerCreationSettingType CustomerCreateById { get; set; }

        [CountrySettingCode("ComplActionWhenPossibleDuplicationByNames")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNames { get; set; }

        [CountrySettingCode("ComplActionWhenPossibleDuplicationByNamesDOB")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNamesDob { get; set; }

        [CountrySettingCode("ComplActionWhenPossibleDuplicationByNamesDOBID")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNamesDobId { get; set; }

        [CountrySettingCode("ComplActionWhenPossibleDuplicationByNamesID")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionByNamesId { get; set; }

        [CountrySettingCode("ComplActionWhenPossibleDuplicationByID")]
        public PossibleDuplicateCustomerActionSettingType CustomerActionById { get; set; }
    }
}
