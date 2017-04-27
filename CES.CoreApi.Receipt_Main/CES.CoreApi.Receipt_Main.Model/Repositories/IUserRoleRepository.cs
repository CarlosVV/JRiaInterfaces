using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IUserRoleRepository
    {
        UserRole find(string id);
        IEnumerable<UserRole> find(Expression<Func<UserRole, bool>> where);
        void CreateUserRole(UserRole obj);
        void UpdateUserRole(UserRole obj);
        void RemoveUserRole(UserRole obj);
        void SaveChanges();
    }
}
