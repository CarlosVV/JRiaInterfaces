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
    public class FunctionalityRepository : BaseRepository<Functionality>, IFunctionalityRepository
    {
        public FunctionalityRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Functionality find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Functionality> find(Expression<Func<Functionality, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateFunctionality(Functionality obj)
        {
            this.Add(obj);
        }
        public void UpdateFunctionality(Functionality obj)
        {
            this.Update(obj);
        }
        public void RemoveFunctionality(Functionality obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
