using System.ComponentModel;

namespace CES.CoreApi.Settings.Service.Business.Contract.Enumerations
{
    public enum CountrySettingType
    {
        Unknown = 0,
        [Description("AddressValidationOn_Web")]
        AddressValidationOnWeb = 653,
        [Description("AddressValidation_From_On")]
        AddressValidationFromOn = 660,
        [Description("AddressValidation_From_On_Stores")]
        AddressValidationFromOnStores = 662,
        [Description("AddressValidation_From_On_Agents")]
        AddressValidationFromOnAgents = 663,
        [Description("AddressValidation_From_NewCustsOnly")]
        AddressValidationFromNewCustsOnly = 664,
        [Description("AddressValidation_From_OnlyWhenIDRequired")]
        AddressValidationFromOnlyWhenIdRequired = 665,
        [Description("AddressValidation_From_RejectInvalid")]
        AddressValidationFromRejectInvalid = 666,
        [Description("AddressValidation_From_OnHoldInvalid")]
        AddressValidationFromOnHoldInvalid = 667,
        [Description("MTReqScannedID")]
        MtReqScannedId = 11451,
        [Description("MTOrderIDAmt1")]
        MtOrderIdAmt1 = 11424,
        [Description("MTOrderIDAmt2")]
        MtOrderIdAmt2 = 11425,
        [Description("MTDefCountryTo")]
        MtDefCountryTo = 11402,
        [Description("ARLimitDisplayOption")]
        ArLimitDisplayOption = 12300,
        [Description("CorporateCustomerServiceNumber")]
        CorporateCustomerServiceNumber = 13000,
        [Description("CorporateHelpDeskNumber")]
        CorporateHelpDeskNumber = 13001,
        [Description("CorporateARNumber")]
        CorporateArNumber = 13002,
        [Description("CorporateComplianceNumber")]
        CorporateComplianceNumber = 13003,
        [Description("CorporateAgentSupportNumber")]
        CorporateAgentSupportNumber = 13004,
        [Description("MTNumOrdersDailyCancellationLimit")]
        MtNumOrdersDailyCancellationLimit = 11260,
        [Description("MTAmountDailyCancellationLimit")]
        MtAmountDailyCancellationLimit = 11261,
        [Description("ComplUseCustomerValidation")]
        ComplUseCustomerValidation = 16000,
        [Description("ComplValidationMethod")]
        ComplValidationMethod = 16001,
        [Description("ComplAmountThresholdForValidation")]
        ComplAmountThresholdForValidation = 16002,
        [Description("ComplCustomerValidationLegalDeptEmailAddress")]
        ComplCustomerValidationLegalDeptEmailAddress = 16003,
        [Description("ComplUseCustomerValidationWhenSendFromThisState")]
        ComplUseCustomerValidationWhenSendFromThisState = 16004,
        [Description("ComplCustomerValidationMaximumOfRetries")]
        ComplCustomerValidationMaximumOfRetries = 16005,
        [Description("ComplCustomerValidationAlertWhenValidationFail")]
        ComplCustomerValidationAlertWhenValidationFail = 16006,
        [Description("ComplUseCustomerValidationWhenSendToThisCountry")]
        ComplUseCustomerValidationWhenSendToThisCountry = 16007,
        [Description("ComplianceWarningPeriodicity")]
        ComplianceWarningPeriodicity = 16100,
        [Description("BillPayApplyTax")]
        BillPayApplyTax = 20100,
        [Description("RiaPriterV2MaxCharPerLine")]
        RiaPriterV2MaxCharPerLine = 16150,
        [Description("MTEnableRecurrentBeneficiary")]
        MtEnableRecurrentBeneficiary = 11455,
        [Description("MTShowBeneficiariesTabFxOnline")]
        MtShowBeneficiariesTabFxOnline = 21507,
        [Description("MTBeneficiaryConsolidation")]
        MtBeneficiaryConsolidation = 11454,
        [Description("EnableCitiesServed")]
        EnableCitiesServed = 654,
        [Description("ComplCheckPossibleDuplicationByNames")]
        ComplCheckPossibleDuplicationByNames = 1525,
        [Description("ComplCheckPossibleDuplicationByNamesDOB")]
        ComplCheckPossibleDuplicationByNamesDob = 1526,
        [Description("ComplCheckPossibleDuplicationByNamesDOBID")]
        ComplCheckPossibleDuplicationByNamesDobid = 1527,
        [Description("ComplCheckPossibleDuplicationByNamesID")]
        ComplCheckPossibleDuplicationByNamesId = 1528,
        [Description("ComplCheckPossibleDuplicationByID")]
        ComplCheckPossibleDuplicationById = 1529,

