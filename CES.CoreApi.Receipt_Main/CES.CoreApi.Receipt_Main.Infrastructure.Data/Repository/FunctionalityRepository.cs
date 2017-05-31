
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
    public class FunctionalityRepository : BaseRepository<systblApp_TaxReceipt_Functionality>, IFunctionalityRepository
    {
        public FunctionalityRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_TaxReceipt_Functionality find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_Functionality> find(Expression<Func<systblApp_TaxReceipt_Functionality, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateFunctionality(systblApp_TaxReceipt_Functionality obj)
        {
            this.Add(obj);
        }
        public void UpdateFunctionality(systblApp_TaxReceipt_Functionality obj)
        {
            this.Update(obj);
        }
        public void RemoveFunctionality(systblApp_TaxReceipt_Functionality obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
