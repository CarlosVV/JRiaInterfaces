using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;

namespace CES.CoreApi.ReferenceData.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ReferenceDataServiceDataContractNamespace)]
    public class GetIdentificationTypeRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int IdentificationTypeId { get; set; }

        [DataMember(IsRequired = true)]
        public int LocationDepartmentId { get; set; }
    }
}