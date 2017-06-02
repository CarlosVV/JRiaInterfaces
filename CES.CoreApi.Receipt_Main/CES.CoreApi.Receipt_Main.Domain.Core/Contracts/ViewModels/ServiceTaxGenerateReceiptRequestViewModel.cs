using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels
{
    public class ServiceTaxGenerateReceiptRequestViewModel
    {
        public string OrderNumber { get; set; }
        public int Folio { get; set; }
        public long PersistenceId { get; set; }
    }
}