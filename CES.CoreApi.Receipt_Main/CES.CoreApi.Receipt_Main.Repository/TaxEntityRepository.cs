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
    public class TaxEntityRepository : BaseRepository<systblApp_CoreAPI_TaxEntity>, ITaxEntityRepository
    {
        public TaxEntityRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreAPI_TaxEntity find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_CoreAPI_TaxEntity> find(Expression<Func<systblApp_CoreAPI_TaxEntity, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaxEntity(systblApp_CoreAPI_TaxEntity obj)
        {
            this.Add(obj);
        }
        public void UpdateTaxEntity(systblApp_CoreAPI_TaxEntity obj)
        {
            this.Update(obj);
        }
        public void RemoveTaxEntity(systblApp_CoreAPI_TaxEntity obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
