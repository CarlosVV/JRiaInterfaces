using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class ScannerSettingsModel
    {
        [CountrySetting("ComplAllowNoneForCustomerTaxID")]
        public int ComplianceAllowNoneForCustomerTaxId { get; set; }

        [CountrySetting("ComplDefaultCustomerTaxIdLabelWhenNone")]
        public string ComplianceDefaultCustomerTaxIdLabelWhenNone { get; set; }

        /// <summary>
        /// Get the scanning resolution configured to use when use a scan function
        /// </summary>
        [CountrySetting("DefaultIdScanResolution")]
        public int DefaultIdScanResolution { get; set; }

        [CountrySetting("DefaultDocumentScanResolution")]
        public int DefaultDocumentScanResolution { get; set; }

        [CountrySetting("DocumentScanResolutionRange", true, "|")]
        public List<int> DocumentScanResolutionRange { get; set; }

        [CountrySetting("EnableDocumentScanResolutionRange")]
        public bool EnableDocumentScanResolutionRange { get; set; }
    }
}
