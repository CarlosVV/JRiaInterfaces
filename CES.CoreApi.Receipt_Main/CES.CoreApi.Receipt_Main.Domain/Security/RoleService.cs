using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Security;
using CES.CoreApi.Receipt_Main.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class RoleService : IRoleService
    {
        private IRoleRepository repo;
        public RoleService(IRoleRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_Role> GetAllRoles()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateRole(systblApp_TaxReceipt_Role objectEntry)
        {
            this.repo.CreateRole(objectEntry);
          
        }
        public void UpdateRole(systblApp_TaxReceipt_Role objectEntry)
        {
            this.repo.UpdateRole(objectEntry);
          
        }
        public void RemoveRole(systblApp_TaxReceipt_Role objectEntry)
        {
            this.repo.RemoveRole(objectEntry);
           
        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
