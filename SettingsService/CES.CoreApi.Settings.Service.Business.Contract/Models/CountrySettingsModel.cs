using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;
using CES.CoreApi.Settings.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class CountrySettingsModel
    {
        public CountrySettingsModel()
        {
            MoneyTransferSettings = new MoneyTransferSettingsModel();
            AccountReceivableSettings = new AccountReceivableSettingsModel();
            BeneficiarySettings = new BeneficiarySettingsModel();
            CorporateSettings = new CorporateSettingsModel();
            MoneyTransferSettings = new MoneyTransferSettingsModel();
            AddressValidationSettings = new AddressValidationSettingsModel();
            PossibleDuplicationSettings = new PossibleDuplicationSettingsModel();
            DigitalReceiptSettings = new DigitalReceiptSettingsModel();
            ScannerSettings = new ScannerSettingsModel();
            CustomerConfidentialSettings = new CustomerConfidentialSettingsModel();

            WatchListPayoutCountrySetting = WatchListPayoutSettingType.Off;
            AllowViewCustomerIdentificationImages = true;
        }

        public int CountryId { get; set; }
        
        public AccountReceivableSettingsModel AccountReceivableSettings { get; set; }
        public BeneficiarySettingsModel BeneficiarySettings { get; set; }
        public CorporateSettingsModel CorporateSettings { get; set; }
        public MoneyTransferSettingsModel MoneyTransferSettings { get; set; }
        public AddressValidationSettingsModel AddressValidationSettings { get; set; }
        public PossibleDuplicationSettingsModel PossibleDuplicationSettings { get; set; }
        public DigitalReceiptSettingsModel DigitalReceiptSettings { get; set; }
        public ScannerSettingsModel ScannerSettings { get; set; }
        public CustomerConfidentialSettingsModel CustomerConfidentialSettings { get; set; }

        [CountrySettingCodeAttribute("CheckCashingTelephoneNumber")]
        public string CheckCashingTelephoneNumber { get; set; }

        [CountrySettingCodeAttribute("ComplianceWarningPeriodicity")]
        public ComplianceWarningPeriodicityType ComplianceWarningPeriodicity { get; set; }

        [CountrySettingCodeAttribute("BillPayApplyTax")]
        public string BillPayApplyTax { get; set; }

        [CountrySettingCodeAttribute("EnableCitiesServed")]
        public bool CityServedEnabled { get; set; }

        [CountrySettingCodeAttribute("ComplCheckWatchListWhenPayingOut")]
        public WatchListPayoutSettingType WatchListPayoutCountrySetting { get; set; }

        [CountrySettingCode("FxOnlineNotification_Correspondents_SendIfCompanyLocationNotSetup")]
        public bool NotificationCorrespondentsSendIfCompanyLocationNotSetup { get; set; }

        [CountrySettingCode("FxOnlineNotification_Correspondents_SendIfBaseCurrencyNotSetup")]
        public bool NotificationCorrespondentsSendIfBaseCurrencyNotSetup { get; set; }

        [CountrySettingCode("AllowViewCustomerIdentificationImages")]
        public bool AllowViewCustomerIdentificationImages { get; set; }

        [CountrySettingCode("OCRIdentificationScanningResolutionRange")]
        public List<int> OcrIdentificationScanningResolutionRange { get; set; }
    }
}