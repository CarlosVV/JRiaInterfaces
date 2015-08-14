using System.Runtime.Serialization;
using System.Security.AccessControl;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class GetPayingAgentResponse: BaseResponse
    {
        public GetPayingAgentResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember(EmitDefaultValue = false)]
        public PayingAgent PayingAgent { get; set; }
    }
}