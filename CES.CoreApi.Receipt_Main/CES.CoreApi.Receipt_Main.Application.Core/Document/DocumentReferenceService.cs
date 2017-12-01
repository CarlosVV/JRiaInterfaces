using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;

namespace CES.CoreApi.Receipt_Main.Application.Core.Document
{
    public class DocumentReferenceService : IDocumentReferenceService
    {
        private IDocumentReferenceRepository repo;
        public DocumentReferenceService(IDocumentReferenceRepository repository)
        {
            repo = repository;
        }

        public void CreateDocumentReference(actblTaxDocument_Reference objectEntry)
        {
            throw new NotImplementedException();
        }

        public List<actblTaxDocument_Reference> GetAllDocumentReferences()
        {
            return repo.find(c => !c.fDisabled && !c.fDelete).ToList();
        }

        public void RemoveDocumentReference(actblTaxDocument_Reference objectEntry)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateDocumentReference(actblTaxDocument_Reference objectEntry)
        {
            throw new NotImplementedException();
        }

        List<actblTaxDocument_Reference> IDocumentReferenceService.GetAllDocumentReferences()
        {
            throw new NotImplementedException();
        }
    }
}
