using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext _dbContext)
            : base(_dbContext)
        {
        }

        public void CreateUser(User obj)
        {
            throw new NotImplementedException();
        }

        public User find(string id)
        {
            return this.Get(p=> p.Id.ToString() == id);
        }

        public IEnumerable<User> find(Expression<Func<User, bool>> where)
        {
            return this.GetAll(where);
        }

        public void RemoveUser(User obj)
        {
            this.RemoveUser(obj);
        }

        public void SaveChanges()
        {
            this.SaveChanges();
        }

        public void UpdateUser(User obj)
        {
            this.UpdateUser(obj);
        }
    }
}
