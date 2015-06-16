using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class GetCountrySettingsResponse
    {
        [DataMember]
        public int CountryId { get; set; }

        [DataMember]
        public AccountReceivableSettings AccountReceivableSettings { get; set; }

        [DataMember]
        public BeneficiarySettings BeneficiarySettings { get; set; }

        [DataMember]
        public CorporateSettings CorporateSettings { get; set; }

        [DataMember]
        public MoneyTransferSettings MoneyTransferSettings { get; set; }

        [DataMember]
        public AddressValidationSettings AddressValidationSettings { get; set; }

        [DataMember]
        public PossibleDuplicationSettings PossibleDuplicationSettings { get; set; }

        [DataMember]
        public DigitalReceiptSettings DigitalReceiptSettings { get; set; }

        [DataMember]
        public ScannerSettings ScannerSettings { get; set; }

        [DataMember]
        public CustomerConfidentialSettings CustomerConfidentialSettings { get; set; }

        [DataMember]
        public string CheckCashingTelephoneNumber { get; set; }

        [DataMember]
        public ComplianceWarningPeriodicity ComplianceWarningPeriodicity { get; set; }

        [DataMember]
        public string BillPayApplyTax { get; set; }

        [DataMember]
        public bool CityServedEnabled { get; set; }

        [DataMember]
        public WatchListPayoutSetting WatchListPayoutCountrySetting { get; set; }

        [DataMember]
        public bool NotificationCorrespondentsSendIfCompanyLocationNotSetup { get; set; }

        [DataMember]
        public bool NotificationCorrespondentsSendIfBaseCurrencyNotSetup { get; set; }

        [DataMember]
        public bool AllowViewCustomerIdentificationImages { get; set; }

        [DataMember]
        public List<int> OcrIdentificationScanningResolutionRange { get; set; }
    }
}