using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class CustomerName : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string LastName1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string LastName2 { get; set; }
    }
}