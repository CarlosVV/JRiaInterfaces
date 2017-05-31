using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface ITaxEntityRepository
    {
        systblApp_CoreAPI_TaxEntity find(string id);
        IEnumerable<systblApp_CoreAPI_TaxEntity> find(Expression<Func<systblApp_CoreAPI_TaxEntity, bool>> where);
        void CreateTaxEntity(systblApp_CoreAPI_TaxEntity obj);
        void UpdateTaxEntity(systblApp_CoreAPI_TaxEntity obj);
        void RemoveTaxEntity(systblApp_CoreAPI_TaxEntity obj);
        void SaveChanges();
    }
}
