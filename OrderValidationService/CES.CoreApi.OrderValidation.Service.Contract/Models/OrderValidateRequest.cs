using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.OrderValidation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.OrderValidation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderValidationServiceDataContractNamespace)]
    public class OrderValidateRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public OrderAmount Amount { get; set; }

        [DataMember(IsRequired = true)]
        public int CorrespondentId { get; set; }

        [DataMember(IsRequired = true)]
        public int CustomerId { get; set; }

        [DataMember(IsRequired = true)]
        public Beneficiary Beneficiary { get; set; }
    }
}