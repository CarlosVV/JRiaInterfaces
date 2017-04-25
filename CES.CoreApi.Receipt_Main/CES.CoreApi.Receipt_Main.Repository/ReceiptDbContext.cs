using CES.CoreApi.Receipt_Main.Model;
using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Repository
{
    public partial class ReceiptDbContext : DbContext
    {
        public ReceiptDbContext()
            : base("name=main")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Caf>().ToTable("systblApp_CoreAPI_Caf");
        }
    }
}
