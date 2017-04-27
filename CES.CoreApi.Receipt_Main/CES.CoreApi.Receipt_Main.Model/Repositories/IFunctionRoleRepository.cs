using CES.CoreApi.Receipt_Main.Model.Documents;
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
        FunctionRole find(string id);
        IEnumerable<FunctionRole> find(Expression<Func<FunctionRole, bool>> where);
        void CreateFunctionRole(FunctionRole obj);
        void UpdateFunctionRole(FunctionRole obj);
        void RemoveFunctionRole(FunctionRole obj);
        void SaveChanges();
    }
}
