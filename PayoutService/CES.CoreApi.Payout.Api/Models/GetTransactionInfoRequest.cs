using System;

using CES.CoreApi.Payout.Service.Contract.Constants;
using System.Runtime.Serialization;


namespace CES.CoreApi.Payout.Models
{
    
    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class GetTransactionInfoRequest 
    {

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public int PersistenceID { get; set; }//required

        [DataMember(EmitDefaultValue = true)]
        public RequesterInfo RequesterInfo { get; set; }

        [DataMember(IsRequired = true)]
        public string OrderPIN { get; set; }

        [DataMember(IsRequired = true)]
        public int OrderID { get; set; }

        [DataMember(IsRequired = false)]
        public string CountryTo { get; set; }

        [DataMember(IsRequired = false)]
        public string StateTo { get; set; }
    }

}
