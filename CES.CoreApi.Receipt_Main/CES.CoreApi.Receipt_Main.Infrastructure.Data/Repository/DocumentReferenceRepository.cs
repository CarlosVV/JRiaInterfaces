using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository;
using CES.CoreApi.Receipt_Main.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace CES.CoreApi.Receipt_Main.Repository.Repository
{
    public class DocumentReferenceRepository : BaseRepository<actblTaxDocument_Reference>, IDocumentReferenceRepository
    {
        public DocumentReferenceRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public void CreateDocumentReference(actblTaxDocument_Reference obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<actblTaxDocument_Reference> find(Expression<Func<actblTaxDocument_Reference, bool>> where)
        {
            throw new NotImplementedException();
        }

        public actblTaxDocument_Reference find(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveDocumentReference(actblTaxDocument_Reference obj)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateDocumentReference(actblTaxDocument_Reference obj)
        {
            throw new NotImplementedException();
        }
    }
}
