using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;

namespace CES.CoreApi.Agent.Service.Business.Logic.Processors
{
    public class AgentUserProcessor : IAgentUserProcessor
    {
        #region Core

        private readonly IAgentUserRepository _agentUserRepository;

        public AgentUserProcessor(IAgentUserRepository agentUserRepository)
        {
            if (agentUserRepository == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "AgentUserRepository");

            _agentUserRepository = agentUserRepository;
        }

        #endregion

        #region Public methods

        public async Task<ProcessSignatureResponseModel> ProcessSignature(ProcessSignatureRequestModel requestModel)
        {
            //do whatever needs to be done to save agent signature to database, use _agentUserRepository to access Image DB

            return await Task.Run(() => new ProcessSignatureResponseModel {IsCompleted = true});
        } 

        #endregion
    }
}
