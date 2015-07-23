using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
    public class Phone : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string CountryCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AreaCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PhoneType { get; set; }
    }
}