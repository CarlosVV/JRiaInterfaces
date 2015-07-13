using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class DigitalReceiptSettings : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public List<string> AllowUploadFileType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int UploadedMaxFileSize { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<int> ScanResolutionsRange { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<string> BulkFileTypes { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int MaxBulkFileSize { get; set; }
    }
}