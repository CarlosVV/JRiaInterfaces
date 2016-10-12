using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    public class ValidateBankDepositRequest
    {       
        public int AppID { get; set; }       
        public int AppObjectID { get; set; }       
        public int UserNameID { get; set; }       
        public string UserLocale { get; set; }       
        public bool CheckIfValidOnly { get; set; }       
        public string LocalDate { get; set; }       
        public int AgentID { get; set; }       
        public int AgentLocID { get; set; }       
        public int BankID { get; set; }       
        public int BankCountryID { get; set; }       
        public string DepositCurrency { get; set; }       
        public int ProviderID { get; set; }       
        public int BankAccountTypeID { get; set; }       
        public string BankAccountNo { get; set; }       
        public string UnitaryAccountNo { get; set; }       
        public string BankRoutingCode { get; set; }       
        public string BIC { get; set; }       
        public int BankBranchID { get; set; }       
        public string BankBranchName { get; set; }       
        public string BankBranchCity { get; set; }       
        public string BankBranchNumber { get; set; }
    }
}
