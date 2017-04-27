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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Role find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Role> find(Expression<Func<Role, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateRole(Role obj)
        {
            this.Add(obj);
        }
        public void UpdateRole(Role obj)
        {
            this.Update(obj);
        }
        public void RemoveRole(Role obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
