using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class ScannerSettingsModel
    {
        [CountrySettingCode("ComplAllowNoneForCustomerTaxID")]
        public int ComplianceAllowNoneForCustomerTaxId { get; set; }

        [CountrySettingCode("ComplDefaultCustomerTaxIdLabelWhenNone")]
        public string ComplianceDefaultCustomerTaxIdLabelWhenNone { get; set; }

        /// <summary>
        /// Get the scanning resolution configured to use when use a scan function
        /// </summary>
        [CountrySettingCode("DefaultIdScanResolution")]
        public int DefaultIdScanResolution { get; set; }

        [CountrySettingCode("DefaultDocumentScanResolution")]
        public int DefaultDocumentScanResolution { get; set; }

        [CountrySettingCode("DocumentScanResolutionRange")]
        public List<int> DocumentScanResolutionRange { get; set; }

        [CountrySettingCode("EnableDocumentScanResolutionRange")]
        public bool EnableDocumentScanResolutionRange { get; set; }
    }
}
