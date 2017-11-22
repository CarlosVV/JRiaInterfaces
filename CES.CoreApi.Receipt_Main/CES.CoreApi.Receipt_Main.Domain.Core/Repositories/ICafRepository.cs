using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface ICafRepository
    {
        actblTaxDocument_AuthCode find(int id);
        IEnumerable<actblTaxDocument_AuthCode> find(Expression<Func<actblTaxDocument_AuthCode, bool>> where);
        void CreateCaf(actblTaxDocument_AuthCode obj);
        void UpdateCaf(actblTaxDocument_AuthCode obj);
        void RemoveCaf(actblTaxDocument_AuthCode obj);
        void SaveChanges();
    }
}
