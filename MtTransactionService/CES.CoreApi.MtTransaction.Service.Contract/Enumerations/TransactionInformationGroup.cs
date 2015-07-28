using System;
using System.Runtime.Serialization;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Enumerations
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    [Flags]
    public enum TransactionInformationGroup
    {
        [EnumMember] Undefined = 0,
        [EnumMember] Basic = 1,
        [EnumMember] Customer = 2,
        [EnumMember] Beneficiary = 4,
        [EnumMember] ProcessingInformation = 8,
        [EnumMember] ComplianceInformation = 16,
        [EnumMember] MoneyTransferDetails = 32,
        [EnumMember] PayingAgent = 64,
        [EnumMember] ReceivingAgent = 128,
        [EnumMember] TransactionStatus = 256,
        [EnumMember] Full = Basic | Customer | Beneficiary | ProcessingInformation | ComplianceInformation | MoneyTransferDetails | PayingAgent | ReceivingAgent | TransactionStatus,
        [EnumMember] Medium = Basic | Customer | Beneficiary | PayingAgent | ReceivingAgent | TransactionStatus
    }
}