using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    public class ValidateBankAccountInfo
    {
        public ValidateBankAccountInfo()
        {

        }
        public ValidateBankAccountResult DbResult { get; set; }
        public List<ValidateBankAccountMapFields> DbMapFields_List { get; set; }
        public BankAccountInfo CorrespInfo { get; set; }
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public string Message { get; set; }
    }
}
