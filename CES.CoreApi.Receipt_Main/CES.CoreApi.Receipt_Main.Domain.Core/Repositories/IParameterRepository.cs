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
    public interface IParameterRepository
    {
        systblApp_TaxReceipt_Parameter find(string id);
        IEnumerable<systblApp_TaxReceipt_Parameter> find(Expression<Func<systblApp_TaxReceipt_Parameter, bool>> where);
        void CreateParameter(systblApp_TaxReceipt_Parameter obj);
        void UpdateParameter(systblApp_TaxReceipt_Parameter obj);
        void RemoveParameter(systblApp_TaxReceipt_Parameter obj);
        void SaveChanges();
    }
}
