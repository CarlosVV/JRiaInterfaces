using CES.CoreApi.BankAccount.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Interfaces
{
    public interface IBankAccountValidationProcessor
    {
        ValidateBankAccountInfo ValidateBankDeposit(ValidateBankDepositRequest requestModel);
        BankAccountInfo ValidateBankAccount(int nProcessorID, int nBankID, int nPayagentId, string sBankName, string sBankCode, string sAccountNumber, bool bMatchProcessor);
        void LogMessage(string sType, string sModule, string sMsg);
        void LogMessage(string sMsg);
        //void Init(ILoggerProxy loggerProxy, ILogMonitorFactory logMonitorFactory, IIdentityManager identityManager, IDatabaseInstanceProvider databaseInstanceProvider, ICacheProvider cacheProvider);
    }
}
