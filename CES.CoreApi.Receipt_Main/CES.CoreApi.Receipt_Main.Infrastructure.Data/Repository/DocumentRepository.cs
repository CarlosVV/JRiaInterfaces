using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository
{
    public class DocumentRepository : BaseRepository<actblTaxDocument>, IDocumentRepository
    {
        public DocumentRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public actblTaxDocument find(string id)
        {
            return this.Get(p => p.fDocumentID.ToString() == id);
        }

        public IEnumerable<actblTaxDocument> find(Expression<Func<actblTaxDocument, bool>> where)
        {
            return this.GetWhere(where);
        }

        public void CreateDocument(actblTaxDocument obj)
        {
            this.Add(obj);
        }
        public void UpdateDocument(actblTaxDocument obj)
        {
            this.Update(obj);
        }
        public void RemoveDocument(actblTaxDocument obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
