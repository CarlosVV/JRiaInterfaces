﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class GetTransactionInfoRequestModel
    {
        public int PersistenceID { get; set; }

        public RequesterInfoModel RequesterInfo { get; set; }
        public string OrderPIN { get; set; }
        public int OrderID { get; set; }
        public string  CountryTo { get; set; }
        public string StateTo { get; set; }
        public int ProviderID { get; set; }



    }
}