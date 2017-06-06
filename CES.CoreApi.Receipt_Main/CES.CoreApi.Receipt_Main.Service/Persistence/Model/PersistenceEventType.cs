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
        //Payout Persistence
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
        PayoutErrorResponse =25,
        InvalidRequestModel = 26,
        ErrorResponse = 27,

        //OrderSave Persistence
        OrderSaveRequest = 50,
        IDValidationRequest = 51,
        IDValidationResponse = 52,
        TaxGenerationResponse = 53,
        ReceiptGenerationError = 54,
        TaxGenerationRequest = 55,
        TaxCreateCAFRequest = 56,
        TaxSearchCAFRequest = 57,
        TaxCreateCAFResponse = 58,
        TaxSearchCAFResponse = 59,
        TaxUpdateCAFResponse = 60,
        TaxDeleteCAFResponse = 61,
        TaxGetFolioResponse = 62,
        TaxUpdateFolioResponse = 63,
        TaxCreateDocumentResponse = 64,
        TaxSearchDocumentResponse = 65,
        TaxGenerateReceiptResponse = 66,
        TaxSIIGetDocumentBatchResponse = 67,
        TaxUpdateCAFRequest = 68,
        TaxSearchCAFByTypeRequest = 69,
        TaxDeleteCAFRequest = 70,
        TaxUpdateFolioRequest = 71,
        TaxCreateDocumentRequest = 72,
        TaxSearchDocumentRequest = 73,
        TaxGenerateReceiptRequest = 74,
        TaxSIIGetDocumentRequest = 75,
        TaxSIISendDocumentRequest = 76,
        TaxSIIGetDocumentBatchRequest = 77,
    }
}


