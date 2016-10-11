using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents
{

    /// <summary>
    /// Defines the calls to the various correspondents.
    /// Creates a uniform interface that all correspendents
    /// calls can conform to.
    /// 
    /// Author  : David Go
    /// </summary>
    public interface ICorrespondentAPI
    {
        void SetProviderInfo(ProviderModel providerInfo);

        GetTransactionInfoResponseModel GetTransactionInfo(GetTransactionInfoRequestModel request);

        PayoutTransactionResponseModel ValidatePayoutData(PayoutTransactionRequestModel request);

        PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request);

        CreateOrderFromProviderDataResponse CreateOrderFromProviderData(PayoutTransactionRequestModel request);

        PayoutTransactionResponseModel PayoutComplianceCheck(PayoutTransactionRequestModel request);

        bool WriteComplianceReviewIssues();

        ConfirmPayoutResponseModel ConfirmPayout(int persistenceID, string orderPin, string payDocID, DateTime payDocDate, int providerID);

        PayoutTransactionResponseModel SaveTransactionAsPaid(PayoutTransactionRequestModel request);
        PayoutTransactionResponseModel PlaceOrderLegalHold(PayoutTransactionRequestModel request);

    }
}
