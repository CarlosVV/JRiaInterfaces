using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IFunctionRoleService
    {
        List<FunctionRole> GetAllFunctionRoles();
        void CreateFunctionRole(FunctionRole objectEntry);
        void UpdateFunctionRole(FunctionRole objectEntry);
        void RemoveFunctionRole(FunctionRole objectEntry);
        
    }
}
