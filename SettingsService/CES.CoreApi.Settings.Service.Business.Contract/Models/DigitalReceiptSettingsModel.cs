using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class DigitalReceiptSettingsModel
    {
        [CountrySetting("DigitalReceiptAllowUploadFileType", true, ",")]
        public List<string> AllowUploadFileType { get; set; }

        [CountrySetting("DigitalReceiptUploadedMaxFileSize")]
        public int UploadedMaxFileSize { get; set; }

        [CountrySetting("DigitalReceiptScannResolutionsRange", true, "|")]
        public List<int> ScanResolutionsRange { get; set; }

        [CountrySetting("DigitalReceiptBulkFileTypes", true, ",")]
        public List<string> BulkFileTypes { get; set; }

        [CountrySetting("DigitalReceiptMaxBulkFileSize")]
        public int MaxBulkFileSize { get; set; }
    }
}
