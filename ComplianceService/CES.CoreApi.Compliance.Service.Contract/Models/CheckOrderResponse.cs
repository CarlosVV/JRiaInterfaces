using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Compliance.Service.Contract.Constants;

namespace CES.CoreApi.Compliance.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ComplianceServiceDataContractNamespace)]
    public class CheckOrderResponse: BaseResponse
    {
        public CheckOrderResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public string OrderNumber{ get; set; }

        [DataMember]
        public string OrderBody { get; set; }
    }
}
