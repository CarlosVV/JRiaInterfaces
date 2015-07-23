using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
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