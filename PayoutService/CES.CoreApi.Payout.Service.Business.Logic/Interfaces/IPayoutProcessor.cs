using CES.CoreApi.Payout.Service.Business.Contract.Models;

namespace CES.CoreApi.Payout.Service.Business.Contract.Interfaces
{

    /// <summary>
    /// Handle and process calls.
    /// </summary>
    public interface IPayoutProcessor
    {

        GetTransactionInfoResponseModel GetTransactionInfo(GetTransactionInfoRequestModel request);
        PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request);

    }

}
