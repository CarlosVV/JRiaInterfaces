using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Repositories;
using CES.CoreApi.Receipt_Main.Domain.Core.Services;
using CES.CoreApi.Receipt_Main.Infrastructure.Data;
using CES.CoreApi.Receipt_Main.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Application.Core
{
    public class DocumentService : IDocumentService
    {
        private IDocumentRepository repo;
        public DocumentService(IDocumentRepository repository)
        {
            repo = repository;
        }
        public List<actblTaxDocument> GetAllDocuments()
        {
            return repo.find(c => !c.fDisabled && !c.fDelete).ToList();
        }

        public List<string> GetAllDocumentsFoliosByType(string doctype, DateTime? startDate, DateTime? endDate)
        {
            return repo.find(m => m.fDocumentTypeID.Equals(doctype) && (!startDate.HasValue || startDate.Value <= m.fIssuedDate)
            && (!endDate.HasValue || endDate.Value > m.fIssuedDate)).Select(p => p.fAuthorizationNumber).ToList();
        }

        public void CreateDocument(actblTaxDocument objectEntry)
        {
            this.repo.CreateDocument(objectEntry);
        }
        public void UpdateDocument(actblTaxDocument objectEntry)
        {
            this.repo.UpdateDocument(objectEntry);
        }
        public void RemoveDocument(actblTaxDocument objectEntry)
        {
            this.repo.RemoveDocument(objectEntry);
        }

        public void SaveChanges()
        {
            this.repo.SaveChanges();
        }

        public DocumentResultSet GetOrderInfo(int? orderId, string orderNo)
        {
            var result = new DocumentResultSet();

            var parameters = new SqlParameter[] {
                new SqlParameter("@lAppID", System.Data.SqlDbType.Int),
                new SqlParameter("@lAppObjectID", System.Data.SqlDbType.Int),
                new SqlParameter("@lAgentID", System.Data.SqlDbType.Int),
                new SqlParameter("@lAgentLocID", System.Data.SqlDbType.Int),
                new SqlParameter("@lUserNameID", System.Data.SqlDbType.Int),
                new SqlParameter("@sLocale", System.Data.SqlDbType.VarChar),
                new SqlParameter("@TellerDrawerInstanceID", System.Data.SqlDbType.Int),
                new SqlParameter("@AgentCountry", System.Data.SqlDbType.Char),
                new SqlParameter("@lAgentCountryID", System.Data.SqlDbType.Int),
                new SqlParameter("@AgentState", System.Data.SqlDbType.VarChar),
                new SqlParameter("@lOrderID", System.Data.SqlDbType.Int),
                new SqlParameter("@OrderNo", System.Data.SqlDbType.VarChar) };

            parameters[0].Value = 1;
            parameters[1].Value = 0;
            parameters[2].Value = 0;
            parameters[3].Value = 0;
            parameters[4].Value = 1;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = 0;
            parameters[7].Value = "CL";
            parameters[8].Value = 1;
            parameters[9].Value = "RM";

            if (orderId.HasValue)
            {
                parameters[10].Value = orderId.Value;
            }
            else
            {
                parameters[10].Value = DBNull.Value;
            }

            if (!string.IsNullOrWhiteSpace( orderNo) )
            {
                parameters[11].Value = orderNo;
            }
            else
            {
                parameters[11].Value = DBNull.Value;
            }
           

            using (var db = new MainDbContext())
            {
                db.Database.Initialize(force: false);
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandText = "[dbo].[coreapi_sp_taxreceipt_OrderInfo_Detail_TaxReceipt]";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);

                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();

                    var info = ((IObjectContextAdapter)db)
                        .ObjectContext
                        .Translate<MessageInfoResult>(reader);

                    result.MessageInfoResult = info.FirstOrDefault();

                    reader.NextResult();
                    var orders = ((IObjectContextAdapter)db)
                        .ObjectContext
                        .Translate<OrderInfoResult>(reader);


                    result.OrderInfoResult = orders.FirstOrDefault();

                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }

            return result;
        }
    }
}
