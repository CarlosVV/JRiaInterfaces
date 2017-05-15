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
        systblApp_CoreAPI_TaxAddress find(string id);
        IEnumerable<systblApp_CoreAPI_TaxAddress> find(Expression<Func<systblApp_CoreAPI_TaxAddress, bool>> where);
        void CreateTaxAddress(systblApp_CoreAPI_TaxAddress obj);
        void UpdateTaxAddress(systblApp_CoreAPI_TaxAddress obj);
        void RemoveTaxAddress(systblApp_CoreAPI_TaxAddress obj);
        void SaveChanges();
    }
}
