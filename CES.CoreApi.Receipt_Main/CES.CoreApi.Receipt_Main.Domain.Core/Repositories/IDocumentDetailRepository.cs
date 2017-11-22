using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface IDocumentDetailRepository
    {
        actblTaxDocument_Detail find(string id);
        IEnumerable<actblTaxDocument_Detail> find(Expression<Func<actblTaxDocument_Detail, bool>> where);
        void CreateDocumentDetail(actblTaxDocument_Detail obj);
        void UpdateDocumentDetail(actblTaxDocument_Detail obj);
        void RemoveDocumentDetail(actblTaxDocument_Detail obj);
        void SaveChanges();
    }
}
