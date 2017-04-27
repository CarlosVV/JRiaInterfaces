using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IRoleRepository
    {
        Role find(string id);
        IEnumerable<Role> find(Expression<Func<Role, bool>> where);
        void CreateRole(Role obj);
        void UpdateRole(Role obj);
        void RemoveRole(Role obj);
        void SaveChanges();
    }
}
