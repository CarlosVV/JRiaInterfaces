using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IDocument_TypeRepository
    {
        Document_Type find(string id);
        IEnumerable<Document_Type> find(Expression<Func<Document_Type, bool>> where);
        void CreateDocument_Type(Document_Type obj);
        void UpdateDocument_Type(Document_Type obj);
        void RemoveDocument_Type(Document_Type obj);
        void SaveChanges();
    }
}
