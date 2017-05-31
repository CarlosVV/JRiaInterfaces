using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class DocumentService : IDocumentService
    {
        private IDocumentRepository repo;
        public DocumentService(IDocumentRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_CoreAPI_Document> GetAllDocuments()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public List<int> GetAllDocumentsFoliosByType(string doctype, DateTime? startDate, DateTime? endDate)
        {
            return repo.find(m => m.DocumentType.Equals(doctype) && (!startDate.HasValue || startDate.Value <= m.IssuedDate)
            && (!endDate.HasValue || endDate.Value > m.IssuedDate)).Select(p => p.Folio).ToList();
        }

        public void CreateDocument(systblApp_CoreAPI_Document objectEntry)
        {
            this.repo.CreateDocument(objectEntry);       
        }
        public void UpdateDocument(systblApp_CoreAPI_Document objectEntry)
        {
            this.repo.UpdateDocument(objectEntry);
        }
        public void RemoveDocument(systblApp_CoreAPI_Document objectEntry)
        {
            this.repo.RemoveDocument(objectEntry);            
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
