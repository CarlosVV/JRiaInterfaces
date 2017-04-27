using CES.CoreApi.Receipt_Main.Models;
//using CES.CoreApi.Receipt_Main.Model.Documents;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CES.CoreApi.Receipt_Main.Models.DTOs;

namespace CES.CoreApi.Receipt_Main.Repositories
{
    public class DocumentRepository
    {
        private const string usp_DocumentInsert = "coreapi_sp_systblApp_CoreAPI_Document_Create";
        private const string usp_DocumentUpdate = "coreapi_sp_systblApp_CoreAPI_Document_Update";
        private const string usp_DocumentDelete = "coreapi_sp_systblApp_CoreAPI_Document_Delete";
        private const string usp_DocumentSelect = "coreapi_sp_systblApp_CoreAPI_Document_Get";
        private const string usp_DocumentSearch = "coreapi_sp_systblApp_CoreAPI_Document_Search";
        private const string usp_DocumentSelectByOrderNumberAndFolio = "coreapi_sp_systblApp_CoreAPI_Document_SelectByOrderNumberAndFolio";

        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();

        public virtual void Create(Document entity)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, usp_DocumentInsert))
            {
                entity.DocumentId = Guid.NewGuid().ToString();

                sql.AddParam("@Id", entity.DocumentId);
                sql.AddParam("@Number", entity.DocumentNumber);
                sql.AddParam("@Type", entity.DocumentTypeId);
                sql.AddParam("@Folio", entity.Folio);
                sql.AddParam("@Branch", entity.Branch);
                sql.AddParam("@TellerNumber", entity.TellerNumber);
                sql.AddParam("@TellerName", entity.TellerName);
                sql.AddParam("@Issued", entity.Issued);
                sql.AddParam("@TotalAmount", entity.TotalAmount);
                sql.AddParam("@SenderId", entity.Sender.Id);
                sql.AddParam("@ReceiverId", entity.Receiver.Id);
                sql.AddParam("@ModifiedBy", entity.ModifiedBy);
                sql.AddParam("@CreatedBy", entity.CreatedBy);
                sql.AddParam("@CreatedOn", entity.CreatedOn);
                sql.AddParam("@ModifiedOn", entity.ModifiedOn);

                sql.Execute();
            }
        }

        public virtual bool Update(Document entity)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, usp_DocumentUpdate))
            {
                sql.AddParam("@Id", entity.DocumentId);
                sql.AddParam("@Number", entity.DocumentNumber);
                sql.AddParam("@Type", entity.DocumentTypeId);
                sql.AddParam("@Folio", entity.Folio);
                sql.AddParam("@Branch", entity.Branch);
                sql.AddParam("@TellerNumber", entity.TellerNumber);
                sql.AddParam("@TellerName", entity.TellerName);
                sql.AddParam("@Issued", entity.Issued);
                sql.AddParam("@TotalAmount", entity.TotalAmount);
                sql.AddParam("@SenderId", entity.Sender.Id);
                sql.AddParam("@ReceiverId", entity.Receiver.Id);
                sql.AddParam("@ModifiedBy", entity.ModifiedBy);
                sql.AddParam("@CreatedBy", entity.CreatedBy);
                sql.AddParam("@CreatedOn", entity.CreatedOn);
                sql.AddParam("@ModifiedOn", entity.ModifiedOn);

                sql.Execute();
            }

            return true;
        }

        public virtual bool Delete(string id)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, usp_DocumentDelete))
            {
                sql.AddParam("@Id", id);
                sql.Execute();
            }

            return true;
        }

        public virtual IEnumerable<Document> Get(string id)
        {
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Transactional, usp_DocumentSelect))
            {
                sql.AddParam("@Id", id);
                return sql.Query<Document>();
            }
        }

        public virtual IEnumerable<DocumentSearchResultItem> Search(string documentid, string documenttypecode, string itemcode, string receiverfirstname, string receivermiddlename
                , string receiverlastname1, string receiverlastname2, string senderfirstname, string sendermiddlename, string senderlastname1, string senderlastname2, int? documentfolio
                , string documentbranch, string documenttellernumber, string documenttellername, DateTime? documentissued, decimal? documenttotalamount)
        {
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Transactional, usp_DocumentSearch))
            {
                sql.AddParam("@DocumentId", documentid);               
                sql.AddParam("@DocumentTypeCode", documenttypecode);
                sql.AddParam("@ItemCode", itemcode);
                sql.AddParam("@ReceiverFirstName", receiverfirstname);
                sql.AddParam("@ReceiverMiddleName", receivermiddlename);
                sql.AddParam("@ReceiverLastName1", receiverlastname1);
                sql.AddParam("@ReceiverLastName2", receiverlastname2);
                sql.AddParam("@SenderFirstName", senderfirstname);
                sql.AddParam("@SenderMiddleName", sendermiddlename);
                sql.AddParam("@SenderLastName1", senderlastname1);
                sql.AddParam("@SenderLastName2", senderlastname2);
                sql.AddParam("@DocumentFolio", documentfolio);
                sql.AddParam("@DocumentBranch", documentbranch);
                sql.AddParam("@DocumentTellerNumber", documenttellernumber);
                sql.AddParam("@DocumentTellerName", documenttellername);
                sql.AddParam("@DocumentIssued", documentissued);
                sql.AddParam("@DocumentTotalAmount", documenttotalamount);

                return sql.Query<DocumentSearchResultItem>();
            }
        }

        public virtual IEnumerable<Document> GetByOrderNumberAndFolio(string ordernumber, int folio)
        {
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Transactional, usp_DocumentSelectByOrderNumberAndFolio))
            {
                sql.AddParam("@OrderNo", ordernumber);
                sql.AddParam("@Folio", folio);
                return sql.Query<Document>();
            }
        }
    }
}