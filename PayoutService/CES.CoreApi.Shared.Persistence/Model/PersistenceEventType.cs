using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Model
{
    public enum PersistenceEventType
    {
        Undefined = 0,
        PayoutPinRequestInfoRequest =1,
        PayoutGetProviderInfoRequest = 2,
        PayoutGetProviderInfoResponse = 3,
        PayoutPinRequestInfoResponse =4,
        PayoutRequest=5,
        ComplianceCheckPayoutRequest=6,
        ComplianceExternalCheckPayoutRequest=7,
        ComplianceExternalCheckPayoutResponse=8,
        ComplianceCheckPayoutResponse=9,
        PayoutTransactionRequest=10,
        PayoutTransactionResponse =11,
        PayoutExternalConfirmationRequest =12,
        PayoutExternalConfirmationResponse=13,
        PayoutResponseSuccess =14,
        FaultException =15,
        ComplianceWLFRequest= 16,
        ComplianceWLFResponse = 17,
        SendEmailRequest = 18,
        SendEmailResponse = 19,
        PlaceOrderOnHoldRequest = 20,
        PlaceOrderOnHoldResponse = 21,
        ValidateOrderRequest = 22,
        ValidateOrderResponse = 23,
        PayoutResponse = 24,

    }
}


