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
    public interface IRoleRepository
    {
        systblApp_TaxReceipt_Role find(string id);
        IEnumerable<systblApp_TaxReceipt_Role> find(Expression<Func<systblApp_TaxReceipt_Role, bool>> where);
        void CreateRole(systblApp_TaxReceipt_Role obj);
        void UpdateRole(systblApp_TaxReceipt_Role obj);
        void RemoveRole(systblApp_TaxReceipt_Role obj);
        void SaveChanges();
    }
}
