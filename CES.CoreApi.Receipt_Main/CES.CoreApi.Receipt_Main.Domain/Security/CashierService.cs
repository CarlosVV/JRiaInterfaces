using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class CashierService : ICashierService
    {
        private ICashierRepository repo;
        public CashierService(ICashierRepository repository)
        {
            repo = repository;
        }
        public List<Cashier> GetAllCashiers()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateCashier(Cashier objectEntry)
        {
            this.repo.CreateCashier(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateCashier(Cashier objectEntry)
        {
            this.repo.UpdateCashier(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveCashier(Cashier objectEntry)
        {
            this.repo.RemoveCashier(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
