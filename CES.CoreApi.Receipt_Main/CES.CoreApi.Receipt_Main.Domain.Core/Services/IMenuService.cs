using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Services
{
    public interface IMenuService
    {
        List<systblApp_TaxReceipt_Menu> GetAllMenus();
        void CreateMenu(systblApp_TaxReceipt_Menu objectEntry);
        void UpdateMenu(systblApp_TaxReceipt_Menu objectEntry);
        void RemoveMenu(systblApp_TaxReceipt_Menu objectEntry);
        void SaveChanges();
    }
}
