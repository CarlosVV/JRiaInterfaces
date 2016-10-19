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
        const string SpGetRules = "compl_sp_Watchlist_Match_Rules_Get";
        const string SpCreateReviewAlert = "compl_sp_ReviewIssue_Create_External";
        const string SPGetConsts = "ol_sp_GetSystblConst2Values";
        const string SPCommServerMessage = "sys_sp_CommServer_Message_ToSend_Setup";


        /// <summary>
        /// Default constructor
        /// </summary>
        public WatchlistRuleRepository()
        {
        }

        public virtual IEnumerable<Rule> GetAllScreeningRules(string countryFrom, string countryTo)
        {
            
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
                    SearchDef = "All Sanctions1",
                    BusinessUnit = "TRG-Americas-US-MTCoreRTSanctions-Internal"

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
                    SearchDef = "All Sanctions1",
                    BusinessUnit = "TRG-Americas-US-MTCoreRTSanctions-Internal"

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
                    SearchDef = "All Sanctions1",
                    BusinessUnit = "TRG-Americas-US-MTCoreRTSanctions-Internal"

                },
            };
        }


        public virtual IEnumerable<Rule> GetScreeningRulesForTransaction(RulesRequest request, int providerID=0)
        {
            return GetScreeningRules(request.TransDateTime, request.RuntimeID, request.ServiceId, request.ProductId, request.CountryFromId, request.CountryToId, request.ReceivingAgentID, request.ReceivingAgentLocID, request.PayAgentID, request.PayAgentLocID, request.DeliveryMethod, request.EntryTypeId, request.SendCurrency, request.SendAmount, request.SendTotalAmount, request.PartyType, providerID);
        }

        public virtual IEnumerable<Rule> GetScreeningRulesForTransaction(Request request, PartyType partyType, int providerID)
        {
            return GetScreeningRules(request.TransDateTime, request.RuntimeID, request.ServiceId, request.ProductId, request.CountryFromId, request.CountryToId, request.ReceivingAgent.ID, request.ReceivingAgent.LocID, request.PayAgent.ID, request.PayAgent.LocID, request.DeliveryMethod, request.EntryTypeId, request.SendCurrency, request.SendAmount, request.SendTotalAmount, partyType, providerID);
        }
        private  IEnumerable<Rule> GetScreeningRules(DateTime transDateTime, int runtimeID, ServiceIdType serviceId, int productId, int countryFromId, int countryToId, int receivingAgentID, int receivingAgentLocID, int payAgentID, int payAgentLocID, DeliveryMethod  deliveryMethod, int entryTypId, string sendCurrency, double sendAmount, double sendTotalAmount, PartyType partyType, int providerID =0)
        {
            #region SPCallLog
            var parameters = new Collection<SqlParameter>();
            parameters.Add(new SqlParameter("@lAppID", AppSettings.AppId));
            parameters.Add(new SqlParameter("@lAppObjectID", AppSettings.AppObjectId));
            parameters.Add(new SqlParameter("@lUserNameID", 1));
            parameters.Add(new SqlParameter("@TransDate", transDateTime));
            parameters.Add(new SqlParameter("@lRunTimeID", runtimeID));
            parameters.Add(new SqlParameter("@lServiceID", (int)serviceId));
            parameters.Add(new SqlParameter("@lProductID", productId));
            parameters.Add(new SqlParameter("@lCountryFromID", countryFromId));
            parameters.Add(new SqlParameter("@lCountryToID", countryToId));
            parameters.Add(new SqlParameter("@lRecAgentID", receivingAgentID));
            parameters.Add(new SqlParameter("@lRecAgentLocID", receivingAgentLocID));
            parameters.Add(new SqlParameter("@lPayAgentID", payAgentID));
            parameters.Add(new SqlParameter("@lPayAgentLocID", payAgentLocID));
            parameters.Add(new SqlParameter("@lDeliveryMethodID", (int)deliveryMethod));
            parameters.Add(new SqlParameter("@lEntryTypeID", entryTypId));
            parameters.Add(new SqlParameter("@sCurrency", sendCurrency));
            parameters.Add(new SqlParameter("@mOrderAmount", sendAmount));
            parameters.Add(new SqlParameter("@mTotalAmount", sendTotalAmount));
            var callLog = Database.LogSPCall(SpGetRules, parameters);
            Logging.Log.Info(callLog);
            #endregion

            using (var sql = sqlMapper.CreateQuery(Database.Main, SpGetRules))
            {
               

                sql.AddParam("@lAppID", AppSettings.AppId);
                sql.AddParam("@lAppObjectID", AppSettings.AppObjectId);
                sql.AddParam("@lUserNameID", 1);
                sql.AddParam("@TransDate", transDateTime);
                sql.AddParam("@lRunTimeID", runtimeID);
                sql.AddParam("@lServiceID", serviceId); 
                sql.AddParam("@lProductID", productId);
                sql.AddParam("@lCountryFromID", countryFromId);
                sql.AddParam("@lCountryToID", countryToId);
                sql.AddParam("@lRecAgentID", receivingAgentID);
                sql.AddParam("@lRecAgentLocID", receivingAgentLocID);
                sql.AddParam("@lPayAgentID", payAgentID);
                sql.AddParam("@lPayAgentLocID", payAgentLocID);
                sql.AddParam("@lDeliveryMethodID", deliveryMethod ); 
                sql.AddParam("@lEntryTypeID", entryTypId); 
                sql.AddParam("@sCurrency", sendCurrency);
                sql.AddParam("@mOrderAmount", sendAmount);
                sql.AddParam("@mTotalAmount", sendTotalAmount);

              
                var rules=sql.Query<Rule>();

                if (providerID ==0) //All
                {
                    return rules.Where(r => r.fNameTypeID == partyType);
                }                 


                return rules.Where(r=>r.ProviderID == providerID && r.fNameTypeID == partyType);
              
   
            }
        }

        public virtual ReviewAlertCreateResponse CreateReviewAlert(ReviewAlertCreateRequest request)
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

            var lReviewID = new SqlParameter("@lReviewID", System.Data.SqlDbType.Int);
            lReviewID.Direction =System.Data.ParameterDirection.Output;
            parameters.Add(lReviewID);

            var callLog = Database.LogSPCall(SpCreateReviewAlert, parameters);
            Logging.Log.Info(callLog);

            #endregion

           
            using (var sql = sqlMapper.CreateQuery(Database.Main, SpCreateReviewAlert))
            {

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

                var result = sql.QueryOne<ReviewAlertCreateResponse>();

                result.reviewIssueID = reviewID.GetSafeValue<long>();

                return result;
            }
           
            
        }

        public virtual IEnumerable<Constant> GetEntryTypes()
        {
            using (var sql = sqlMapper.CreateQuery(Database.Main, SPGetConsts))
            {
                #region SPCallLog
                var parameters = new Collection<SqlParameter>();
                parameters.Add(new SqlParameter("@key1", 7352));               
                var callLog = Database.LogSPCall(SPGetConsts, parameters);
                Logging.Log.Info(callLog);
                #endregion

                sql.AddParam("@key1",7352);

                return sql.Query<Constant>();

            }
        }

        #region Email
        public virtual SendEmailResponse SendMail(SendEmailRequest request)
        {

            #region SPCallLog
            var parameters = new Collection<SqlParameter>();
            parameters.Add(new SqlParameter("@fMessage", request.Message));
            parameters.Add(new SqlParameter("@fMessageType", request.MessageType));
            parameters.Add(new SqlParameter("@fMessageFormat", request.MessageFormat));
            parameters.Add(new SqlParameter("@fMessageFrom", request.MessageFrom));
            parameters.Add(new SqlParameter("@fMessageTo", request.MessageTo));
            parameters.Add(new SqlParameter("@fMessageCc", request.MessageCc));
            parameters.Add(new SqlParameter("@fMessageBcc", request.MessageBcc));
            parameters.Add(new SqlParameter("@fMessageSubject", request.MessageSubject));
            parameters.Add(new SqlParameter("@fMessageSendMethod", request.MessageSendMethod));
            parameters.Add(new SqlParameter("@fUserNameID", request.UserNameID));
            var callLog = Database.LogSPCall(SPCommServerMessage, parameters);
            Logging.Log.Info(callLog);
            #endregion

            using (var sql = sqlMapper.CreateCommand(Database.Main, SPCommServerMessage))
            {             

               
                sql.AddParam("@fMessage", request.Message);
                sql.AddParam("@fMessageType", request.MessageType);
                sql.AddParam("@fMessageFormat", request.MessageFormat);
                sql.AddParam("@fMessageFrom", request.MessageFrom);
                sql.AddParam("@fMessageTo", request.MessageTo);
                sql.AddParam("@fMessageCc", request.MessageCc);
                sql.AddParam("@fMessageBcc", request.MessageBcc);
                sql.AddParam("@fMessageSubject", request.MessageSubject);
                sql.AddParam("@fMessageSendMethod", request.MessageSendMethod);
                sql.AddParam("@fUserNameID", request.UserNameID);


                var fRetVal = sql.AddOutputParam("@fRetVal", System.Data.SqlDbType.Int);
                var lRetMessageID = sql.AddOutputParam("@lRetMessageID", System.Data.SqlDbType.Int);
                sql.Execute();

                return new SendEmailResponse() { ReturnValue= fRetVal.GetSafeValue<int>(), ReturnMessageID = lRetMessageID.GetSafeValue<int>() };

            }
        }
    #endregion
    }
}