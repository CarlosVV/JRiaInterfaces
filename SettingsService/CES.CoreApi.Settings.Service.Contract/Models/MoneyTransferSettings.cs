using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class MoneyTransferSettings : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public bool RequireScannedId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal OrderIdAmount1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal OrderIdAmount2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int NumberOrdersDailyCancellationLimit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal AmountDailyCancellationLimit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> CustomerIdentificationAllowUploadFileType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CustomerIdentificationUploadedFileMaxSize { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool SameCityPayoutRestricted { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool RequireImageIdentificationForRiaLink { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool RequireDoBToSearchCustomers { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool GenerateRefundCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int RefundPinMaxFailedAttempts { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool CustomerPhoneSearchForceExactMatch { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> OrderFilesAllowUploadFileType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int OrderFilesUploadedMaxFileSize { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool AllowToChangeIdentificationExpireDateWhenExpired { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool RequireSmsServiceNotifications { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DefaultCountryTo { get; set; }
    }
}