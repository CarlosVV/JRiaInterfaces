using CES.CoreApi.Agent.Service.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentUserProcessor
    {
        ProcessSignatureResponseModel ProcessSignature(ProcessSignatureRequestModel requestModel);
    }
}