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
        actblTaxDocument_Entity find(string id);
        IEnumerable<actblTaxDocument_Entity> find(Expression<Func<actblTaxDocument_Entity, bool>> where);
        void CreateTaxEntity(actblTaxDocument_Entity obj);
        void UpdateTaxEntity(actblTaxDocument_Entity obj);
        void RemoveTaxEntity(actblTaxDocument_Entity obj);
        void SaveChanges();
    }
}
