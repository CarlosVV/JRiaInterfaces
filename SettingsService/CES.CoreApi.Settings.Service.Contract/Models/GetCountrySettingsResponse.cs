using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class GetCountrySettingsResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public int CountryId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AccountReceivableSettings AccountReceivableSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public BeneficiarySettings BeneficiarySettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CorporateSettings CorporateSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public MoneyTransferSettings MoneyTransferSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AddressValidationSettings AddressValidationSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public PossibleDuplicationSettings PossibleDuplicationSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DigitalReceiptSettings DigitalReceiptSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ScannerSettings ScannerSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CustomerConfidentialSettings CustomerConfidentialSettings { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CheckCashingTelephoneNumber { get; set; }

        [DataMember]
        public ComplianceWarningPeriodicity ComplianceWarningPeriodicity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string BillPayApplyTax { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool CityServedEnabled { get; set; }

        [DataMember]
        public WatchListPayoutSetting WatchListPayoutCountrySetting { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool NotificationCorrespondentsSendIfCompanyLocationNotSetup { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool NotificationCorrespondentsSendIfBaseCurrencyNotSetup { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool AllowViewCustomerIdentificationImages { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<int> OcrIdentificationScanningResolutionRange { get; set; }
    }
}