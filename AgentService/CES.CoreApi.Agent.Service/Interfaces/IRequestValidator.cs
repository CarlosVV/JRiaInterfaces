using CES.CoreApi.Agent.Service.Contract.Models;

namespace CES.CoreApi.Agent.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(GetAgentCurrencyRequest request);
        void Validate(ProcessSignatureRequest request);
        void Validate(GetPayingAgentRequest request);
    }
}