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
    public class UserRoleService : IUserRoleService
    {
        private IUserRoleRepository repo;
        public UserRoleService(IUserRoleRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_UserRole> GetAllUserRoles()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateUserRole(systblApp_TaxReceipt_UserRole objectEntry)
        {
            this.repo.CreateUserRole(objectEntry);
          
        }
        public void UpdateUserRole(systblApp_TaxReceipt_UserRole objectEntry)
        {
            this.repo.UpdateUserRole(objectEntry);
           
        }
        public void RemoveUserRole(systblApp_TaxReceipt_UserRole objectEntry)
        {
            this.repo.RemoveUserRole(objectEntry);
           
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
