using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Services
{
    public interface IItemService
    {
        List<Item> GetAllItems();
        void CreateItem(Item objectEntry);
        void UpdateItem(Item objectEntry);
        void RemoveItem(Item objectEntry);
        
    }
}
