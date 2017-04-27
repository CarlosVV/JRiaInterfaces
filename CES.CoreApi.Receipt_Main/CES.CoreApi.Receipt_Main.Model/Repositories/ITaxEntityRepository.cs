using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ITaxEntityRepository
    {
        TaxEntity find(string id);
        IEnumerable<TaxEntity> find(Expression<Func<TaxEntity, bool>> where);
        void CreateTaxEntity(TaxEntity obj);
        void UpdateTaxEntity(TaxEntity obj);
        void RemoveTaxEntity(TaxEntity obj);
        void SaveChanges();
    }
}
