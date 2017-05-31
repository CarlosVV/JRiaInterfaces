using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IDocumentReferenceService
    {
        List<systblApp_CoreAPI_DocumentReference> GetAllDocumentReferences();
        void CreateDocumentReference(systblApp_CoreAPI_DocumentReference objectEntry);
        void UpdateDocumentReference(systblApp_CoreAPI_DocumentReference objectEntry);
        void RemoveDocumentReference(systblApp_CoreAPI_DocumentReference objectEntry);
        void SaveChanges();
    }
}
