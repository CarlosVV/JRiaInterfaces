using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Compliance.Service.Contract.Constants;


namespace CES.CoreApi.Compliance.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ComplianceServiceDataContractNamespace)]
    public class CheckPayoutResponse : BaseResponse
    {
        public CheckPayoutResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
