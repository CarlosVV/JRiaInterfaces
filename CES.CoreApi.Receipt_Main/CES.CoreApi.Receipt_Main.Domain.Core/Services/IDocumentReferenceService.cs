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
        List<actblTaxDocument_Reference> GetAllDocumentReferences();
        void CreateDocumentReference(actblTaxDocument_Reference objectEntry);
        void UpdateDocumentReference(actblTaxDocument_Reference objectEntry);
        void RemoveDocumentReference(actblTaxDocument_Reference objectEntry);
        void SaveChanges();
    }
}
