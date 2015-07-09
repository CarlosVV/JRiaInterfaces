using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;

namespace CES.CoreApi.ReferenceData.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ReferenceDataServiceDataContractNamespace)]
    public class GetIdentificationTypeListResponse: BaseResponse
    {
        public GetIdentificationTypeListResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<IdentificationTypeResponse> IdentificationTypeList { get; set; }
    }
}