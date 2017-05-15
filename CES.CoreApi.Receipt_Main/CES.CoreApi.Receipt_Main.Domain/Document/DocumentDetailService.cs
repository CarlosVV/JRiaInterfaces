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
    public class DocumentDetailService : IDocumentDetailService
    {
        private IDocumentDetailRepository repo;
        public DocumentDetailService(IDocumentDetailRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_CoreAPI_DocumentDetail> GetAllDocumentDetails()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry)
        {
            this.repo.CreateDocumentDetail(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry)
        {
            this.repo.UpdateDocumentDetail(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveDocumentDetail(systblApp_CoreAPI_DocumentDetail objectEntry)
        {
            this.repo.RemoveDocumentDetail(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
