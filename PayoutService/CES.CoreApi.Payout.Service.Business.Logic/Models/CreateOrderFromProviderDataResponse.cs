﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class CreateOrderFromProviderDataResponse
    {
        public int ReturnValue { get; set; }
        public string ReturnMessage { get; set; }
        public long TransactionID { get; set; }
    }
}