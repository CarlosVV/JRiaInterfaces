using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IIdentificationTypeRepository
    {
        IdentificationType find(string id);
        IEnumerable<IdentificationType> find(Expression<Func<IdentificationType, bool>> where);
        void CreateIdentificationType(IdentificationType obj);
        void UpdateIdentificationType(IdentificationType obj);
        void RemoveIdentificationType(IdentificationType obj);
        void SaveChanges();
    }
}
