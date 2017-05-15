using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Security;
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
    public class RoleRepository : BaseRepository<systblApp_TaxReceipt_Role>, IRoleRepository
    {
        public RoleRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_TaxReceipt_Role find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_Role> find(Expression<Func<systblApp_TaxReceipt_Role, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateRole(systblApp_TaxReceipt_Role obj)
        {
            this.Add(obj);
        }
        public void UpdateRole(systblApp_TaxReceipt_Role obj)
        {
            this.Update(obj);
        }
        public void RemoveRole(systblApp_TaxReceipt_Role obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
