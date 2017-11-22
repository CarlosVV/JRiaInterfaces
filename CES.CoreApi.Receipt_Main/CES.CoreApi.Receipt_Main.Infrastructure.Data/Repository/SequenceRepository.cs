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
    public class SequenceRepository //: BaseRepository<actblTaxDocument_TableSeq>, ISequenceRepository
    {
       /* public SequenceRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }*/

       /* public actblTaxDocument_TableSeq find(string entityName)
        {
            return this.Get(p => p.fEntityName == entityName);
        }

        public IEnumerable<actblTaxDocument_TableSeq> find(Expression<Func<actblTaxDocument_TableSeq, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateSequence(actblTaxDocument_TableSeq obj)
        {
            this.Add(obj);
        }
        public void UpdateSequence(actblTaxDocument_TableSeq obj)
        {
            this.Update(obj);
        }
        public void RemoveSequence(actblTaxDocument_TableSeq obj)
        {
            this.Delete(obj);
        }*/
        public void SaveChanges()
        {
           // this.Save();
        }   
    }
}
