using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IMenuService
    {
        List<systblApp_TaxReceipt_Menu> GetAllMenus();
        void CreateMenu(systblApp_TaxReceipt_Menu objectEntry);
        void UpdateMenu(systblApp_TaxReceipt_Menu objectEntry);
        void RemoveMenu(systblApp_TaxReceipt_Menu objectEntry);
        
    }
}
