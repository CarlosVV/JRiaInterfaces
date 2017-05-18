using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IDocumentService
    {
        List<systblApp_CoreAPI_Document> GetAllDocuments();
        List<int> GetAllDocumentsFoliosByType(string doctype, DateTime? startDate, DateTime? endDate);
        void CreateDocument(systblApp_CoreAPI_Document objectEntry);
        void UpdateDocument(systblApp_CoreAPI_Document objectEntry);
        void RemoveDocument(systblApp_CoreAPI_Document objectEntry);
        
    }
}
