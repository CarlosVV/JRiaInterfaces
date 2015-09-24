using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.LimitVerification.Service.Contract.Constants;

namespace CES.CoreApi.LimitVerification.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.LimitVerificationServiceDataContractNamespace)]
    public class CheckPayingAgentLimitsResponse: BaseResponse
    {
        public CheckPayingAgentLimitsResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }
    }
}