using System.Runtime.Serialization;
using CES.CoreApi.Accounting.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AccountingServiceDataContractNamespace)]
    public class TransactionSummary : ExtensibleObject
    {
        [DataMember]
        public decimal TransferTotal { get; set; }

        [DataMember]
        public decimal UsdTotal { get; set; }

        [DataMember]
        public decimal LocalUsdTotal { get; set; }

        [DataMember]
        public decimal AmountTotal { get; set; }
    }
}