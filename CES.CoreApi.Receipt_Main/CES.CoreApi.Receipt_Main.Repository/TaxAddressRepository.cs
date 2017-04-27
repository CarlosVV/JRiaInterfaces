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
    public class TaxAddressRepository : BaseRepository<TaxAddress>, ITaxAddressRepository
    {
        public TaxAddressRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public TaxAddress find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<TaxAddress> find(Expression<Func<TaxAddress, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaxAddress(TaxAddress obj)
        {
            this.Add(obj);
        }
        public void UpdateTaxAddress(TaxAddress obj)
        {
            this.Update(obj);
        }
        public void RemoveTaxAddress(TaxAddress obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
