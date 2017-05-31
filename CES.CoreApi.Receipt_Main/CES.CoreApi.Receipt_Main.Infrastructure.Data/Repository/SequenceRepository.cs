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
    public class SequenceRepository : BaseRepository<systblApp_CoreApi_Sequence>, ISequenceRepository
    {
        public SequenceRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public systblApp_CoreApi_Sequence find(string entityName)
        {
            return this.Get(p => p.EntityName == entityName);
        }

        public IEnumerable<systblApp_CoreApi_Sequence> find(Expression<Func<systblApp_CoreApi_Sequence, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateSequence(systblApp_CoreApi_Sequence obj)
        {
            this.Add(obj);
        }
        public void UpdateSequence(systblApp_CoreApi_Sequence obj)
        {
            this.Update(obj);
        }
        public void RemoveSequence(systblApp_CoreApi_Sequence obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
