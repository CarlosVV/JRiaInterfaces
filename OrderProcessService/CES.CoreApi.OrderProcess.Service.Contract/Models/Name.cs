using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
    public class Name : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string LastName1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string LastName2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FullName { get; set; }
    }
}