
using CES.CoreApi.Receipt_Main.Domain.Core.Activity;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Security;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Infrastructure.Data
{
    public partial class ReceiptDbContext : DbContext
    {
        public int CommandTimeout
        {
            get
            {
                int commandTimeout = 180;
                if(ConfigurationManager.AppSettings["CommandTimeout"] != null && int.TryParse(ConfigurationManager.AppSettings["CommandTimeout"], out commandTimeout))
                {
                    return commandTimeout;
                }
                return commandTimeout;
            }
        }
        public ReceiptDbContext()
            : base("name=receipt")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = CommandTimeout;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<systblApp_CoreApi_Sequence>().ToTable("systblApp_CoreApi_Sequence");
            modelBuilder.Entity<systblApp_CoreAPI_Caf>().ToTable("systblApp_CoreAPI_Caf");

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.ExemptAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.TaxAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .HasMany(e => e.DocumentDetails)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.DocumentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .HasMany(e => e.DocumentReferences)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.DocumentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .HasMany(e => e.TaskDetails)
                .WithOptional(e => e.Document)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .HasMany(e => e.Activity)
                .WithOptional(e => e.Document)
                .HasForeignKey(e => e.DocumentId);

            modelBuilder.Entity<systblApp_CoreAPI_DocumentDetail>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_DocumentDetail>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Task>()
                .HasMany(e => e.TaskDetail)
                .WithOptional(e => e.Task)
                .HasForeignKey(e => e.TaskId);

            modelBuilder.Entity<systblApp_CoreAPI_TaxEntity>()
                .HasMany(e => e.Senders)
                .WithRequired(e => e.Sender)
                .HasForeignKey(e => e.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<systblApp_CoreAPI_TaxEntity>()
                .HasMany(e => e.Receivers)
                .WithRequired(e => e.Receiver)
                .HasForeignKey(e => e.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<systblApp_CoreAPI_TaxEntity>()
                .HasMany(e => e.TaxAddresses)
                .WithOptional(e => e.TaxEntity)
                .HasForeignKey(e => e.TaxEntityId);

            modelBuilder.Entity<systblApp_TaxReceipt_Activity>()
                .Property(e => e.OrderNo)
                .IsFixedLength();

            modelBuilder.Entity<systblApp_TaxReceipt_Activity>()
                .Property(e => e.Folio)
                .IsFixedLength();

            modelBuilder.Entity<systblApp_TaxReceipt_Activity>()
                .HasMany(e => e.ActivityDetail)
                .WithOptional(e => e.TaxReceipt_Activity)
                .HasForeignKey(e => e.ActivityId);

            modelBuilder.Entity<systblApp_TaxReceipt_ActivityDetail>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<systblApp_TaxReceipt_Functionality>()
                .HasMany(e => e.FunctionRole)
                .WithOptional(e => e.Functionality)
                .HasForeignKey(e => e.FunctionalityId);

            modelBuilder.Entity<systblApp_TaxReceipt_Menu>()
                .HasMany(e => e.FunctionRole)
                .WithOptional(e => e.Menu)
                .HasForeignKey(e => e.MenuId);

            modelBuilder.Entity<systblApp_TaxReceipt_Menu>()
                .HasMany(e => e.Menus)
                .WithOptional(e => e.MenuParent)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<systblApp_TaxReceipt_ParameterCategory>()
                .HasMany(e => e.Parameter)
                .WithOptional(e => e.ParameterCategory)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<systblApp_TaxReceipt_Printer>()
                .HasMany(e => e.CashRegister)
                .WithOptional(e => e.Printer)
                .HasForeignKey(e => e.PrinterId);

            modelBuilder.Entity<systblApp_TaxReceipt_Role>()
                .HasMany(e => e.FunctionRole)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<systblApp_TaxReceipt_Role>()
                .HasMany(e => e.UserRole)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<systblApp_TaxReceipt_Store>()
                .HasMany(e => e.CashRegister)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.StoreId);

            modelBuilder.Entity<systblApp_TaxReceipt_Store>()
                .HasMany(e => e.Printer)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.StoreId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<systblApp_TaxReceipt_Store>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Store)
                .HasForeignKey(e => e.StoreId);

            modelBuilder.Entity<systblApp_TaxReceipt_User>()
                .HasMany(e => e.UserRoles)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
    }
}
