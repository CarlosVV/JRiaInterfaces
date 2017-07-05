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
    public class CafRepository : BaseRepository<systblApp_CoreAPI_Caf>, ICafRepository
    {
        public CafRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreAPI_Caf find(string id)
        {
            return this.Get(p => p.fCafId.ToString() == id);
        }

        public IEnumerable<systblApp_CoreAPI_Caf> find(Expression<Func<systblApp_CoreAPI_Caf, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateCaf(systblApp_CoreAPI_Caf obj)
        {
            this.Add(obj);
        }
        public void UpdateCaf(systblApp_CoreAPI_Caf obj)
        {
            this.Update(obj);
        }
        public void RemoveCaf(systblApp_CoreAPI_Caf obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
