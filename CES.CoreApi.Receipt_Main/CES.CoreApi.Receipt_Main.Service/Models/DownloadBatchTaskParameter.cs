﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class DownloadBatchTaskParameter
    {
        public int FolioStart { get; set; }

        public int FolioEnd{ get; set; }
    }
}