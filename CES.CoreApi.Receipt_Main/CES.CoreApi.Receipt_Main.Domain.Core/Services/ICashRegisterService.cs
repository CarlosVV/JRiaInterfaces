using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface ICashRegisterService
    {
        List<systblApp_TaxReceipt_CashRegister> GetAllCashRegisters();
        void CreateCashRegister
            (systblApp_TaxReceipt_CashRegister objectEntry);
        void UpdateCashRegister(systblApp_TaxReceipt_CashRegister objectEntry);
        void RemoveCashRegister(systblApp_TaxReceipt_CashRegister objectEntry);
        void SaveChanges();
    }
}
