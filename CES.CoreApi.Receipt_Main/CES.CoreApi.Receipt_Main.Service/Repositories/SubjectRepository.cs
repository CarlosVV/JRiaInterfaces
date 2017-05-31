using CES.CoreApi.Receipt_Main.Service.Model.Documents;
using CES.CoreApi.Receipt_Main.Service.Models.DTOs;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Repositories
{
    public class SubjectRepository
    {
        private const string usp_SubjectInsert = "coreapi_sp_systblApp_CoreAPI_TaxEntity_Create";
        private const string usp_SubjectUpdate = "coreapi_sp_systblApp_CoreAPI_TaxEntity_Update";
        private const string usp_SubjectDelete = "coreapi_sp_systblApp_CoreAPI_TaxEntity_Delete";
        private const string usp_SubjectSelect = "coreapi_sp_systblApp_CoreAPI_TaxEntity_Get";
        private const string usp_AddressCreate = "coreapi_sp_systblApp_CoreAPI_TaxAddress_Create";
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();

        public virtual void CreateTaxEntity(Subject entity)
        {
            //@Id, @FirstName, @MiddleName, @LastName1, @LastName2, @FullName, @Gender, @Occupation, 
            //@DateOfBirth, @Nationality, @CountryOfBirth, @Phone, @CellPhone, @Email, @EconomicActivity, 
            //@ModifiedBy, @CreatedBy, @CreatedOn, @ModifiedOn
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, usp_SubjectInsert))
            {
                entity.Id = Guid.NewGuid().ToString();

                sql.AddParam("@Id", entity.Id);
                sql.AddParam("@FirstName", entity.FirstName);
                sql.AddParam("@MiddleName", entity.MidleName);
                sql.AddParam("@LastName1", entity.LastName1);
                sql.AddParam("@LastName2", entity.LastName2);
                sql.AddParam("@FullName", entity.FullName);
                sql.AddParam("@Gender", entity.Gender);
                sql.AddParam("@Occupation", entity.Ocupation);
                sql.AddParam("@DateOfBirth", entity.DateOfBirth);
                sql.AddParam("@Nationality", entity.Nationality);
                sql.AddParam("@CountryOfBirth", entity.CountryOfBirth);
                sql.AddParam("@Phone", entity.Phone);
                sql.AddParam("@CellPhone", entity.CellPhone);
                sql.AddParam("@Email", entity.Email);
                sql.AddParam("@EconomicActivity", entity.EconomicActivity);
                sql.AddParam("@ModifiedBy", entity.ModifiedBy);
                sql.AddParam("@CreatedBy", entity.CreatedBy);
                sql.AddParam("@CreatedOn", entity.ModifiedOn);
                sql.AddParam("@ModifiedOn", entity.ModifiedOn);

                sql.Execute();
            }
        }

        public virtual void CreateAddress(Address entity)
        {
            //using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, usp_SubjectInsert))
            //{
            //    entity.Id = Guid.NewGuid().ToString();

            //    sql.AddParam("@Id", entity.Id);
            //    sql.AddParam("@Entity", entity.FirstName);
            //    sql.AddParam("@Address", entity.MidleName);
            //    sql.AddParam("@LastName1", entity.LastName1);
            //    sql.AddParam("@LastName2", entity.LastName2);
            //    sql.AddParam("@FullName", entity.FullName);
            //    sql.AddParam("@Gender", entity.Gender);
            //    sql.AddParam("@Occupation", entity.Ocupation);
            //    sql.AddParam("@DateOfBirth", entity.DateOfBirth);
            //    sql.AddParam("@Nationality", entity.Nationality);
            //    sql.AddParam("@CountryOfBirth", entity.CountryOfBirth);
            //    sql.AddParam("@Phone", entity.Phone);
            //    sql.AddParam("@CellPhone", entity.CellPhone);
            //    sql.AddParam("@Email", entity.Email);
            //    sql.AddParam("@EconomicActivity", entity.EconomicActivity);
            //    sql.AddParam("@ModifiedBy", entity.ModifiedBy);
            //    sql.AddParam("@CreatedBy", entity.CreatedBy);
            //    sql.AddParam("@CreatedOn", entity.ModifiedOn);
            //    sql.AddParam("@ModifiedOn", entity.ModifiedOn);

            //    sql.Execute();
            //}
        }
    }
}