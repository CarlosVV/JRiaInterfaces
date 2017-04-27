using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IDocumentService
    {
        List<Document> GetAllDocuments();
        void CreateDocument(Document objectEntry);
        void UpdateDocument(Document objectEntry);
        void RemoveDocument(Document objectEntry);
        
    }
}
