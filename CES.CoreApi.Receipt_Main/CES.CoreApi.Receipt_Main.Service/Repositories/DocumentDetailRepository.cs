using CES.CoreApi.Receipt_Main.Service.Models.DTOs;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Repositories
{
    public class DocumentDetailRepository
    {
        private const string usp_DocumentDetailInsert = "usp_DocumentDetailInsert";
        private const string usp_DocumentDetailUpdate = "usp_DocumentDetailUpdate";
        private const string usp_DocumentDetailDelete = "usp_DocumentDetailDelete";
        private const string usp_DocumentDetailSelect = "usp_DocumentDetailSelect";
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();

        public virtual void Create(DocumentDetail entity)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, usp_DocumentDetailInsert))
            {
                entity.DocumentId = Guid.NewGuid().ToString();
                sql.AddParam("@Id", entity.DocumentDetailId);
                sql.AddParam("@LineNumber", entity.LineNumber);
                sql.AddParam("@DocumentId", entity.DocumentId);
                sql.AddParam("@ItemId", entity.ItemId);
                sql.AddParam("@ItemId", entity.Item.ItemId);
                sql.AddParam("@Amount", entity.Amount);
                sql.AddParam("@CreatedBy", entity.CreatedBy);
                sql.AddParam("@CreatedOn", entity.CreatedOn);
                sql.AddParam("@ModifiedBy", entity.ModifiedBy);
                sql.AddParam("@ModifiedOn", entity.ModifiedOn);              

                sql.Execute();
            }
        }
    }
}