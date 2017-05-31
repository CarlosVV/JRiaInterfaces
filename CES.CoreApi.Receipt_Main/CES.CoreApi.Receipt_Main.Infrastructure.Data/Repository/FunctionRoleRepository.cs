
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
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
    public class FunctionRoleRepository : BaseRepository<systblApp_TaxReceipt_FunctionRole>, IFunctionRoleRepository
    {
        public FunctionRoleRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_TaxReceipt_FunctionRole find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_FunctionRole> find(Expression<Func<systblApp_TaxReceipt_FunctionRole, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateFunctionRole(systblApp_TaxReceipt_FunctionRole obj)
        {
            this.Add(obj);
        }
        public void UpdateFunctionRole(systblApp_TaxReceipt_FunctionRole obj)
        {
            this.Update(obj);
        }
        public void RemoveFunctionRole(systblApp_TaxReceipt_FunctionRole obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
