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
    public interface IFunctionalityRepository
    {
        systblApp_TaxReceipt_Functionality find(string id);
        IEnumerable<systblApp_TaxReceipt_Functionality> find(Expression<Func<systblApp_TaxReceipt_Functionality, bool>> where);
        void CreateFunctionality(systblApp_TaxReceipt_Functionality obj);
        void UpdateFunctionality(systblApp_TaxReceipt_Functionality obj);
        void RemoveFunctionality(systblApp_TaxReceipt_Functionality obj);
        void SaveChanges();
    }
}
