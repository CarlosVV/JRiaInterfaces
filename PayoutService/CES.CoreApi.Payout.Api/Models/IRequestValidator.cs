using CES.CoreApi.Payout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Contract.Interfaces
{

    /// <summary>
    /// Defines methods for Validating Requests
    /// </summary>
    public interface IRequestValidator
    {

        GetTransactionInfoResponse Validate(GetTransactionInfoRequest request);
        PayoutTransactionResponse Validate(PayoutTransactionRequest request);

    }

}
