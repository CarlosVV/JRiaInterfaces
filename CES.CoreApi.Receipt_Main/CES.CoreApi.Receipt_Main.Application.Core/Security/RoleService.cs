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
