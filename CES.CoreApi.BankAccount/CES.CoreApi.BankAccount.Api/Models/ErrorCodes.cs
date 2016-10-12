using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    public enum ErrorCodes
    {
        CorrespNotSetup = 9001,
        CorrespDisabled = 9002,
        ExceptionValidateBankAcct = 9900,
        InvalidRequest = 3001
    }
}
