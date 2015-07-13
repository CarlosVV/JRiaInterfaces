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

        [CountrySetting("MTReqScannedID")]
        public bool RequireScannedId { get; set; }

        [CountrySetting("MTOrderIDAmt1")]
        public decimal OrderIdAmount1 { get; set; }

        [CountrySetting("MTOrderIDAmt2")]
        public decimal OrderIdAmount2 { get; set; }

        [CountrySetting("MTNumOrdersDailyCancellationLimit")]
        public int NumberOrdersDailyCancellationLimit { get; set; }

        [CountrySetting("MTAmountDailyCancellationLimit")]
        public decimal AmountDailyCancellationLimit { get; set; }

        [CountrySetting("MTCustomerIdentificationAllowUploadFileType", true, ",")]
        public IList<string> CustomerIdentificationAllowUploadFileType { get; set; }

        [CountrySetting("MTCustomerIdentificationUploadedFileMaxSize")]
        public int CustomerIdentificationUploadedFileMaxSize { get; set; }

        [CountrySetting("MTSameCityPayoutRestricted")]
        public bool SameCityPayoutRestricted { get; set; }

        [CountrySetting("MTRequireImageIdentificationForRiaLink")]
        public bool RequireImageIdentificationForRiaLink { get; set; }

        [CountrySetting("MTRequireDoBToSearchCustomers")]
        public bool RequireDoBToSearchCustomers { get; set; }

        [CountrySetting("MTGenerateRefundCode")]
        public bool GenerateRefundCode { get; set; }

        [CountrySetting("MTRefundPINMaxFailedAttempts")]
        public int RefundPinMaxFailedAttempts { get; set; }

        [CountrySetting("MTCustPhoneSearch_ForceExactMatch")]
        public bool CustomerPhoneSearchForceExactMatch { get; set; }

        [CountrySetting("MTOrderFilesAllowUploadFileType", true, ",")]
        public IList<string> OrderFilesAllowUploadFileType { get; set; }

        [CountrySetting("MTOrdersFilesUploadedMaxFileSize")]
        public int OrderFilesUploadedMaxFileSize { get; set; }

        [CountrySetting("MTAllowToChangeIdentificationExpDateWhenExpired")]
        public bool AllowToChangeIdentificationExpireDateWhenExpired { get; set; }

        [CountrySetting("MTRequireSMSServiceNotifications")]
        public bool RequireSmsServiceNotifications { get; set; }

        [CountrySetting("MTDefCountryTo")]
        public string DefaultCountryTo { get; set; }
    }
}
