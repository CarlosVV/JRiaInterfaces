using System.Collections.Generic;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class DigitalReceiptSettingsModel
    {
        [CountrySettingCode("DigitalReceiptAllowUploadFileType")]
        public string AllowUploadFileType { get; set; }

        [CountrySettingCode("DigitalReceiptUploadedMaxFileSize")]
        public int UploadedMaxFileSize { get; set; }

        [CountrySettingCode("DigitalReceiptScannResolutionsRange")]
        public List<int> ScanResolutionsRange { get; set; }

        [CountrySettingCode("DigitalReceiptBulkFileTypes")]
        public string BulkFileTypes { get; set; }

        [CountrySettingCode("DigitalReceiptMaxBulkFileSize")]
        public int MaxBulkFileSize { get; set; }
    }
}
