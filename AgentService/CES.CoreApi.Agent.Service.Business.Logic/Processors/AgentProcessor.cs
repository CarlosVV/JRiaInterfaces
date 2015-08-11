using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;

namespace CES.CoreApi.Agent.Service.Business.Logic.Processors
{
    public class AgentProcessor : IAgentProcessor
    {
        #region Core

        private readonly IRepositoryFactory _repositoryFactory;

        public AgentProcessor(IRepositoryFactory repositoryFactory)
        {
            if (repositoryFactory == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "repositoryFactory");

            _repositoryFactory = repositoryFactory;
        }

        #endregion

        #region MyRegion


        public Task<GetAgentResponseResponseModel> GetPayingAgent(int agentId, int locationId, string currencySymbol, InformationGroup detalizationLevel)
        {
            var agentRepository = _repositoryFactory.GetInstance<IPayingAgentRepository>();

            var response = agentRepository.GetAgent(agentId);

            if ((detalizationLevel & InformationGroup.AgentCurrency) == InformationGroup.AgentCurrency)
            {
                response.AgentCurrencyInformation = agentRepository.GetAgentCurrency(agentId, currencySymbol);
            }

        }

        public async Task<PayingAgentCurrencyModel> GetAgentCurrency(int agentId, string currencySymbol)
        {
            return await Task.Run(() => _repository.Get(agentId, currencySymbol));
        } 

        #endregion
    }
}