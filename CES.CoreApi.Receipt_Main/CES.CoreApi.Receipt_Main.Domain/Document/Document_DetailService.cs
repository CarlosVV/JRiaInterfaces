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
    public class Document_DetailService : IDocument_DetailService
    {
        private IDocument_DetailRepository repo;
        public Document_DetailService(IDocument_DetailRepository repository)
        {
            repo = repository;
        }
        public List<Document_Detail> GetAllDocument_Details()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateDocument_Detail(Document_Detail objectEntry)
        {
            this.repo.CreateDocument_Detail(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateDocument_Detail(Document_Detail objectEntry)
        {
            this.repo.UpdateDocument_Detail(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveDocument_Detail(Document_Detail objectEntry)
        {
            this.repo.RemoveDocument_Detail(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
