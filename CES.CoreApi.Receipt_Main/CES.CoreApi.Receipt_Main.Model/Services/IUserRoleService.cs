using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IUserRoleService
    {
        List<systblApp_TaxReceipt_UserRole> GetAllUserRoles();
        void CreateUserRole(systblApp_TaxReceipt_UserRole objectEntry);
        void UpdateUserRole(systblApp_TaxReceipt_UserRole objectEntry);
        void RemoveUserRole(systblApp_TaxReceipt_UserRole objectEntry);
        
    }
}
