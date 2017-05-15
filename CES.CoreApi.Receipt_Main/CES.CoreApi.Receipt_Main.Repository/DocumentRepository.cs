using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class DocumentRepository : BaseRepository<systblApp_CoreAPI_Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreAPI_Document find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_CoreAPI_Document> find(Expression<Func<systblApp_CoreAPI_Document, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateDocument(systblApp_CoreAPI_Document obj)
        {
            this.Add(obj);
        }
        public void UpdateDocument(systblApp_CoreAPI_Document obj)
        {
            this.Update(obj);
        }
        public void RemoveDocument(systblApp_CoreAPI_Document obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
