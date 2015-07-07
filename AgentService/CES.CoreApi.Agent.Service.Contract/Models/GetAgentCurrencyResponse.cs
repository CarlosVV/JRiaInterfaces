using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class GetAgentCurrencyResponse : BaseResponse
    {
        public GetAgentCurrencyResponse(ICurrentDateTimeProvider currentDateTimeProvider)
            : base(currentDateTimeProvider)
        {
        }

        [DataMember(EmitDefaultValue = false)]
        public PayingAgentCurrency PayingAgentCurrencyResponse { get; set; }
    }
}