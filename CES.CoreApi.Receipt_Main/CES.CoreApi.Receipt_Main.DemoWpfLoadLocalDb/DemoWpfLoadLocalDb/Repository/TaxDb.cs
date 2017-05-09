namespace WpfLocalDb.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TaxDb : DbContext
    {
        public TaxDb()
            : base("name=TaxDb")
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityDetail> ActivityDetails { get; set; }
        public virtual DbSet<ActivityStatu> ActivityStatus { get; set; }
        public virtual DbSet<ActivityType> ActivityTypes { get; set; }
        public virtual DbSet<Cashier> Cashiers { get; set; }
        public virtual DbSet<Functionality> Functionalities { get; set; }
        public virtual DbSet<FunctionRole> FunctionRoles { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ParameterCategory> ParameterCategories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<systblApp_CoreAPI_Caf> systblApp_CoreAPI_Caf { get; set; }
        public virtual DbSet<systblApp_CoreAPI_Document> systblApp_CoreAPI_Document { get; set; }
        public virtual DbSet<systblApp_CoreAPI_Document_Detail> systblApp_CoreAPI_Document_Detail { get; set; }
        public virtual DbSet<systblApp_CoreAPI_Task> systblApp_CoreAPI_Task { get; set; }
        public virtual DbSet<systblApp_CoreAPI_TaskDetail> systblApp_CoreAPI_TaskDetail { get; set; }
        public virtual DbSet<systblApp_CoreAPI_TaxAddress> systblApp_CoreAPI_TaxAddress { get; set; }
        public virtual DbSet<systblApp_CoreAPI_TaxEntity> systblApp_CoreAPI_TaxEntity { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .Property(e => e.OrderNo)
                .IsFixedLength();

            modelBuilder.Entity<Activity>()
                .Property(e => e.DocumentId)
                .IsFixedLength();

            modelBuilder.Entity<Activity>()
                .Property(e => e.Folio)
                .IsFixedLength();

            modelBuilder.Entity<ActivityDetail>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<ActivityStatu>()
                .HasMany(e => e.Activities)
                .WithOptional(e => e.ActivityStatu)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Menu1)
                .WithOptional(e => e.Menu2)
                .HasForeignKey(e => e.Parent);

            modelBuilder.Entity<ParameterCategory>()
                .HasMany(e => e.Parameters)
                .WithOptional(e => e.ParameterCategory)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.ExemptAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.Tax)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document_Detail>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<systblApp_CoreAPI_Document_Detail>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);
        }
    }
}
