using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface IDocumentReferenceRepository
    {
        actblTaxDocument_Reference find(string id);
        IEnumerable<actblTaxDocument_Reference> find(Expression<Func<actblTaxDocument_Reference, bool>> where);
        void CreateDocumentReference(actblTaxDocument_Reference obj);
        void UpdateDocumentReference(actblTaxDocument_Reference obj);
        void RemoveDocumentReference(actblTaxDocument_Reference obj);
        void SaveChanges();
    }
}
