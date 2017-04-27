using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ITaxAddressRepository
    {
        TaxAddress find(string id);
        IEnumerable<TaxAddress> find(Expression<Func<TaxAddress, bool>> where);
        void CreateTaxAddress(TaxAddress obj);
        void UpdateTaxAddress(TaxAddress obj);
        void RemoveTaxAddress(TaxAddress obj);
        void SaveChanges();
    }
}
