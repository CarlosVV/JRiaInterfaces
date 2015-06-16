using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class DigitalReceiptSettings : ExtensibleObject
    {
        [DataMember]
        public string AllowUploadFileType { get; set; }

        [DataMember]
        public int UploadedMaxFileSize { get; set; }

        [DataMember]
        public List<int> ScanResolutionsRange { get; set; }

        [DataMember]
        public string BulkFileTypes { get; set; }

        [DataMember]
        public int MaxBulkFileSize { get; set; }
    }
}