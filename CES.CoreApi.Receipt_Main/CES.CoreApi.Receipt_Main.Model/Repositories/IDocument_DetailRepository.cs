using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IDocument_DetailRepository
    {
        Document_Detail find(string id);
        IEnumerable<Document_Detail> find(Expression<Func<Document_Detail, bool>> where);
        void CreateDocument_Detail(Document_Detail obj);
        void UpdateDocument_Detail(Document_Detail obj);
        void RemoveDocument_Detail(Document_Detail obj);
        void SaveChanges();
    }
}
