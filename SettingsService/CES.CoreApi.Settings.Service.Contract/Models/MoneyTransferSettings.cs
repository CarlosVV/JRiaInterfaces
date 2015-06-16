using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class MoneyTransferSettings : ExtensibleObject
    {
        [DataMember]
        public bool RequireScannedId { get; set; }

        [DataMember]
        public decimal OrderIdAmt1 { get; set; }

        [DataMember]
        public decimal OrderIdAmt2 { get; set; }

        [DataMember]
        public int NumberOrdersDailyCancellationLimit { get; set; }

        [DataMember]
        public decimal AmountDailyCancellationLimit { get; set; }

        [DataMember]
        public IList<string> CustomerIdentificationAllowUploadFileType { get; set; }

        [DataMember]
        public int CustomerIdentificationUploadedFileMaxSize { get; set; }

        [DataMember]
        public bool SameCityPayoutRestricted { get; set; }

        [DataMember]
        public bool RequireImageIdentificationForRiaLink { get; set; }

        [DataMember]
        public bool RequireDoBToSearchCustomers { get; set; }

        [DataMember]
        public bool GenerateRefundCode { get; set; }

        [DataMember]
        public int RefundPinMaxFailedAttempts { get; set; }

        [DataMember]
        public bool CustomerPhoneSearchForceExactMatch { get; set; }

        [DataMember]
        public IList<string> OrderFilesAllowUploadFileType { get; set; }

        [DataMember]
        public int OrderFilesUploadedMaxFileSize { get; set; }

        [DataMember]
        public bool AllowToChangeIdentificationExpireDateWhenExpired { get; set; }

        [DataMember]
        public bool RequireSmsServiceNotifications { get; set; }

        [DataMember]
        public string DefaultCountryTo { get; set; }
    }
}