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
    public class TaxEntityRepository : BaseRepository<TaxEntity>, ITaxEntityRepository
    {
        public TaxEntityRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public TaxEntity find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<TaxEntity> find(Expression<Func<TaxEntity, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaxEntity(TaxEntity obj)
        {
            this.Add(obj);
        }
        public void UpdateTaxEntity(TaxEntity obj)
        {
            this.Update(obj);
        }
        public void RemoveTaxEntity(TaxEntity obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
