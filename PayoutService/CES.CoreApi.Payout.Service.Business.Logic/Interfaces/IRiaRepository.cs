using CES.CoreApi.Payout.Service.Business.Contract.Models;
using System.Collections.Generic;

namespace CES.CoreApi.Payout.Service.Business.Contract.Interfaces
{
    public interface IRiaRepository
    {
        GetTransactionInfoResponseModel GetOrderInfo(GetTransactionInfoRequestModel requestModel);

        GetTransactionInfoResponseModel GetOrderInfoExternal(
            GetTransactionInfoRequestModel requestModel,
            GetTransactionInfoResponseModel partnerCallResponse,
            string xmlProviderRequiredFields);

        PayoutTransactionResponseModel PayoutOrder(PayoutTransactionRequestModel requestModel);


        PayoutTransactionResponseModel PayoutConfirmExternalValidate(PayoutTransactionRequestModel requestModel, ref string log);

        PayoutTransactionResponseModel PayoutConfirmComplianceWatchlist(PayoutTransactionRequestModel requestModel,ref string log);

        CreateOrderFromProviderDataResponse OrderCreateFromProviderData(PayoutTransactionRequestModel requestModel);

        PayoutTransactionResponseModel SaveTransactionAsPaid(PayoutTransactionRequestModel requestModel);

        IEnumerable<WLFMatchRuleModel> ComplianceMatchRulesGet(PayoutTransactionRequestModel requestModel);
        ComplianceProviderResponseModel GetComplianceProvider (PayoutTransactionRequestModel requestModel);

        PayoutTransactionResponseModel ComplianceWriteIssue(PayoutTransactionRequestModel requestModel, string issueDesc);

        CurrencyCodeModel GetCurrencyCodeData(int codeNum, string codeText);

        CountryCodeModel GetCountryCodeData(int lookupType, string countryCode);

        IEnumerable<IssueModel> ComplianceListIssues(PayoutTransactionRequestModel requestModel);

        SendEmailResponseModel EnviarMail(SendEmailRequestModel request, ref string log);
        PlaceOnLegalHoldResponseModel PlaceLegalHoldPayout(PlaceOnLegalHoldRequestModel requestModel, ref string log);
    }
}
