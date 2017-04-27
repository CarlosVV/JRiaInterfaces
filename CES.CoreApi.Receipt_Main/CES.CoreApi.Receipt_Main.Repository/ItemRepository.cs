using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public Item find(string id)
        {
            return this.Get(p => p.Id.ToString() == id);
        }

        public IEnumerable<Item> find(Expression<Func<Item, bool>> where)
        {
            return this.GetAll(where);
        }

        public void CreateItem(Item obj)
        {
            this.Add(obj);
        }
        public void UpdateItem(Item obj)
        {
            this.Update(obj);
        }
        public void RemoveItem(Item obj)
        {
            this.Delete(obj);
        }
        public void SaveChanges()
        {
            this.Save();
        }   
    }
}
