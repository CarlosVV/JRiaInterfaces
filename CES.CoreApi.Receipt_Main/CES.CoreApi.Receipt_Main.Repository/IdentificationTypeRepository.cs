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
    public class IdentificationTypeRepository : BaseRepository<IdentificationType>, IIdentificationTypeRepository
    {
        public IdentificationTypeRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public IdentificationType find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<IdentificationType> find(Expression<Func<IdentificationType, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateIdentificationType(IdentificationType obj)
        {
            this.Add(obj);
        }
        public void UpdateIdentificationType(IdentificationType obj)
        {
            this.Update(obj);
        }
        public void RemoveIdentificationType(IdentificationType obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
