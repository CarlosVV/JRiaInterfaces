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
    public class TaxAddressRepository : BaseRepository<systblApp_CoreAPI_TaxAddress>, ITaxAddressRepository
    {
        public TaxAddressRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreAPI_TaxAddress find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_CoreAPI_TaxAddress> find(Expression<Func<systblApp_CoreAPI_TaxAddress, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaxAddress(systblApp_CoreAPI_TaxAddress obj)
        {
            this.Add(obj);
        }
        public void UpdateTaxAddress(systblApp_CoreAPI_TaxAddress obj)
        {
            this.Update(obj);
        }
        public void RemoveTaxAddress(systblApp_CoreAPI_TaxAddress obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
