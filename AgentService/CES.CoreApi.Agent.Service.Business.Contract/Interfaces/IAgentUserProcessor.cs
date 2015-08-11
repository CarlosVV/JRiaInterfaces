using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentUserProcessor
    {
        Task<ProcessSignatureResponseModel> ProcessSignature(ProcessSignatureRequestModel requestModel);
    }
}