using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IDocumentDetailService
    {
        List<systblApp_CoreAPI_DocumentDetail> GetAllDocumentDetails();
        void CreateDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry);
        void UpdateDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry);
        void RemoveDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry);
        void SaveChanges();
    }
}
