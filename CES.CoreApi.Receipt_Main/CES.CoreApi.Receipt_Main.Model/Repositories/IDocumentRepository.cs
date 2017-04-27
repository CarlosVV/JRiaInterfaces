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
        Document find(string id);
        IEnumerable<Document> find(Expression<Func<Document, bool>> where);
        void CreateDocument(Document obj);
        void UpdateDocument(Document obj);
        void RemoveDocument(Document obj);
        void SaveChanges();
    }
}
