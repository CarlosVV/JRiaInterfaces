using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface ICashierService
    {
        List<Cashier> GetAllCashiers();
        void CreateCashier(Cashier objectEntry);
        void UpdateCashier(Cashier objectEntry);
        void RemoveCashier(Cashier objectEntry);
        
    }
}
