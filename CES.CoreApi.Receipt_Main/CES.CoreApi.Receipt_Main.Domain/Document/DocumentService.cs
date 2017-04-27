using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class DocumentService : IDocumentService
    {
        private IDocumentRepository repo;
        public DocumentService(IDocumentRepository repository)
        {
            repo = repository;
        }
        public List<Document> GetAllDocuments()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateDocument(Document objectEntry)
        {
            this.repo.CreateDocument(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateDocument(Document objectEntry)
        {
            this.repo.UpdateDocument(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveDocument(Document objectEntry)
        {
            this.repo.RemoveDocument(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
