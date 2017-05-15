using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IDocumentRepository
    {
        systblApp_CoreAPI_Document find(string id);
        IEnumerable<systblApp_CoreAPI_Document> find(Expression<Func<systblApp_CoreAPI_Document, bool>> where);
        void CreateDocument(systblApp_CoreAPI_Document obj);
        void UpdateDocument(systblApp_CoreAPI_Document obj);
        void RemoveDocument(systblApp_CoreAPI_Document obj);
        void SaveChanges();
    }
}
