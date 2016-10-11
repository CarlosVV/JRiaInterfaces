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
    public class ReturnInfo
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }
        [DataMember]
        public bool AvailableForPayout { get; set; }
       
    }
}
