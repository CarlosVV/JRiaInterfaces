using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models.DTOs
{
    public class CAF
    {
        public string Id { get; set; }
        public string CompanyTaxId { get; set; }
        public string CompanyLegalName { get; set; }
        public int DocumentType { get; set; }
        public int FolioCurrentNumber { get; set; }
        public int FolioStartNumber { get; set; }
        public int FolioEndNumber { get; set; }
        public string DateAuthorization { get; set; }
        public string FileContent { get; set; }
    }
}