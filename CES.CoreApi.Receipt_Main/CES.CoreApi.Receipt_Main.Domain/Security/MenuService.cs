using CES.CoreApi.Receipt_Main.Model;
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
    public class MenuService : IMenuService
    {
        private IMenuRepository repo;
        public MenuService(IMenuRepository repository)
        {
            repo = repository;
        }
        public List<Menu> GetAllMenus()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateMenu(Menu objectEntry)
        {
            this.repo.CreateMenu(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateMenu(Menu objectEntry)
        {
            this.repo.UpdateMenu(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveMenu(Menu objectEntry)
        {
            this.repo.RemoveMenu(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
