using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IEntityIdentificationRepository
    {
        EntityIdentification find(string id);
        IEnumerable<EntityIdentification> find(Expression<Func<EntityIdentification, bool>> where);
        void CreateEntityIdentification(EntityIdentification obj);
        void UpdateEntityIdentification(EntityIdentification obj);
        void RemoveEntityIdentification(EntityIdentification obj);
        void SaveChanges();
    }
}
