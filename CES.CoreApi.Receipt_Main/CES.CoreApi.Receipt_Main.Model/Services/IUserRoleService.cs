using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IUserRoleService
    {
        List<UserRole> GetAllUserRoles();
        void CreateUserRole(UserRole objectEntry);
        void UpdateUserRole(UserRole objectEntry);
        void RemoveUserRole(UserRole objectEntry);
        
    }
}
