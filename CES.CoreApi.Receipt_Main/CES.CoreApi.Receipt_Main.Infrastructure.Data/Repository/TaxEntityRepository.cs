using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
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
    public class TaxEntityRepository : BaseRepository<actblTaxDocument_Entity>, ITaxEntityRepository
    {
        public TaxEntityRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public actblTaxDocument_Entity find(string id)
        {
            return this.Get(p => p.fEntityID.ToString() == id);
        }

        public IEnumerable<actblTaxDocument_Entity> find(Expression<Func<actblTaxDocument_Entity, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaxEntity(actblTaxDocument_Entity obj)
        {
            this.Add(obj);
        }
        public void UpdateTaxEntity(actblTaxDocument_Entity obj)
        {
            this.Update(obj);
        }
        public void RemoveTaxEntity(actblTaxDocument_Entity obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
