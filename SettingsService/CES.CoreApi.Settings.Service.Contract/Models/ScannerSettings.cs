using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class ScannerSettings : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int ComplianceAllowNoneForCustomerTaxId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ComplianceDefaultCustomerTaxIdLabelWhenNone { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DefaultIdScanResolution { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DefaultDocumentScanResolution { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<int> DocumentScanResolutionRange { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool EnableDocumentScanResolutionRange { get; set; }
    }
}
