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
    public class CafRepository : BaseRepository<Caf>, ICafRepository
    {
        public CafRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Caf find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Caf> find(Expression<Func<Caf, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateCaf(Caf obj)
        {
            this.Add(obj);
        }
        public void UpdateCaf(Caf obj)
        {
            this.Update(obj);
        }
        public void RemoveCaf(Caf obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
