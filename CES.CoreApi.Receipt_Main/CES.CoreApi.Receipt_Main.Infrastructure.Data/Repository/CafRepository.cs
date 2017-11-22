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
    public class CafRepository : BaseRepository<actblTaxDocument_AuthCode>, ICafRepository
    {
        public CafRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public actblTaxDocument_AuthCode find(int id)
        {
            return this.Get(p => p.fAuthCodeID == id);
        }

        public IEnumerable<actblTaxDocument_AuthCode> find(Expression<Func<actblTaxDocument_AuthCode, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateCaf(actblTaxDocument_AuthCode obj)
        {
            this.Add(obj);
        }
        public void UpdateCaf(actblTaxDocument_AuthCode obj)
        {
            this.Update(obj);
        }
        public void RemoveCaf(actblTaxDocument_AuthCode obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
