using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    interface IDocumentReferenceRepository
    {
        systblApp_CoreAPI_DocumentReference find(string id);
        IEnumerable<systblApp_CoreAPI_DocumentDetail> find(Expression<Func<systblApp_CoreAPI_DocumentReference, bool>> where);
        void CreateDocumentReference(systblApp_CoreAPI_DocumentReference obj);
        void UpdateDocumentReference(systblApp_CoreAPI_DocumentReference obj);
        void RemoveDocumentReference(systblApp_CoreAPI_DocumentReference obj);
        void SaveChanges();
    }
}
