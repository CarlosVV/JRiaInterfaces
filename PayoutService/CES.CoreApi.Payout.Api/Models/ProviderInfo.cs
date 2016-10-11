using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using CES.CoreApi.Payout.Service.Contract.Constants;

namespace CES.CoreApi.Payout.Models
{
    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class ProviderInfo
    {

        [DataMember]
        public int ProviderID { get; set; }
        [DataMember]
        public int ProviderTypeID { get; set; }
        [DataMember]
        public string ProviderName { get; set; }
    }
}
