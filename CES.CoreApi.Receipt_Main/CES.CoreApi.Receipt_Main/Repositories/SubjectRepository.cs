using CES.CoreApi.Receipt_Main.Models.DTOs;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Repositories
{
    public class SubjectRepository
    {
        private const string usp_SubjectInsert = "usp_SubjectInsert";
        private const string usp_SubjectUpdate = "usp_SubjectUpdate";
        private const string usp_SubjectDelete = "usp_SubjectDelete";
        private const string usp_SubjectSelect = "usp_SubjectSelect";
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();

        public virtual void Create(Subject entity)
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
    }
}