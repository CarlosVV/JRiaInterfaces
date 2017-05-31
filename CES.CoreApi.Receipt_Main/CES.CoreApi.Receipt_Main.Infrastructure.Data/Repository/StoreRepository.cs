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
    public class StoreRepository : BaseRepository<systblApp_TaxReceipt_Store>, IStoreRepository
    {
        public StoreRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_TaxReceipt_Store find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_Store> find(Expression<Func<systblApp_TaxReceipt_Store, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateStore(systblApp_TaxReceipt_Store obj)
        {
            this.Add(obj);
        }
        public void UpdateStore(systblApp_TaxReceipt_Store obj)
        {
            this.Update(obj);
        }
        public void RemoveStore(systblApp_TaxReceipt_Store obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
