using CES.CoreApi.BankAccount.Api.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Interfaces
{
    public interface IBankAccountRepository
    {
        int AppId { get; set; }
        int RunBankDepositValidate(ValidateBankDepositRequest rqm, ValidateBankAccountInfo respModel);
        string GetCoreAPISettings(string sName, int nAppID);
        string GetCoreAPISettings(string sName);
        string GetBankcode(int nBankID, out string sBankName);
        BankAccountInfo GetBankAcctInfo(int nProcessorID, int nPayAgentID, string sBankName, string sBankCode, string sAccountNumber, int nDaysExpired);
        void SaveBankAcctInfo(BankAccountInfo baInfo);
        int GetPaWsSetting(CPaConn paConn);
        Hashtable GetPaExportSetting(int nExportFormatID);
        byte[] LoadKeyFromDB(string sKeyID);

        Hashtable GetProcessorInfo(int nProcessorID);
        int GetProcessorSetting(int nProcessorID, Hashtable ht);
        List<int> GetProcessor(int nProviderMapID, int nBankID, int nBankCounteyID);
    }
}
