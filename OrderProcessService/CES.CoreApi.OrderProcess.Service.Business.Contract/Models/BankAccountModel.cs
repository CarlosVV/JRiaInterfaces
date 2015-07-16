namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class BankAccountModel
    {
        public string AccountNumber { get; set; }
        public int AccountType { get; set; }
        public string UnitaryAccountNumber { get; set; }
        public string UnitaryAccountNumberType { get; set; }
    }
}