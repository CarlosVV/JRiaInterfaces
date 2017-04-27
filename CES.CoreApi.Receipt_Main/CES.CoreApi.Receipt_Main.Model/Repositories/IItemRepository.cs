using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Model.Repositories
{
    public interface IItemRepository
    {
        Item find(string id);
        IEnumerable<Item> find(Expression<Func<Item, bool>> where);
        void CreateItem(Item obj);
        void UpdateItem(Item obj);
        void RemoveItem(Item obj);
        void SaveChanges();
    }
}
