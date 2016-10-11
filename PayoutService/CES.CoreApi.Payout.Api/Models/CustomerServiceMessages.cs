using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Contract.Constants;
using System.Runtime.Serialization;

namespace CES.CoreApi.Payout.Models
{
    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class CustomerServiceMessages
    {
        [DataMember]
        public string MessageID { get; set;}
        [DataMember]
        public string Category { get; set;}
        [DataMember]
        public DateTime MessageTime { get; set; }
        [DataMember]
        public string EnteredBy { get; set; }
        [DataMember]
        public string MessageBody { get; set; }

    }
}
