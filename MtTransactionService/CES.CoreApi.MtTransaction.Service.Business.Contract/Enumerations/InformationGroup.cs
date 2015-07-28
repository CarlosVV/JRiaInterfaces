using System;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations
{
    [Flags]
    public enum InformationGroup
    {
        Undefined = 0,
        Basic = 1,
        Customer = 2,
        Beneficiary = 4,
        ProcessingInformation = 8,
        ComplianceInformation = 16,
        MoneyTransferDetails = 32,
        PayingAgent = 64,
        ReceivingAgent = 128,
        TransactionStatus = 256,
        Full = Basic | Customer | Beneficiary | ProcessingInformation | ComplianceInformation | MoneyTransferDetails | PayingAgent | ReceivingAgent | TransactionStatus,
        Medium = Basic | Customer | Beneficiary | PayingAgent | ReceivingAgent | TransactionStatus
    }
}
