namespace WpfLocalDb.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Configuration;
    using System.Data.Entity.Infrastructure;

    public partial class TaxDb : DbContext
    {
        public int CommandTimeout
        {
            get
            {
                int commandTimeout = 180;
                if (ConfigurationManager.AppSettings["CommandTimeout"] != null && int.TryParse(ConfigurationManager.AppSettings["CommandTimeout"], out commandTimeout))
                {
                    return commandTimeout;
                }
                return commandTimeout;
            }
        }
        public TaxDb()
            : base("name=TaxDb")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = CommandTimeout;
        }
        public virtual DbSet<systblApp_CoreApi_Sequence> systblApp_CoreApi_Sequence { get; set; }
        public virtual DbSet<systblApp_CoreAPI_Caf> systblApp_CoreAPI_Caf { get; set; }
        public virtual DbSet<systblApp_CoreAPI_Document> systblApp_CoreAPI_Document { get; set; }
        public virtual DbSet<systblApp_CoreAPI_DocumentDetail> systblApp_CoreAPI_DocumentDetail { get; set; }
        public virtual DbSet<systblApp_CoreAPI_DocumentReference> systblApp_CoreAPI_DocumentReference { get; set; }
        public virtual DbSet<systblApp_CoreAPI_Task> systblApp_CoreAPI_Task { get; set; }
        public virtual DbSet<systblApp_CoreAPI_TaskDetail> systblApp_CoreAPI_TaskDetail { get; set; }
        public virtual DbSet<systblApp_CoreAPI_TaxAddress> systblApp_CoreAPI_TaxAddress { get; set; }
        public virtual DbSet<systblApp_CoreAPI_TaxEntity> systblApp_CoreAPI_TaxEntity { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_Activity> systblApp_TaxReceipt_Activity { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_ActivityDetail> systblApp_TaxReceipt_ActivityDetail { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_CashRegister> systblApp_TaxReceipt_CashRegister { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_Functionality> systblApp_TaxReceipt_Functionality { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_FunctionRole> systblApp_TaxReceipt_FunctionRole { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_Menu> systblApp_TaxReceipt_Menu { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_Parameter> systblApp_TaxReceipt_Parameter { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_ParameterCategory> systblApp_TaxReceipt_ParameterCategory { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_Printer> systblApp_TaxReceipt_Printer { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_Role> systblApp_TaxReceipt_Role { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_Store> systblApp_TaxReceipt_Store { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_User> systblApp_TaxReceipt_User { get; set; }
        public virtual DbSet<systblApp_TaxReceipt_UserRole> systblApp_TaxReceipt_UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
