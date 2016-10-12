using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    public class ValidateBankDepositResponse
    {
        public BankAccountInfo CorrespInfo { get; set; }
        public string Provider { get; set; }
        public bool IsAccountValid { get; set; }

        //1001:valid; 1002:invalid; 1003:PA not supported; 2001:general error; 2002:internal error; 3001:pa error; 3999:exception
        //3001:invalid request parameters,
        public int ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
        public string CorrespErrCode { get; set; }
        public string CorrespErrMsg { get; set; }
        public string CorrespMsg { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string BankCode { get; set; }
    }
}
