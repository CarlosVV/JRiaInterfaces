using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Models.DTO;
using CES.CoreApi.Compliance.Screening.Utilities;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class WatchlistRuleRepository
    {
        private SqlMapper sqlMapper = Database.CreateSqlMapper();

        /// <summary>
        /// Default constructor
        /// </summary>
        public WatchlistRuleRepository()
        {
        }

        public virtual IEnumerable<Rule> GetAllScreeningRules(string countryFrom, string countryTo)
        {
            // TODO: replace this mocked code with sp calls
            return new List<Rule> {
                new Rule() {
                    ProviderID = 201,
                    ProviderName = "Actimize",
                    fRuleID = 142755,
                    fActionID = 2,
                    CountryTo = countryTo,
                    ContryFrom = countryFrom,
                    fNameTypeID = PartyType.Customer,
                    fMatchTypeID = 10,
                    fIssueItemID=1605,                    
                    SearchDef = "Test",
                    BusinessUnit = "All"                   

                },
                 new Rule() {
                    ProviderID = 201,
                    ProviderName = "Actimize",
                    fRuleID = 140655,
                    fActionID = 2,
                    CountryTo = countryTo,
                    ContryFrom = countryFrom,
                    fNameTypeID = PartyType.OnBehalfOf,
                    fMatchTypeID = 10,
                    fIssueItemID=1655,
                    SearchDef = "Test",
                    BusinessUnit = "All"

                },
                   new Rule() {
                    ProviderID = 201,
                    ProviderName = "Actimize",
                    fRuleID = 140555,
                    fActionID = 10,
                    CountryTo = countryTo,
                    ContryFrom = countryFrom,
                    fNameTypeID =PartyType.Beneficiary,
                    fMatchTypeID = 10,
                    fIssueItemID=1705,
                    SearchDef = "Test",
                    BusinessUnit = "All"

                },
            };
        }

        /// <summary>
        /// THIS IS MOCKED, replaces params with VALID data
        /// </summary>
        /// <param name="transAmount"></param>
        /// <param name="countryFrom"></param>
        /// <param name="countryTo"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public virtual IEnumerable<Rule> GetScreeningRulesForTransaction(Request request, PartyType partyType, int providerID)
        {
            //TODO: Cache
         
            using (var sql = sqlMapper.CreateQuery(Database.Main, "compl_sp_Watchlist_Match_Rules_Get"))
            {
                #region SPCallLog
                var parameters = new Collection<SqlParameter>();
                parameters.Add(new SqlParameter("@lAppID", AppSettings.AppId));
                parameters.Add(new SqlParameter("@lAppObjectID", AppSettings.AppObjectId));
                parameters.Add(new SqlParameter("@lUserNameID", 1));
                parameters.Add(new SqlParameter("@TransDate", request.TransDateTime));
                parameters.Add(new SqlParameter("@lRunTimeID", request.RuntimeID));
                parameters.Add(new SqlParameter("@lServiceID", 111)); //TODO: request
                parameters.Add(new SqlParameter("@lProductID", request.ProductId));
                parameters.Add(new SqlParameter("@lCountryFromID", request.CountryFromId));
                parameters.Add(new SqlParameter("@lCountryToID", request.CountryToId));
                parameters.Add(new SqlParameter("@lRecAgentID", request.ReceivingAgent.ID));
                parameters.Add(new SqlParameter("@lRecAgentLocID", request.ReceivingAgent.LocID));
                parameters.Add(new SqlParameter("@lPayAgentID", request.PayAgent.ID));
                parameters.Add(new SqlParameter("@lPayAgentLocID", request.PayAgent.LocID));
                parameters.Add(new SqlParameter("@lDeliveryMethodID", 1)); //TODO: request
                parameters.Add(new SqlParameter("@lEntryTypeID", 10)); //TODO: request
                parameters.Add(new SqlParameter("@sCurrency", request.SendCurrency));
                parameters.Add(new SqlParameter("@mOrderAmount", request.SendAmount));
                parameters.Add(new SqlParameter("@mTotalAmount", request.SendTotalAmount));
                var callLog = Database.LogSPCall("compl_sp_Watchlist_Match_Rules_Get", parameters);
                Logging.Log.Info(callLog);
                #endregion

                sql.AddParam("@lAppID", AppSettings.AppId);
                sql.AddParam("@lAppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@TransDate", request.TransDateTime);
                sql.AddParam("@lRunTimeID", request.RuntimeID);
                sql.AddParam("@lServiceID", 111); //TODO: request
                sql.AddParam("@lProductID", request.ProductId);
                sql.AddParam("@lCountryFromID", request.CountryFromId);
                sql.AddParam("@lCountryToID", request.CountryToId);
                sql.AddParam("@lRecAgentID", request.ReceivingAgent.ID);
                sql.AddParam("@lRecAgentLocID", request.ReceivingAgent.LocID);
                sql.AddParam("@lPayAgentID", request.PayAgent.ID);
                sql.AddParam("@lPayAgentLocID", request.PayAgent.LocID);
                sql.AddParam("@lDeliveryMethodID", 1); //TODO: request
                sql.AddParam("@lEntryTypeID", 10); //TODO: request
                sql.AddParam("@sCurrency", request.SendCurrency);
                sql.AddParam("@mOrderAmount", request.SendAmount);
                sql.AddParam("@mTotalAmount", request.SendTotalAmount);

              
                var rules=sql.Query<Rule>();

                if (providerID ==0) //All
                {
                    return rules.Where(r => r.fNameTypeID == partyType);
                }                 


                return rules.Where(r=>r.ProviderID == providerID && r.fNameTypeID == partyType);
              
   
            }
        }

        public virtual long CreateReviewAlert(ReviewAlertCreateRequest request)
        {
            
            using (var sql = sqlMapper.CreateCommand(Database.Main, "compl_sp_ReviewIssue_Create_External "))
            {
                #region SPCallLog
                var parameters = new Collection<SqlParameter>();
                parameters.Add(new SqlParameter("@lAppID", AppSettings.AppId));
                parameters.Add(new SqlParameter("@lAppObjectID", AppSettings.AppObjectId));
                parameters.Add(new SqlParameter("@lUserNameID", 1));
                parameters.Add(new SqlParameter("@UserLocale", ""));
                parameters.Add(new SqlParameter("@DateTime", request.DateTime));
                parameters.Add(new SqlParameter("@lServiceID", request.ServiceID));
                parameters.Add(new SqlParameter("@lTransactionID", request.TransactionID));
                parameters.Add(new SqlParameter("@lIssueID", request.IssueID));
                parameters.Add(new SqlParameter("@IssueDescription", request.IssueDescription));
                parameters.Add(new SqlParameter("@lFilterID", request.FilterID));
                parameters.Add(new SqlParameter("@lIssueTypeID", request.IssueTypeID));
                parameters.Add(new SqlParameter("@lActionID", request.ActionID));
                parameters.Add(new SqlParameter("@lProviderID", request.ProviderID));
                parameters.Add(new SqlParameter("@ProviderAlertID", request.ProviderAlertID));
                parameters.Add(new SqlParameter("@ProviderAlertStatusID", request.ProviderAlertStatusID));
                var callLog = Database.LogSPCall("compl_sp_Watchlist_Match_Rules_Get", parameters);
                Logging.Log.Info(callLog);
                #endregion

                sql.AddParam("@lAppID", AppSettings.AppId);
                sql.AddParam("@lAppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@UserLocale", "");
                sql.AddParam("@DateTime", request.DateTime);
                sql.AddParam("@lServiceID", request.ServiceID);
                sql.AddParam("@lTransactionID", request.TransactionID);
                sql.AddParam("@lIssueID", request.IssueID);
                sql.AddParam("@IssueDescription", request.IssueDescription);
                sql.AddParam("@lFilterID", request.FilterID);
                sql.AddParam("@lIssueTypeID", request.IssueTypeID);
                sql.AddParam("@lActionID", request.ActionID);
                sql.AddParam("@lProviderID", request.ProviderID);
                sql.AddParam("@ProviderAlertID", request.ProviderAlertID);
                sql.AddParam("@ProviderAlertStatusID", request.ProviderAlertStatusID);

                var reviewID = sql.AddOutputParam("@lReviewID", System.Data.SqlDbType.BigInt);
             
                sql.Execute();

                var reviewIssueID = reviewID.GetSafeValue<long>();
                return reviewIssueID;
            }

        }
    }
}