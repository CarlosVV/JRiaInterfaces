using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class MoneyTransferSettingsModel
    {
        private const string AllowUploadFileType = "jpg,png";
        private const int FileMaxSize = 307200;
        private const int NumberOfFailedAttempts = 3;

        public MoneyTransferSettingsModel()
        {
            var allowedUploadFileTypeList = AllowUploadFileType.Split(',');

            RefundPinMaxFailedAttempts = NumberOfFailedAttempts;
            CustomerIdentificationUploadedFileMaxSize = FileMaxSize;
            CustomerIdentificationAllowUploadFileType = allowedUploadFileTypeList;
            OrderFilesUploadedMaxFileSize = FileMaxSize;
            OrderFilesAllowUploadFileType = allowedUploadFileTypeList;
        }

        [CountrySettingCode("MTReqScannedID")]
        public bool RequireScannedId { get; set; }

        [CountrySettingCode("MTOrderIDAmt1")]
        public decimal OrderIdAmt1 { get; set; }

        [CountrySettingCode("MTOrderIDAmt2")]
        public decimal OrderIdAmt2 { get; set; }

        [CountrySettingCode("MTNumOrdersDailyCancellationLimit")]
        public int NumberOrdersDailyCancellationLimit { get; set; }

        [CountrySettingCode("MTAmountDailyCancellationLimit")]
        public decimal AmountDailyCancellationLimit { get; set; }

        [CountrySettingCode("MTCustomerIdentificationAllowUploadFileType")]
        public IList<string> CustomerIdentificationAllowUploadFileType { get; set; }

        [CountrySettingCode("MTCustomerIdentificationUploadedFileMaxSize")]
        public int CustomerIdentificationUploadedFileMaxSize { get; set; }

        [CountrySettingCode("MTSameCityPayoutRestricted")]
        public bool SameCityPayoutRestricted { get; set; }

        [CountrySettingCode("MTRequireImageIdentificationForRiaLink")]
        public bool RequireImageIdentificationForRiaLink { get; set; }

        [CountrySettingCode("MTRequireDoBToSearchCustomers")]
        public bool RequireDoBToSearchCustomers { get; set; }

        [CountrySettingCode("MTGenerateRefundCode")]
        public bool GenerateRefundCode { get; set; }

        [CountrySettingCode("MTRefundPINMaxFailedAttempts")]
        public int RefundPinMaxFailedAttempts { get; set; }

        [CountrySettingCode("MTCustPhoneSearch_ForceExactMatch")]
        public bool CustomerPhoneSearchForceExactMatch { get; set; }

        [CountrySettingCode("MTOrderFilesAllowUploadFileType")]
        public IList<string> OrderFilesAllowUploadFileType { get; set; }

        [CountrySettingCode("MTOrdersFilesUploadedMaxFileSize")]
        public int OrderFilesUploadedMaxFileSize { get; set; }

        [CountrySettingCode("MTAllowToChangeIdentificationExpDateWhenExpired")]
        public bool AllowToChangeIdentificationExpireDateWhenExpired { get; set; }

        [CountrySettingCode("MTRequireSMSServiceNotifications")]
        public bool RequireSmsServiceNotifications { get; set; }

        [CountrySettingCode("MTDefCountryTo")]
        public string DefaultCountryTo { get; set; }
    }
}
