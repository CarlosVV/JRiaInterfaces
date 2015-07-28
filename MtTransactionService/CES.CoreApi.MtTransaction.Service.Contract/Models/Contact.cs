using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class Contact
    {
        [DataMember(EmitDefaultValue = false)]
        public ICollection<Phone> PhoneList { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? NoSms { get; set; }
    }
}