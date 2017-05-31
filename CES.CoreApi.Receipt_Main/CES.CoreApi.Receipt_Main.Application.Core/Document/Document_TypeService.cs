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
    public class Document_TypeService : IDocument_TypeService
    {
        private IDocument_TypeRepository repo;
        public Document_TypeService(IDocument_TypeRepository repository)
        {
            repo = repository;
        }
        public List<Document_Type> GetAllDocument_Types()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateDocument_Type(Document_Type objectEntry)
        {
            this.repo.CreateDocument_Type(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateDocument_Type(Document_Type objectEntry)
        {
            this.repo.UpdateDocument_Type(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveDocument_Type(Document_Type objectEntry)
        {
            this.repo.RemoveDocument_Type(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
