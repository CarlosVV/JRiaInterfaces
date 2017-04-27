using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IMenuService
    {
        List<Menu> GetAllMenus();
        void CreateMenu(Menu objectEntry);
        void UpdateMenu(Menu objectEntry);
        void RemoveMenu(Menu objectEntry);
        
    }
}
