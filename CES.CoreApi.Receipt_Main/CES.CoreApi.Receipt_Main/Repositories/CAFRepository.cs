﻿using CES.CoreApi.Receipt_Main.Models.DTOs;
using CES.CoreApi.Receipt_Main.Utilities;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Repositories
{
    public class CAFRepository
    {
        private const string spCreateCAF = "CAF_Insert";
        private const string spUpdateCAF = "CAF_Update";
        private const string spDeleteCAF = "CAF_Delete";
        private const string spSelectCAF = "CAF_Select";
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();
        public virtual void Create(CAF entity)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, spCreateCAF))
            {
                entity.Id = Guid.NewGuid().ToString();
                sql.AddParam("@Id", entity.Id);
                sql.AddParam("@CompanyTaxId", entity.CompanyTaxId);
                sql.AddParam("@CompanyLegalName", entity.CompanyLegalName);
                sql.AddParam("@DocumentType", entity.DocumentType);
                sql.AddParam("@FolioCurrentNumber", entity.FolioCurrentNumber);
                sql.AddParam("@FolioStartNumber", entity.FolioStartNumber);
                sql.AddParam("@FolioEndNumber", entity.FolioEndNumber);
                sql.AddParam("@DateAuthorization", entity.DateAuthorization);
                sql.AddParam("@FileContent", entity.FileContent);

                sql.Execute();             
            }
        }

        public virtual bool Update(CAF entity)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, spUpdateCAF))
            {
                sql.AddParam("@Id", entity.Id);
                sql.AddParam("@CompanyTaxId", entity.CompanyTaxId);
                sql.AddParam("@CompanyLegalName", entity.CompanyLegalName);
                sql.AddParam("@DocumentType", entity.DocumentType);
                sql.AddParam("@FolioCurrentNumber", entity.FolioCurrentNumber);
                sql.AddParam("@FolioStartNumber", entity.FolioStartNumber);
                sql.AddParam("@FolioEndNumber", entity.FolioEndNumber);
                sql.AddParam("@DateAuthorization", entity.DateAuthorization);
                sql.AddParam("@FileContent", entity.FileContent);

                sql.Execute();

                return true;
            }
        }

        public virtual bool Delete(string id)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Transactional, spDeleteCAF))
            {
                sql.AddParam("@Id", id);              

                sql.Execute();

                return true;
            }
        }

        public virtual IEnumerable<CAF> Get(string id, int? documentType, int? folioCurrentNumber, int? folioStartNumber, int? folioEndNumber)
        {
            var results = new List<CAF>();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DatabaseName.Transactional].ConnectionString))
            {
                using (var cmd = new SqlCommand(spSelectCAF, cn))
                {
                    ICollection<SqlParameter> spParameters = new Collection<SqlParameter>() { new SqlParameter("@Id", id), new SqlParameter("@DocumentType", documentType),
                        new SqlParameter("@FolioCurrentNumber", folioCurrentNumber), new SqlParameter("@folioStartNumber", folioStartNumber), new SqlParameter("@FolioEndNumber", folioEndNumber) };
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(spParameters.ToArray<SqlParameter>());
                    var logtext = DatabaseName.GetStoredProcedureExecutionLogText(spSelectCAF, spParameters);

                    Logging.Log.Info($"Call Stored Procedure: {logtext}. ");
                    cn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            //Id CompanyTaxId    CompanyLegalName DocumentType    FolioCurrentNumber FolioStartNumber    FolioEndNumber DateAuthorization   FileContent
                            //07BEFF77 - 27FF - 4CDA - 910B - 032594D210BE    76134934 - 1  RIA CHILE SERVICIOS FINANCIEROS SPA 33  0   1   20  2 / 27 / 2015 <? xml version = "1.0" ?>                    
                            results.Add(new CAF
                            {
                                Id = reader[0].ToString(),
                                CompanyTaxId = reader.GetSafeValue<string>("CompanyTaxId"),
                                CompanyLegalName = reader.GetSafeValue<string>("CompanyLegalName"),
                                DocumentType = reader.GetSafeValue<int>("DocumentType"),
                                FolioCurrentNumber = reader.GetSafeValue<int>("FolioCurrentNumber"),
                                FolioStartNumber = reader.GetSafeValue<int>("FolioStartNumber"),
                                FolioEndNumber = reader.GetSafeValue<int>("FolioEndNumber"),
                                DateAuthorization = reader.GetSafeValue<string>("DateAuthorization"),
                                FileContent = reader.GetSafeValue<string>("FileContent"),
                            });
                        }
                    }
                }
            }

            return results;
        }

        internal object Delete(object id)
        {
            throw new NotImplementedException();
        }
    }
}