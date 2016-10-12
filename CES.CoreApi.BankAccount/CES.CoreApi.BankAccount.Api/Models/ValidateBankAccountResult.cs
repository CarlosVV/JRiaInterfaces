using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    public class ValidateBankAccountResult
    {
        public int RetVal { get; set; }
        public string RetMsg { get; set; }
        public bool bProviderChanged { get; set; }
        public int ProviderID { get; set; }
        public int CorrespID { get; set; }
        public int CorrespLocID { get; set; }
    }
}
