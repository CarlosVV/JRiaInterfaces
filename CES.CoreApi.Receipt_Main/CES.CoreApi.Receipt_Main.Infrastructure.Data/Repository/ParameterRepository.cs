using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CES.CoreApi.Receipt_Main.Repository.Repository
{
    public class ParameterRepository : BaseRepository<systblApp_TaxReceipt_Parameter>, IParameterRepository
    {
        public ParameterRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public systblApp_TaxReceipt_Parameter find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_Parameter> find(Expression<Func<systblApp_TaxReceipt_Parameter, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateParameter(systblApp_TaxReceipt_Parameter obj)
        {
            this.Add(obj);
        }
        public void UpdateParameter(systblApp_TaxReceipt_Parameter obj)
        {
            this.Update(obj);
        }
        public void RemoveParameter(systblApp_TaxReceipt_Parameter obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }
    }
}
