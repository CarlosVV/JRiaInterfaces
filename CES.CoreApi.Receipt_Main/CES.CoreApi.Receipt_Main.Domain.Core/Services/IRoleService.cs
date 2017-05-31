using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IRoleService
    {
        List<systblApp_TaxReceipt_Role> GetAllRoles();
        void CreateRole(systblApp_TaxReceipt_Role objectEntry);
        void UpdateRole(systblApp_TaxReceipt_Role objectEntry);
        void RemoveRole(systblApp_TaxReceipt_Role objectEntry);
        void SaveChanges();
    }
}
