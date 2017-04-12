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
    }
}
