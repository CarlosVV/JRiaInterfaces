using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Models.DTOs;
using CES.CoreApi.BankAccount.Api.Utilities;
using CES.Data.Sql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;


namespace CES.CoreApi.BankAccount.Api.Repositories
{
    internal class BankAccountRespository
    {
        #region core
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();

        private const string getProcessorInfo = "mt_sp_BankDeposit_AcctValidate_GetProcessorInfo";
        private const string getCoreAPISetting = "coreapi_sp_GetCoreAPISetting";
        private const string getValidatedBankAcctInfo = "coreapi_sp_GetValidatedBankAcctInfo";
        private const string getProcessors = "mt_sp_BankDeposit_AcctValidate_GetProcessors";
        private const string getProcessorSetting = "mt_sp_BankDeposit_AcctValidate_GetProcessorSettings";
        private const string getBankCode = "coreapi_sp_GetBankCode";
        private const string saveValidate = "mt_sp_BankDeposit_Save_Validate";
        private const string saveValidatedBankAcctInfo = "coreapi_sp_SaveValidatedBankAcctInfo";
        private const string getPaWsSetting = "coreapi_sp_GetPaWsSetting";
        private const string getPaExportSetting = "coreapi_sp_GetPaExportSetting";
        private const string getContentPGPKeyDef = "sys_sp_PGPKeyDef_GetContent";

        #endregion
        #region public methods

