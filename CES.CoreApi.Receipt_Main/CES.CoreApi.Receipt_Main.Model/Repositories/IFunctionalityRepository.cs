using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IFunctionalityRepository
    {
        Functionality find(string id);
        IEnumerable<Functionality> find(Expression<Func<Functionality, bool>> where);
        void CreateFunctionality(Functionality obj);
        void UpdateFunctionality(Functionality obj);
        void RemoveFunctionality(Functionality obj);
        void SaveChanges();
    }
}
