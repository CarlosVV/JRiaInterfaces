using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface ICafRepository
    {
        Caf find(string id);
        IEnumerable<Caf> find(Expression<Func<Caf, bool>> where);
        void CreateCaf(Caf obj);
        void UpdateCaf(Caf obj);
        void RemoveCaf(Caf obj);
        void SaveChanges();
    }
}
