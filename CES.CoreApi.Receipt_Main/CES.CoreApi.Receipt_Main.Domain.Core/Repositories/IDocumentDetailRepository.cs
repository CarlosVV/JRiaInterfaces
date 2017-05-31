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
        systblApp_CoreAPI_DocumentDetail find(string id);
        IEnumerable<systblApp_CoreAPI_DocumentDetail> find(Expression<Func<systblApp_CoreAPI_DocumentDetail, bool>> where);
        void CreateDocumentDetail(systblApp_CoreAPI_DocumentDetail obj);
        void UpdateDocumentDetail(systblApp_CoreAPI_DocumentDetail obj);
        void RemoveDocumentDetail(systblApp_CoreAPI_DocumentDetail obj);
        void SaveChanges();
    }
}
