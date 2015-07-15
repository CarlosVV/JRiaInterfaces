using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class BankAccountModel
    {
        public string AccountNumber { get; set; }

        public string BankName { get; set; }

        public string AccountType { get; set; }

        public string BankCity { get; set; }
        
        public string RoutingCode { get; set; }
        
        public int RoutingType { get; set; }
    }
}
