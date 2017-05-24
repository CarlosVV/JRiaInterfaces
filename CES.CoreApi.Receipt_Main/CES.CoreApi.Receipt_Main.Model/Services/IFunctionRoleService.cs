using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IFunctionRoleService
    {
        List<systblApp_TaxReceipt_FunctionRole> GetAllFunctionRoles();
        void CreateFunctionRole(systblApp_TaxReceipt_FunctionRole objectEntry);
        void UpdateFunctionRole(systblApp_TaxReceipt_FunctionRole objectEntry);
        void RemoveFunctionRole(systblApp_TaxReceipt_FunctionRole objectEntry);
        void SaveChanges();
    }
}