        [Description("ComplCreateCustomerWhenPossibleDuplicationByNames")]
        ComplCreateCustomerWhenPossibleDuplicationByNames = 1500,
        [Description("ComplCreateCustomerWhenPossibleDuplicationByNamesDOB")]
        ComplCreateCustomerWhenPossibleDuplicationByNamesDob = 1501,
        [Description("ComplCreateCustomerWhenPossibleDuplicationByNamesDOBID")]
        ComplCreateCustomerWhenPossibleDuplicationByNamesDobid = 1502,
        [Description("ComplCreateCustomerWhenPossibleDuplicationByNamesID")]
        ComplCreateCustomerWhenPossibleDuplicationByNamesId = 1503,
        [Description("ComplCreateCustomerWhenPossibleDuplicationByID")]
        ComplCreateCustomerWhenPossibleDuplicationById = 1504,
        [Description("ComplActionWhenPossibleDuplicationByNames")]
        ComplActionWhenPossibleDuplicationByNames = 1505,
        [Description("ComplActionWhenPossibleDuplicationByNamesDOB")]
        ComplActionWhenPossibleDuplicationByNamesDob = 1506,
        [Description("ComplActionWhenPossibleDuplicationByNamesDOBID")]
        ComplActionWhenPossibleDuplicationByNamesDobid = 1507,
        [Description("ComplActionWhenPossibleDuplicationByNamesID")]
        ComplActionWhenPossibleDuplicationByNamesId = 1508,
        [Description("ComplActionWhenPossibleDuplicationByID")]
        ComplActionWhenPossibleDuplicationById = 1509,
        [Description("ComplCheckWatchListWhenPayingOut")]
        ComplCheckWatchListWhenPayingOut = 1600,
        [Description("FxOnlineNotification_Correspondents_SendIfCompanyLocationNotSetup")]
        FxOnlineNotificationCorrespondentsSendIfCompanyLocationNotSetup = 11900,
        [Description("FxOnlineNotification_Correspondents_SendIfBaseCurrencyNotSetup")]
        FxOnlineNotificationCorrespondentsSendIfBaseCurrencyNotSetup = 11901,
        [Description("DefaultIdScanResolution")]
        DefaultIdScanResolution = 18500,
        [Description("DefaultDocumentScanResolution")]
        DefaultDocumentScanResolution = 18501,
        [Description("DocumentScanResolutionRange")]
        DocumentScanResolutionRange = 18502,
        [Description("EnableDocumentScanResolutionRange")]
        EnableDocumentScanResolutionRange = 18503,
        [Description("OCRIdentificationScanningResolutionRange")]
        OcrIdentificationScanningResolutionRange = 18600,
        [Description("CorporateCustomerServiceFaxNumber")]
        CorporateCustomerServiceFaxNumber = 21400,
        [Description("CorporateCustomerServiceRefundEmail")]
        CorporateCustomerServiceRefundEmail = 21404,
        [Description("MTCustomerIdentificationAllowUploadFileType")]
        MtCustomerIdentificationAllowUploadFileType = 970,
        [Description("MTCustomerIdentificationUploadedFileMaxSize")]
        MtCustomerIdentificationUploadedFileMaxSize = 971,
        [Description("MTSameCityPayoutRestricted")]
        MtSameCityPayoutRestricted = 965,
        [Description("DigitalReceiptAllowUploadFileType")]
        DigitalReceiptAllowUploadFileType = 4000,
        [Description("DigitalReceiptUploadedMaxFileSize")]
        DigitalReceiptUploadedMaxFileSize = 4001,
        [Description("DigitalReceiptScannResolutionsRange")]
        DigitalReceiptScannResolutionsRange = 4002,
        [Description("AllowViewCustomerIdentificationImages")]
        AllowViewCustomerIdentificationImages = 975,
        [Description("MTRequireImageIdentificationForRiaLink")]
        MtRequireImageIdentificationForRiaLink = 7100,
        [Description("MTRequireDoBToSearchCustomers")]
        MtRequireDoBToSearchCustomers = 977,
        [Description("MTOrdersFilesUploadedMaxFileSize")]
        MtOrdersFilesUploadedMaxFileSize = 4101,
        [Description("MTOrderFilesAllowUploadFileType")]
        MtOrderFilesAllowUploadFileType = 4100,
        [Description("MTGenerateRefundCode")]
        MtGenerateRefundCode = 1459,
        [Description("MTRefundPINMaxFailedAttempts")]
        MtRefundPinMaxFailedAttempts = 1460,
        [Description("ComplAllowNoneForCustomerTaxID")]
        ComplAllowNoneForCustomerTaxId = 16050,
        [Description("ComplDefaultCustomerTaxIdLabelWhenNone")]
        ComplDefaultCustomerTaxIdLabelWhenNone = 16051,
        [Description("MTCustPhoneSearch_ForceExactMatch")]
        MtCustPhoneSearchForceExactMatch = 21410,
        [Description("MTAllowToChangeIdentificationExpDateWhenExpired")]
        MtAllowToChangeIdentificationExpDateWhenExpired = 21413,
        [Description("MTRequireSMSServiceNotifications")]
        MtRequireSmsServiceNotifications = 980,
        [Description("ARLimitDepositTime")]
        ArLimitDepositTime = 981,
        [Description("ARFaxNo")]
        ArFaxNo = 11601,
        [Description("CheckCashingTelephoneNumber")]
        CheckCashingTelephoneNumber = 21000,
        [Description("CorporateEmailAR")]
        CorporateEmailAr = 21501,
        [Description("CorporateEmailAgentSupport")]
        CorporateEmailAgentSupport = 21502,
        [Description("CorporateEmailCheckCashing")]
        CorporateEmailCheckCashing = 21503,
        [Description("CorporateEmailCompliance")]
        CorporateEmailCompliance = 21504,
        [Description("CorporateEmailCustomerService")]
        CorporateEmailCustomerService = 21505,
        [Description("CorporateEmailWebSupport")]
        CorporateEmailWebSupport = 21506,
        [Description("DigitalReceiptMaxBulkFileSize")]
        DigitalReceiptMaxBulkFileSize = 4201,
        [Description("DigitalReceiptBulkFileTypes")]
        DigitalReceiptBulkFileTypes = 4200
    }
}
