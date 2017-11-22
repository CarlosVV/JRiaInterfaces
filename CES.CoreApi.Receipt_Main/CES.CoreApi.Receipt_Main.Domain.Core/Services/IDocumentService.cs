using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IDocumentService
    {
        List<actblTaxDocument> GetAllDocuments();
        List<string> GetAllDocumentsFoliosByType(string doctype, DateTime? startDate, DateTime? endDate);
        void CreateDocument(actblTaxDocument objectEntry);
        void UpdateDocument(actblTaxDocument objectEntry);
        void RemoveDocument(actblTaxDocument objectEntry);
        void SaveChanges();
        DocumentResultSet GetOrderInfo(int? orderId, string orderNo);
    }
}
