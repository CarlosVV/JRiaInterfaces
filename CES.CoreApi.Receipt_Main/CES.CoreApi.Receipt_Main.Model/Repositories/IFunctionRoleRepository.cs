using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IFunctionRoleRepository
    {
        systblApp_TaxReceipt_FunctionRole find(string id);
        IEnumerable<systblApp_TaxReceipt_FunctionRole> find(Expression<Func<systblApp_TaxReceipt_FunctionRole, bool>> where);
        void CreateFunctionRole(systblApp_TaxReceipt_FunctionRole obj);
        void UpdateFunctionRole(systblApp_TaxReceipt_FunctionRole obj);
        void RemoveFunctionRole(systblApp_TaxReceipt_FunctionRole obj);
        void SaveChanges();
    }
}
