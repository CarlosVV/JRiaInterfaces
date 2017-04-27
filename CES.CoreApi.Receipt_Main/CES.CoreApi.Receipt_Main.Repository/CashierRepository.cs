using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class CashierRepository : BaseRepository<Cashier>, ICashierRepository
    {
        public CashierRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Cashier find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Cashier> find(Expression<Func<Cashier, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateCashier(Cashier obj)
        {
            this.Add(obj);
        }
        public void UpdateCashier(Cashier obj)
        {
            this.Update(obj);
        }
        public void RemoveCashier(Cashier obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
