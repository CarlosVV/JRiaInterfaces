using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.SettingsServiceDataContractNamespace)]
    public class ScannerSettings : ExtensibleObject
    {
        [DataMember]
        public int ComplianceAllowNoneForCustomerTaxId { get; set; }

        [DataMember]
        public string ComplianceDefaultCustomerTaxIdLabelWhenNone { get; set; }

        [DataMember]
        public int DefaultIdScanResolution { get; set; }

        [DataMember]
        public int DefaultDocumentScanResolution { get; set; }

        [DataMember]
        public List<int> DocumentScanResolutionRange { get; set; }

        [DataMember]
        public bool EnableDocumentScanResolutionRange { get; set; }
    }
}
