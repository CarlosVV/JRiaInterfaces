using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Data.Repository
{
    public class UserRepository : BaseRepository<systblApp_TaxReceipt_User>, IUserRepository
    {
        public UserRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public void CreateUser(systblApp_TaxReceipt_User obj)
        {
            throw new NotImplementedException();
        }

        public systblApp_TaxReceipt_User find(string id)
        {
            return this.Get(p=> p.Id.ToString() == id);
        }

        public IEnumerable<systblApp_TaxReceipt_User> find(Expression<Func<systblApp_TaxReceipt_User, bool>> where)
        {
            return this.GetAll(where);
        }

        public void RemoveUser(systblApp_TaxReceipt_User obj)
        {
            this.RemoveUser(obj);
        }

        public void SaveChanges()
        {
            this.SaveChanges();
        }

        public void UpdateUser(systblApp_TaxReceipt_User obj)
        {
            this.UpdateUser(obj);
        }
    }
}
