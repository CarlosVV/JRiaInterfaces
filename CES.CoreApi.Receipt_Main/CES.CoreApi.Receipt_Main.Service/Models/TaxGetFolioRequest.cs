﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxGetFolioRequest : BaseRequestModel
    {
        public int? Id { get; set; }       
        public string DocumentType { get; set; }
        public int FolioCurrentNumber { get; set; }
    }
}