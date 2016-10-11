using CES.CoreApi.Payout.Service.Contract.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Models
{

    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class Money
    {
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }

    }
}
