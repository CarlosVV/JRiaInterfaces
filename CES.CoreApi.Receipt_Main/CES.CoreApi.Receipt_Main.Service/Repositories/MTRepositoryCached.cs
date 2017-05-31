using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CES.CoreApi.Receipt_Main.Service.Models;

namespace CES.CoreApi.Receipt_Main.Service.Repositories
{
    public class MTRepositoryCached : MTRepository
    {
        internal MTGeneratePayoutResponse GeneratePayout(MTGeneratePayoutRequest mtGeneratePayoutRequest)
        {
            throw new NotImplementedException();
        }

        internal MTGenerateOrderSendResponse GenerateOrderSend(MTGenerateOrderSendRequest mtGenerateOrderSendRequest)
        {
            throw new NotImplementedException();
        }

        internal MTGenerateComplianceResponse GenerateCompliance(MTGenerateComplianceRequest mtGenerateComplianceRequest)
        {
            throw new NotImplementedException();
        }
    }
}