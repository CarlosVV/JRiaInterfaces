using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ICashierRepository
    {
        Cashier find(string id);
        IEnumerable<Cashier> find(Expression<Func<Cashier, bool>> where);
        void CreateCashier(Cashier obj);
        void UpdateCashier(Cashier obj);
        void RemoveCashier(Cashier obj);
        void SaveChanges();
    }
}
