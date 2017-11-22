using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IDocumentDetailService
    {
        List<actblTaxDocument_Detail> GetAllDocumentDetails();
        void CreateDocumentDetail(actblTaxDocument_Detail objectEntry);
        void UpdateDocumentDetail(actblTaxDocument_Detail objectEntry);
        void RemoveDocumentDetail(actblTaxDocument_Detail objectEntry);
        void SaveChanges();
    }
}
