using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class ItemService : IItemService
    {
        private IItemRepository repo;
        public ItemService(IItemRepository repository)
        {
            repo = repository;
        }
        public List<Item> GetAllItems()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateItem(Item objectEntry)
        {
            this.repo.CreateItem(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateItem(Item objectEntry)
        {
            this.repo.UpdateItem(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveItem(Item objectEntry)
        {
            this.repo.RemoveItem(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
