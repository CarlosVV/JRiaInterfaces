using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Repositories
{
    public interface IUserRoleRepository
    {
        systblApp_TaxReceipt_UserRole find(string id);
        IEnumerable<systblApp_TaxReceipt_UserRole> find(Expression<Func<systblApp_TaxReceipt_UserRole, bool>> where);
        void CreateUserRole(systblApp_TaxReceipt_UserRole obj);
        void UpdateUserRole(systblApp_TaxReceipt_UserRole obj);
        void RemoveUserRole(systblApp_TaxReceipt_UserRole obj);
        void SaveChanges();
    }
}
