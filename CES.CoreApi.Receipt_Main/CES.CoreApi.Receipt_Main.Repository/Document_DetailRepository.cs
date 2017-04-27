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
    public class Document_DetailRepository : BaseRepository<Document_Detail>, IDocument_DetailRepository
    {
        public Document_DetailRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Document_Detail find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Document_Detail> find(Expression<Func<Document_Detail, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateDocument_Detail(Document_Detail obj)
        {
            this.Add(obj);
        }
        public void UpdateDocument_Detail(Document_Detail obj)
        {
            this.Update(obj);
        }
        public void RemoveDocument_Detail(Document_Detail obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
