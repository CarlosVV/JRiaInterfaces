//using CES.CoreApi.Receipt_Main.Service.Model.Documents;
using CES.CoreApi.Receipt_Main.Service.Models.DTOs;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Repositories
{
    public class ItemRepository
    {
        private const string usp_ItemInsert = "coreapi_sp_systblApp_CoreAPI_Item_Create";
        private const string usp_ItemUpdate = "coreapi_sp_systblApp_CoreAPI_Item_Update";
        private const string usp_ItemDelete = "coreapi_sp_systblApp_CoreAPI_Item_Delete";
        private const string usp_ItemSelect = "coreapi_sp_systblApp_CoreAPI_Item_Get";
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