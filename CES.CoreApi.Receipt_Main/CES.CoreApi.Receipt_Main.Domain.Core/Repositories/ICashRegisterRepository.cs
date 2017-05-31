using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface ICashRegisterRepository
    {
        systblApp_TaxReceipt_CashRegister find(string id);
        IEnumerable<systblApp_TaxReceipt_CashRegister> find(Expression<Func<systblApp_TaxReceipt_CashRegister, bool>> where);
        void CreateCashRegister(systblApp_TaxReceipt_CashRegister obj);
        void UpdateCashRegister(systblApp_TaxReceipt_CashRegister obj);
        void RemoveCashRegister(systblApp_TaxReceipt_CashRegister obj);
        void SaveChanges();
    }
}
