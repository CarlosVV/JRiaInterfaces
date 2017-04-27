using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using CES.CoreApi.Receipt_Main.Model.Services;
using System.Collections.Generic;
using System.Linq;

namespace CES.CoreApi.Receipt_Main.Domain
{
    public class UserService : IUserService
    {
        private IUserRepository repo;
        public UserService(IUserRepository repository)
        {
            repo = repository;
        }
        public List<User> GetAllUsers()
        {
            return repo.find(user => !user.Disabled && !user.Deleted).ToList();
        }
        public void CreateUser(User objectEntry)
        {
            this.repo.CreateUser(objectEntry);
            this.repo.SaveChanges();
        }
        public void UpdateUser(User objectEntry)
        {
            this.repo.UpdateUser(objectEntry);
            this.repo.SaveChanges();
        }
        public void RemoveUser(User objectEntry)
        {
            this.repo.RemoveUser(objectEntry);
            this.repo.SaveChanges();
        }
    }
}
