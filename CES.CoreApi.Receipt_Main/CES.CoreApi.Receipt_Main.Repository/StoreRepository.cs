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
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Store find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Store> find(Expression<Func<Store, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateStore(Store obj)
        {
            this.Add(obj);
        }
        public void UpdateStore(Store obj)
        {
            this.Update(obj);
        }
        public void RemoveStore(Store obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
