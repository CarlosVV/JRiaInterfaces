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
    public class UserRoleService : IUserRoleService
    {
        private IUserRoleRepository repo;
        public UserRoleService(IUserRoleRepository repository)
        {
            repo = repository;
        }
        public List<UserRole> GetAllUserRoles()
        {
            return repo.find(c => !c.fDisabled.Value && !c.fDelete.Value).ToList();
        }

        public void CreateUserRole(UserRole objectEntry)
        {
            this.repo.CreateUserRole(objectEntry);
            this.repo.SaveChanges();          
        }
        public void UpdateUserRole(UserRole objectEntry)
        {
            this.repo.UpdateUserRole(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveUserRole(UserRole objectEntry)
        {
            this.repo.RemoveUserRole(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
