using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class UserService : IUserService
    {
        private IUserRepository repo;
        public UserService(IUserRepository repository)
        {
            repo = repository;
        }
        public List<systblApp_TaxReceipt_User> GetAllUsers()
        {
            return repo.find(user => !user.fDisabled.Value && !user.fDelete.Value).ToList();
        }
        public void CreateUser(systblApp_TaxReceipt_User objectEntry)
        {
            this.repo.CreateUser(objectEntry);          
        }
        public void UpdateUser(systblApp_TaxReceipt_User objectEntry)
        {
            this.repo.UpdateUser(objectEntry);          
        }
        public void RemoveUser(systblApp_TaxReceipt_User objectEntry)
        {
            this.repo.RemoveUser(objectEntry);           
        }
        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }
    }
}
