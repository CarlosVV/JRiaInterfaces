using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.OrderValidation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.OrderValidation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderValidationServiceDataContractNamespace)]
    public class Beneficiary: ExtensibleObject
    {
        [DataMember(IsRequired = true)]
        public int CustomerId { get; set; }

        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }

        [DataMember(IsRequired = true)]
        public string LastName1 { get; set; }

        [DataMember(IsRequired = true)]
        public string LastName2 { get; set; }

        [DataMember(IsRequired = true)]
        public string MiddleName { get; set; }
    }
}