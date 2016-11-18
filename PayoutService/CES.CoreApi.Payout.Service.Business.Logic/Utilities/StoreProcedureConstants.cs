using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Utilities
{
    public class StoreProcedureConstants
    {
        public const string mt_sp_Payout_OrderInfo_Get = "mt_sp_Payout_OrderInfo_Get";
        public const string mt_sp_Payout_OrderInfo_Get_External = "mt_sp_Payout_OrderInfo_Get_External";
        public const string PayoutConfirmExternalValidate = "mt_sp_Payout_Confirm_External_Validate";
        public const string PayoutConfirmComplianceWatchList = "mt_sp_Payout_Confirm_Compliance_WatchList";
        public const string PayoutConfirm = "mt_sp_Payout_Confirm";
        public const string GetCurrencyCodeAndNumber = "mt_GetCurrencyCodeAndNumber";
        public const string GetCountryCode = "ws_sp_WalmartUSAPI_Country_Code_Lookup";
        public const string OrderCreateFromProviderData = "mt_sp_Payout_Order_Create_FromProviderData";
        public const string SaveTransactionAsPaid = "mt_sp_Payout_MarkPaid";
        public const string ComplianceMatchRulesGet = "compl_sp_Watchlist_Match_Rules_Get";
        public const string ComplianceWriteReviewIssue = "compl_sp_ReviewIssue_Create";
        public const string GetCountryAll = "ol_sp_systblRegCountries_GetAll";
        public const string GetLocInfoForExternalLocNo = "mt_sp_GetLocInfoForExternalLocNo";
        public const string ComplianceListIssues = "compl_sp_ReviewIssues_Items_List_MT";
        public const string CommServerMessage = "sys_sp_CommServer_Message_ToSend_Setup";
        public const string TransactionPlaceLegalHoldPayout = "compl_sp_Transaction_PlaceOnLegalHold";
    }
}
