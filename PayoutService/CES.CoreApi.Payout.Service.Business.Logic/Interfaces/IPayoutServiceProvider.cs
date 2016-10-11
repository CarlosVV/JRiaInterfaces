using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Business.Contract.Models;

namespace CES.CoreApi.Payout.Service.Business.Contract.Interfaces
{
    public interface IPayoutServiceProvider
    {
        GetTransactionInfoResponseModel GetTransactionInfo(GetTransactionInfoRequestModel request);

        PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request);

    }
}
