using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class MenuService : IMenuService
    {
        private IMenuRepository repo;
        public MenuService(IMenuRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_Menu> GetAllMenus()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateMenu(systblApp_TaxReceipt_Menu objectEntry)
        {
            this.repo.CreateMenu(objectEntry);
         
        }
        public void UpdateMenu(systblApp_TaxReceipt_Menu objectEntry)
        {
            this.repo.UpdateMenu(objectEntry);
           
        }
        public void RemoveMenu(systblApp_TaxReceipt_Menu objectEntry)
        {
            this.repo.RemoveMenu(objectEntry);
          
        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
