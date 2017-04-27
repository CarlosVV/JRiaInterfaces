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
    public class Document_TypeRepository : BaseRepository<Document_Type>, IDocument_TypeRepository
    {
        public Document_TypeRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Document_Type find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Document_Type> find(Expression<Func<Document_Type, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateDocument_Type(Document_Type obj)
        {
            this.Add(obj);
        }
        public void UpdateDocument_Type(Document_Type obj)
        {
            this.Update(obj);
        }
        public void RemoveDocument_Type(Document_Type obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
