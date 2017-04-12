using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public class UserRepository : DbContext, IUserRepository
    {
        public virtual DbSet<User> Users { get; set; }
        public UserRepository()
            : base("name=main")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public User find(string id)
        {
            return Users.Find(id);
        }

        public IEnumerable<User> find(Func<User, bool> where)
        {
            return Users.Where(where);
        }
    }
}
