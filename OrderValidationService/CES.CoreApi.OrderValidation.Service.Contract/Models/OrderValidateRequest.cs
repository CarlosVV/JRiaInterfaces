using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.OrderValidation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.OrderValidation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderValidationServiceDataContractNamespace)]
    public class OrderValidateRequest : BaseRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public OrderAmount Amount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CorrespondentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CustomerId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Beneficiary Beneficiary { get; set; }
    }
}