        public virtual IEnumerable<CurrencyCountry> GetServiceOfferdCurrencies(CountryCurrencyRequest request)
        {

            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, "sales_sp_Services_Offered_Currencies_List"))
            {
                sql.AddParam("@fServiceID", 111);
                sql.AddParam("@fDate", DateTime.UtcNow);
                sql.AddParam("@fCountryFrom", request.CountryFrom);
                sql.AddParam("@fCountryTo", request.CountryTo);

                var many = sql.Query<CurrencyCountry>();
                return many;
            }
        }
        public virtual Hashtable GetProcessorInfo(int nProcessorID)
        {
          
            Hashtable ht = null;
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Main, getProcessorInfo))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lProcessorID", nProcessorID);
                var processorName = sql.AddOutputParam("@sProcessorName", System.Data.SqlDbType.VarChar, 100);
                var processorDisabledData = sql.AddOutputParam("@bProcessorDisabled", System.Data.SqlDbType.Bit);
                var codeHandlerIDData = sql.AddOutputParam("@lCodeHandlerID", System.Data.SqlDbType.Int);
                var codeHandlerTypeIDData = sql.AddOutputParam("@lCodeHandlerTypeID", System.Data.SqlDbType.Int);
                var codeHandlerPlatformID = sql.AddOutputParam("@lCodeHandlerPlatformID", System.Data.SqlDbType.Int);
                var codeHandlerName = sql.AddOutputParam("@sCodeHandlerName", System.Data.SqlDbType.VarChar, 100);
                var codeHandlerPath = sql.AddOutputParam("@sCodeHandlerPath", System.Data.SqlDbType.VarChar, 1024);
                var codeHandlerDisabled = sql.AddOutputParam("@sCodeHandlerDisabled", System.Data.SqlDbType.Bit);
                sql.Execute();

                object auto = processorName.GetSafeValue<string>();
                if (auto == null)
                {
                    return ht;
                }

                ht = new Hashtable();
                ht["Name"] = auto;
                bool bAuto = processorDisabledData.GetSafeValue<bool>();
                ht["Disabled"] = (bAuto ? "1" : "0");
                ht["ClassName"] = codeHandlerPath.GetSafeValue<string>();
                return ht;
            }

        }
        public virtual void GetProcessorSetting(int nProcessorID, Hashtable ht)
        {

            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, getProcessorSetting))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lProcessorID", nProcessorID);

                var settings = sql.Query<DictionarySettings>();

                foreach (var value in settings )
                {
                    ht[value.fName] = value.fValue;
                }

                if (!ht.Contains("UseCoreApiWsSetting")) ht["UseCoreApiWsSetting"] = "1";
            }
         
        }
        public virtual Dictionary<string, string> GetCoreAPISettings_App()
        {
            Dictionary<string, string> dictionarySettings = null;
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, getCoreAPISetting))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppId);
                sql.AddParam("@lUserNameID", 1);

                var settings = sql.Query<DictionarySettings>();
                dictionarySettings = settings.ToDictionary(x => x.fName, x => x.fValue);
                return dictionarySettings;
            }
        }
        #endregion
        public ValidateBankAccountInfo RunBankDepositValidate(ValidateBankDepositRequest rqm)
        {
            var dbResult = null as ValidateBankAccountResult;
            var dbMapFields = null as IEnumerable<ValidateBankAccountMapFields>;

            using (var sql = _sqlMapper.CreateQueryAgain(DatabaseName.Main, saveValidate))
            {                
                sql.AddParam("@lAppID", rqm.AgentID);
                sql.AddParam("@lAppObjectID", rqm.AppObjectID);
                sql.AddParam("@lUserNameID", rqm.UserNameID);
                sql.AddParam("@UserLocale", rqm.UserLocale);
                sql.AddParam("@bCheckIfValidOnly", rqm.CheckIfValidOnly);

                sql.AddParam("@LocalDate", DateTime.Parse(rqm.LocalDate));
                sql.AddParam("@lAgentID", rqm.AgentID);
                sql.AddParam("@lAgentLocID", rqm.AgentLocID);
                sql.AddParam("@BankID", rqm.BankID);
                sql.AddParam("@BankCountryID", rqm.BankCountryID);

                sql.AddParam("@DepositCurrency", rqm.DepositCurrency);
                sql.AddParam("@ProviderID", rqm.ProviderID);
                sql.AddParam("@BankAccountTypeID", rqm.BankAccountTypeID);
                sql.AddParam("@BankAccountNo", rqm.BankAccountNo);
                sql.AddParam("@UnitaryAccountNo", rqm.UnitaryAccountNo);

                sql.AddParam("@BankRoutingCode", rqm.BankRoutingCode);
                sql.AddParam("@BIC", rqm.BIC);
                sql.AddParam("@BankBranchID", rqm.BankBranchID);
                sql.AddParam("@BankBranchName", rqm.BankBranchName);
                sql.AddParam("@BankBranchCity", rqm.BankBranchCity);

                sql.AddParam("@BankBranchNumber", rqm.BankBranchNumber);

                sql.QueryAgain(reader =>
                {
                    dbResult = reader.QueryOne<ValidateBankAccountResult>();
                    dbMapFields = reader.Query<ValidateBankAccountMapFields>();
                  
                });

                return new ValidateBankAccountInfo()
                {
                    DbResult = dbResult,
                    DbMapFields_List = dbMapFields.ToList()
                };
               

            }
        }       
        public virtual IEnumerable<Processor> GetProcessor(int nProviderMapID, int nBankID, int nBankCounteyID)
        {
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, getProcessors))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lProviderMapID", nProviderMapID);
                sql.AddParam("@lBankID", nBankID);
                sql.AddParam("@lBankCountryID", nBankCounteyID);

                var processors = sql.Query<Processor>();
                return processors;
            }
        }
        public BankAccountInfo GetBankAcctInfo(int nProcessorID, int nBankID, string sBankName, string sBankCode, string sAccountNumber, int nDaysExpired)
        {
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, getValidatedBankAcctInfo))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lProcessorID", nProcessorID);
                sql.AddParam("@lBankID", nBankID);
                sql.AddParam("@sBankCode", sBankCode);
                sql.AddParam("@sBankAcctNo", sAccountNumber);
                sql.AddParam("@lExpiredNumDays", nDaysExpired);

                var accountInfo = sql.QueryOne<BankAccountInfo>();

                if (accountInfo == null) return null;
                accountInfo.BankID = nBankID;
                accountInfo.Provider = "Cache";
                accountInfo.BankCode = sBankCode;
                accountInfo.BankName = sBankName;
                accountInfo.AccountNo = sAccountNumber;
                return accountInfo;
            }
        }
        public virtual string GetBankcode(int nBankID)
        {
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, getBankCode))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lBankID", nBankID);

                var accountInfo = sql.QueryOne<BankAccountInfo>();
           
                return accountInfo.BankCode;
            }
          
        }
        public byte[] LoadKeyFromDB(string sKeyID)
        {
            var connection = DatabaseName.GetConnectionString(DatabaseName.Main);

            
            byte[] bKey;
            MemoryStream ms = new MemoryStream();

            const int bufferLength = 102400; //100Kb
            byte[] bytesBuffer = new byte[bufferLength];

            using (SqlConnection cnn = new SqlConnection(connection.ConnectionString))
            {
                using (SqlCommand cmd = new System.Data.SqlClient.SqlCommand(getContentPGPKeyDef, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sKeyID", SqlDbType.VarChar, 20).Value = sKeyID;

                    cnn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        long totalBytesRead = 0;
                        int bytesRead = 0;

                        if (dr.Read())
                        {
                            do
                            {
                                bytesRead = (int)dr.GetBytes(0, totalBytesRead, bytesBuffer, 0, bufferLength);
                                totalBytesRead += bytesRead;

                                if (bytesRead != 0) ms.Write(bytesBuffer, 0, bytesRead);

                            } while (bytesRead != 0);

                            ms.Flush();
                            ms.Seek(0, SeekOrigin.Begin);
                        }
                        dr.Close();
                    }
                    cnn.Close();
                }
            }

            using (ms)
            using (BinaryReader br = new BinaryReader(ms))
            {
                bKey = br.ReadBytes((int)ms.Length);
                br.Close();
            }

            return bKey;
        }
        public virtual void SaveBankAcctInfo(BankAccountInfo baInfo)
        {
            using (var sql = _sqlMapper.CreateCommand(DatabaseName.Main, saveValidatedBankAcctInfo))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lProcessorID", baInfo.fProcessorID);
                sql.AddParam("@lBankID", baInfo.BankID);
                sql.AddParam("@sBankAcctNo", baInfo.AccountNo);
                sql.AddParam("@sBankCode", baInfo.BankCode);
                sql.AddParam("@sBankName", string.Empty);
                sql.AddParam("@sBankAcctHolderName", baInfo.fBankAcctHolderName);
                sql.AddParam("@sBankAccountNo_Unitary", string.Empty);
                sql.AddParam("@dRequested", baInfo.fRequested);
                sql.AddParam("@sStatusCode", baInfo.fStatusCode);
                sql.AddParam("@sStatusMsg", baInfo.fStatusMsg);

                sql.Execute();
            }         
        }
        public virtual CPaConn GetPaWsSetting(int payAgentID)
        {
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, getPaWsSetting))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lNameID", payAgentID);

                var paConn = sql.QueryOne<CPaConn>();
                paConn.PayAgentID = payAgentID;

                return paConn;
            }        
        }
        public virtual Hashtable GetPaExportSetting(int nExportFormatID)
        {
            Hashtable ht = new Hashtable();
            using (var sql = _sqlMapper.CreateQuery(DatabaseName.Main, getPaExportSetting))
            {
                sql.AddParam("@AppID", AppSettings.AppId);
                sql.AddParam("@AppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@lFormatID", nExportFormatID);

                var setting = sql.QueryOne<PaExportSetting>();
                if (!string.IsNullOrWhiteSpace(setting.fSettingName))
                    ht[setting.fSettingName] = setting.fSettingvalue;
                return ht;
            }          
        }
        #region private methods
      
       
        #endregion
    }
}