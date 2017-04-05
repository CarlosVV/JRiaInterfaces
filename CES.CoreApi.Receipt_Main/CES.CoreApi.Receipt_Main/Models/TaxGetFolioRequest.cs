using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxGetFolioRequest
    {
        public string Id { get; set; }       
        public int DocumentType { get; set; }
        public int FolioCurrentNumber { get; set; }      
    }
}