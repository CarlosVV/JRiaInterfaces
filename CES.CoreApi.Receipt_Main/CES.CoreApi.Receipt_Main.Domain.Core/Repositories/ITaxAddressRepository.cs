using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface ITaxAddressRepository
    {
        actblTaxDocument_Entity_Address find(string id);
        IEnumerable<actblTaxDocument_Entity_Address> find(Expression<Func<actblTaxDocument_Entity_Address, bool>> where);
        void CreateTaxAddress(actblTaxDocument_Entity_Address obj);
        void UpdateTaxAddress(actblTaxDocument_Entity_Address obj);
        void RemoveTaxAddress(actblTaxDocument_Entity_Address obj);
        void SaveChanges();
    }
}
