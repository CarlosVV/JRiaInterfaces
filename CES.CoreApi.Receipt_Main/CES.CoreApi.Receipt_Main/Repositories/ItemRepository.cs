using CES.CoreApi.Receipt_Main.Models.DTOs;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Repositories
{
    public class ItemRepository
    {
        private const string usp_ItemInsert = "usp_ItemInsert";
        private const string usp_ItemUpdate = "usp_ItemUpdate";
        private const string usp_ItemDelete = "usp_ItemDelete";
        private const string usp_ItemSelect = "usp_ItemSelect";
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();

        public virtual void Create(Item entity)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, usp_ItemInsert))
            {
                entity.ItemId = Guid.NewGuid().ToString();
              
                sql.AddParam("@Id", entity.ItemId);
                sql.AddParam("@Code", entity.ItemId); 
                sql.AddParam("@Description", entity.ItemDescription);
                sql.AddParam("@Value", entity.ItemFee);
                sql.AddParam("@ModifiedBy", entity.ModifiedBy);
                sql.AddParam("@ModifiedOn", entity.ModifiedOn);
                sql.AddParam("@CreatedBy", entity.CreatedBy);
                sql.AddParam("@CreatedOn", entity.CreatedOn);
               
                sql.Execute();
            }
        }
    }
}