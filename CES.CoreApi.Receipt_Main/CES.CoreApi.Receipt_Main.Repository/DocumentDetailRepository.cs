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
    public class DocumentDetailRepository : BaseRepository<systblApp_CoreAPI_DocumentDetail>, IDocumentDetailRepository
    {
        public DocumentDetailRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreAPI_DocumentDetail find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_CoreAPI_DocumentDetail> find(Expression<Func<systblApp_CoreAPI_DocumentDetail, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateDocumentDetail(systblApp_CoreAPI_DocumentDetail obj)
        {
            this.Add(obj);
        }
        public void UpdateDocumentDetail(systblApp_CoreAPI_DocumentDetail obj)
        {
            this.Update(obj);
        }
        public void RemoveDocumentDetail(systblApp_CoreAPI_DocumentDetail obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
