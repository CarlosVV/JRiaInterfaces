using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;


namespace CES.CoreApi.Payout.Service.Business.Logic.Processors
{

    /// <summary>
    /// Process Payout Requests.
    /// </summary>
    public class PayoutProcessor : IPayoutProcessor
    {
        private readonly IPayoutServiceProvider _payoutProvider;
        public PayoutProcessor(IPayoutServiceProvider payoutProvider)
        {

            //if (responseFactory == null)
            //    throw new CoreApiException(TechnicalSubSystem.ComplianceService,
            //      SubSystemError.GeneralRequiredParameterIsUndefined, "responseFactory");

            _payoutProvider = payoutProvider;

        }

        public GetTransactionInfoResponseModel GetTransactionInfo(GetTransactionInfoRequestModel request)
        {
            return _payoutProvider.GetTransactionInfo(request);
        }

        public PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request)
        {
            return _payoutProvider.PayoutTransaction(request);
        }


    }
}
