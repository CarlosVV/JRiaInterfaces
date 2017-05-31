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
    public class EntityIdentificationRepository : BaseRepository<EntityIdentification>, IEntityIdentificationRepository
    {
        public EntityIdentificationRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public EntityIdentification find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<EntityIdentification> find(Expression<Func<EntityIdentification, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateEntityIdentification(EntityIdentification obj)
        {
            this.Add(obj);
        }
        public void UpdateEntityIdentification(EntityIdentification obj)
        {
            this.Update(obj);
        }
        public void RemoveEntityIdentification(EntityIdentification obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
