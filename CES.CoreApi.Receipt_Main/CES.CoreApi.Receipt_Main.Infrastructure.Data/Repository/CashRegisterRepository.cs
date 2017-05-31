
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository
{
    public class CashRegisterRepository : BaseRepository<systblApp_TaxReceipt_CashRegister>, ICashRegisterRepository
    {
        public CashRegisterRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_TaxReceipt_CashRegister find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_CashRegister> find(Expression<Func<systblApp_TaxReceipt_CashRegister, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateCashRegister(systblApp_TaxReceipt_CashRegister obj)
        {
            this.Add(obj);
        }
        public void UpdateCashRegister(systblApp_TaxReceipt_CashRegister obj)
        {
            this.Update(obj);
        }
        public void RemoveCashRegister(systblApp_TaxReceipt_CashRegister obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
