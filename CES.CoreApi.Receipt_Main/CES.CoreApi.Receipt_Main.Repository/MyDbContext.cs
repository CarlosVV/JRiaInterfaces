using CES.CoreApi.Receipt_Main.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=main")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
