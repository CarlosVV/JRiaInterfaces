using AutoMapper;
using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Utilities;
using CES.Data.Sql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CES.CoreApi.Receipt_Main.Repositories
{
    public class CodesRepository
    {
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();
        private const string GetCoreApiCodesByProvider = "coreapi_sp_GetCoreApiCodesByProvider";
        public const int APPID = 9035;
        const bool getConfigOnly = true;

        public CodesRepository()
        {

        }

        public virtual IEnumerable<CoreApiCode> GetCoreApiCodesFromProviderByType(int providerID, string codeType)
        {   
            var results = new List<CoreApiCode>();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DatabaseName.Main].ConnectionString))
            {
                using (var cmd = new SqlCommand(GetCoreApiCodesByProvider, cn))
                {
                    ICollection<SqlParameter> spParameters = new Collection<SqlParameter>() { new SqlParameter("@fAppID", APPID), new SqlParameter("@fProviderID", providerID), new SqlParameter("@fCodeType", codeType) };
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(spParameters.ToArray<SqlParameter>());
                    var logtext = DatabaseName.GetStoredProcedureExecutionLogText(GetCoreApiCodesByProvider, spParameters);

                    Logging.Log.Info($"Call Stored Procedure: {logtext}. ");
                    cn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            //fAppID fProviderId fCodeType fProviderCode   fCoreApiCode fCoreApiMessage
                            //9025    5004    EC  61  34  OCR not sent                            
                            results.Add(new CoreApiCode
                            {
                                fAppID = reader.GetSafeValue<int>("fAppID"),
                                fCodeType = reader.GetSafeValue<string>("fCodeType"),
                                fCoreApiCode = reader.GetSafeValue<string>("fCoreApiCode"),
                                fCoreApiMessage = reader.GetSafeValue<string>("fCoreApiMessage"),
                                fProviderCode = reader.GetSafeValue<string>("fProviderCode"),
                                fProviderID = reader.GetSafeValue<int>("fProviderID")
                            });
                        }
                    }                      
                }
            }

            return results;
        }

    }
}
