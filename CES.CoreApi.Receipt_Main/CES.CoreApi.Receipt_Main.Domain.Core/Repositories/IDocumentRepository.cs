using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface IDocumentRepository
    {
        actblTaxDocument find(string id);
        IEnumerable<actblTaxDocument> find(Expression<Func<actblTaxDocument, bool>> where);
        void CreateDocument(actblTaxDocument obj);
        void UpdateDocument(actblTaxDocument obj);
        void RemoveDocument(actblTaxDocument obj);
        void SaveChanges();
    }
}
