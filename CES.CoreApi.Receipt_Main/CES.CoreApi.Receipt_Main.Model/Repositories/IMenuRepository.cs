using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IMenuRepository
    {
        systblApp_TaxReceipt_Menu find(string id);
        IEnumerable<systblApp_TaxReceipt_Menu> find(Expression<Func<systblApp_TaxReceipt_Menu, bool>> where);
        void CreateMenu(systblApp_TaxReceipt_Menu obj);
        void UpdateMenu(systblApp_TaxReceipt_Menu obj);
        void RemoveMenu(systblApp_TaxReceipt_Menu obj);
        void SaveChanges();
    }
}
