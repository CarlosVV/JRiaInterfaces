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
    public class DocumentDetailRepository : BaseRepository<actblTaxDocument_Detail>, IDocumentDetailRepository
    {
        public DocumentDetailRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public actblTaxDocument_Detail find(string id)
        {
            return this.Get(p => p.fDetailID.ToString() == id);
        }

        public IEnumerable<actblTaxDocument_Detail> find(Expression<Func<actblTaxDocument_Detail, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateDocumentDetail(actblTaxDocument_Detail obj)
        {
            this.Add(obj);
        }
        public void UpdateDocumentDetail(actblTaxDocument_Detail obj)
        {
            this.Update(obj);
        }
        public void RemoveDocumentDetail(actblTaxDocument_Detail obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
