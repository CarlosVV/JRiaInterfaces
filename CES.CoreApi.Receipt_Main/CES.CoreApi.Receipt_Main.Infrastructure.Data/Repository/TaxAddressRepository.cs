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
    public class TaxAddressRepository : BaseRepository<actblTaxDocument_Entity_Address>, ITaxAddressRepository
    {
        public TaxAddressRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public actblTaxDocument_Entity_Address find(string id)
        {
            return this.Get(p => p.fAddressID.ToString() == id);
        }

        public IEnumerable<actblTaxDocument_Entity_Address> find(Expression<Func<actblTaxDocument_Entity_Address, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateTaxAddress(actblTaxDocument_Entity_Address obj)
        {
            this.Add(obj);
        }
        public void UpdateTaxAddress(actblTaxDocument_Entity_Address obj)
        {
            this.Update(obj);
        }
        public void RemoveTaxAddress(actblTaxDocument_Entity_Address obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
