using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxGenerateResponse
    {
        public ReturnInfo ReturnInfo { get; set; }
        public long PersistenceID { get; set; }
        public byte[] Receipt { get; set; }
        public string Folio { get; set; }
    }
}