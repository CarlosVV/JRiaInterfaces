using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Services
{
    public class MTService
    {
        private MTRepositoryCached _repository;

        public MTService()
        {
            _repository = new MTRepositoryCached();
        }

        internal MTGeneratePayoutResponse GeneratePayout(MTGeneratePayoutRequest mtGeneratePayoutRequest)
        {
            return _repository.GeneratePayout(mtGeneratePayoutRequest);
        }

        internal MTGenerateOrderSendResponse GenerateOrderSend(MTGenerateOrderSendRequest mtGenerateOrderSendRequest)
        {
            return _repository.GenerateOrderSend(mtGenerateOrderSendRequest);
        }

        internal MTGenerateComplianceResponse GenerateCompliance(MTGenerateComplianceRequest mtGenerateComplianceRequest)
        {
            return _repository.GenerateCompliance(mtGenerateComplianceRequest);
        }
    }
}