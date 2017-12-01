
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
            : base("name=taxDocument")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = CommandTimeout;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //// modelBuilder.Entity<actblTaxDocument_TableSeq>().ToTable("systblApp_CoreApi_Sequence");
            //modelBuilder.Entity<actblTaxDocument_AuthCode>().ToTable("systblApp_CoreAPI_Caf");

            //modelBuilder.Entity<actblTaxDocument>()
            //    .Property(e => e.fExemptAmount)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument>()
            //    .Property(e => e.fAmount)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument>()
            //    .Property(e => e.fTaxAmount)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument>()
            //    .Property(e => e.fTotalAmount)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument>()
            //    .HasMany(e => e.DocumentDetails)
            //    .WithRequired(e => e.Document)
            //    .HasForeignKey(e => e.fDocumentID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument>()
            //    .HasMany(e => e.DocumentReferences)
            //    .WithRequired(e => e.Document)
            //    .HasForeignKey(e => e.fDocumentID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument>()
            //    .HasMany(e => e.TaskDetails)
            //    .WithOptional(e => e.Document)
            //    .HasForeignKey(e => e.fTaskID);

            ////modelBuilder.Entity<actblTaxDocument>()
            ////    .HasMany(e => e.Activity)
            ////    .WithOptional(e => e.Document)
            ////    .HasForeignKey(e => e.DocumentId);

            //modelBuilder.Entity<actblTaxDocument_Detail>()
            //    .Property(e => e.fPrice)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_Detail>()
            //    .Property(e => e.fAmount)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<systblApp_CoreAPI_Task>()
            //    .HasMany(e => e.TaskDetail)
            //    .WithOptional(e => e.Task)
            //    .HasForeignKey(e => e.fTaskID);

            //modelBuilder.Entity<actblTaxDocument_Entity>()
            //    .HasMany(e => e.EntityFroms)
            //    .WithRequired(e => e.EntityFrom)
            //    .HasForeignKey(e => e.fEntityFromID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument_Entity>()
            //    .HasMany(e => e.EntityTos)
            //    .WithRequired(e => e.EntityTo)
            //    .HasForeignKey(e => e.fEntityToID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument_Entity>()
            //    .HasMany(e => e.TaxAddresses)
            //    .WithOptional(e => e.TaxEntity)
            //    .HasForeignKey(e => e.fEntityID);

            //modelBuilder.Entity<systblApp_TaxReceipt_Activity>()
            //    .Property(e => e.OrderNo)
            //    .IsFixedLength();

            //modelBuilder.Entity<systblApp_TaxReceipt_Activity>()
            //    .Property(e => e.Folio)
            //    .IsFixedLength();

            //modelBuilder.Entity<systblApp_TaxReceipt_Activity>()
            //    .HasMany(e => e.ActivityDetail)
            //    .WithOptional(e => e.TaxReceipt_Activity)
            //    .HasForeignKey(e => e.ActivityId);

            //modelBuilder.Entity<systblApp_TaxReceipt_ActivityDetail>()
            //    .Property(e => e.Notes)
            //    .IsUnicode(false);

            //modelBuilder.Entity<systblApp_TaxReceipt_Functionality>()
            //    .HasMany(e => e.FunctionRole)
            //    .WithOptional(e => e.Functionality)
            //    .HasForeignKey(e => e.FunctionalityId);

            //modelBuilder.Entity<systblApp_TaxReceipt_Menu>()
            //    .HasMany(e => e.FunctionRole)
            //    .WithOptional(e => e.Menu)
            //    .HasForeignKey(e => e.MenuId);

            //modelBuilder.Entity<systblApp_TaxReceipt_Menu>()
            //    .HasMany(e => e.Menus)
            //    .WithOptional(e => e.MenuParent)
            //    .HasForeignKey(e => e.ParentId);

            //modelBuilder.Entity<systblApp_TaxReceipt_ParameterCategory>()
            //    .HasMany(e => e.Parameter)
            //    .WithOptional(e => e.ParameterCategory)
            //    .HasForeignKey(e => e.CategoryId);

            //modelBuilder.Entity<systblApp_TaxReceipt_Printer>()
            //    .HasMany(e => e.CashRegister)
            //    .WithOptional(e => e.Printer)
            //    .HasForeignKey(e => e.PrinterId);

            //modelBuilder.Entity<systblApp_TaxReceipt_Role>()
            //    .HasMany(e => e.FunctionRole)
            //    .WithOptional(e => e.Role)
            //    .HasForeignKey(e => e.RoleId);

            //modelBuilder.Entity<systblApp_TaxReceipt_Role>()
            //    .HasMany(e => e.UserRole)
            //    .WithOptional(e => e.Role)
            //    .HasForeignKey(e => e.RoleId);

            //modelBuilder.Entity<systblApp_TaxReceipt_Store>()
            //    .HasMany(e => e.CashRegister)
            //    .WithOptional(e => e.Store)
            //    .HasForeignKey(e => e.StoreId);

            //modelBuilder.Entity<systblApp_TaxReceipt_Store>()
            //    .HasMany(e => e.Printer)
            //    .WithRequired(e => e.Store)
            //    .HasForeignKey(e => e.StoreId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<systblApp_TaxReceipt_Store>()
            //    .HasMany(e => e.Users)
            //    .WithOptional(e => e.Store)
            //    .HasForeignKey(e => e.StoreId);

            //modelBuilder.Entity<systblApp_TaxReceipt_User>()
            //    .HasMany(e => e.UserRoles)
            //    .WithOptional(e => e.User)
            //    .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fTransactionNo)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fAuthorizationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fCashierName)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fExemptAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fTaxAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fTotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<actblTaxDocument>()
                .Property(e => e.fXmlDocumentContent)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument>()
                .HasMany(e => e.DocumentDetails)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<actblTaxDocument>()
                .HasMany(e => e.DocumentReferences)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument>()
            //    .HasMany(e => e.actblTaxDocument_StatusChange_Log)
            //    .WithRequired(e => e.actblTaxDocument)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<actblTaxDocument_AuthCode>()
                .Property(e => e.fCompanyTaxID)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_AuthCode>()
                .Property(e => e.fCurrentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_AuthCode>()
                .Property(e => e.fStartNumber)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_AuthCode>()
                .Property(e => e.fEndNumber)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_AuthCode>()
                .Property(e => e.fFileContent)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_Detail>()
                .Property(e => e.fPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<actblTaxDocument_Detail>()
                .Property(e => e.fAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .Property(e => e.fTaxID)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .Property(e => e.fNationality)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .Property(e => e.fCountryOfBirth)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .Property(e => e.fPhone)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .Property(e => e.fCellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .HasMany(e => e.EntityFroms)
                .WithRequired(e => e.EntityFrom)
                .HasForeignKey(e => e.fEntityFromID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .HasMany(e => e.EntityTos)
                .WithRequired(e => e.EntityTo)
                .HasForeignKey(e => e.fEntityToID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<actblTaxDocument_Entity>()
                .HasMany(e => e.TaxAddresses)
                .WithRequired(e => e.TaxEntity)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fAccountTotalAmountVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fAccountTotalVoidAmountVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fAccountResultTotalAmountVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fIRSTotalAmountVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fIRSTotalAmountVATNotInIRS)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fIRSTotalAmountIRSNotInVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fIRSTotalVoidCurrentMonthAmountVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fIRSTotalOtherMonthsVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fIRSStoreTotalAmountCalculatedVAT)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .Property(e => e.fTotalDifferenceAmount)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .HasMany(e => e.actblTaxDocument_VAT_Detail)
            //    .WithRequired(e => e.actblTaxDocument_VAT)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument_VAT>()
            //    .HasMany(e => e.actblTaxDocument_VAT_ProcessDetail)
            //    .WithRequired(e => e.actblTaxDocument_VAT)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<actblTaxDocument_VAT_Detail>()
            //    .Property(e => e.fJENo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<actblTaxDocument_VAT_Detail>()
            //    .Property(e => e.fDepartmentAbbrev)
            //    .IsUnicode(false);

            //modelBuilder.Entity<actblTaxDocument_VAT_Detail>()
            //    .Property(e => e.fJEDesc)
            //    .IsUnicode(false);

            //modelBuilder.Entity<actblTaxDocument_VAT_Detail>()
            //    .Property(e => e.fJEDetAmount2)
            //    .HasPrecision(19, 4);

            //modelBuilder.Entity<actblTaxDocument_VAT_Detail>()
            //    .Property(e => e.fCurrencyBase)
            //    .IsUnicode(false);

            //modelBuilder.Entity<actblTaxDocument_VAT_Detail>()
            //    .Property(e => e.fRefNo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<actblTaxDocument_VAT_ProcessDetail>()
            //    .Property(e => e.fSource)
            //    .IsUnicode(false);

            modelBuilder.Entity<systblApp_CoreAPI_Task>()
                .Property(e => e.fRequestObject)
                .IsUnicode(false);

            modelBuilder.Entity<systblApp_CoreAPI_Task_Detail>()
                .Property(e => e.fStateObject)
                .IsUnicode(false);

            modelBuilder.Entity<systblApp_CoreAPI_Task_Detail>()
                .Property(e => e.fResultObject)
                .IsUnicode(false);
        }
    }
}
