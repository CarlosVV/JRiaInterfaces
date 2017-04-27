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
    public class FunctionRoleRepository : BaseRepository<FunctionRole>, IFunctionRoleRepository
    {
        public FunctionRoleRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public FunctionRole find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<FunctionRole> find(Expression<Func<FunctionRole, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateFunctionRole(FunctionRole obj)
        {
            this.Add(obj);
        }
        public void UpdateFunctionRole(FunctionRole obj)
        {
            this.Update(obj);
        }
        public void RemoveFunctionRole(FunctionRole obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
