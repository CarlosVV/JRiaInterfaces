using System.Linq;
using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

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


        public async Task<PayingAgentModel> GetPayingAgent(int agentId, int locationId, string currencySymbol, PayingAgentDetalizationLevel detalizationLevel)
        {
            var agentRepository = _repositoryFactory.GetInstance<IPayingAgentRepository>();

            //Get agent details
            var response = await agentRepository.GetAgent(agentId);

            //Get agent currency information
            if ((detalizationLevel & PayingAgentDetalizationLevel.AgentCurrency) == PayingAgentDetalizationLevel.AgentCurrency)
                response.Currency = await agentRepository.GetAgentCurrency(agentId, currencySymbol);

            //Get agent location details
            if ((detalizationLevel & PayingAgentDetalizationLevel.Location) != PayingAgentDetalizationLevel.Location &&
                (detalizationLevel & PayingAgentDetalizationLevel.LocationWithCurrency) != PayingAgentDetalizationLevel.LocationWithCurrency &&
                (detalizationLevel & PayingAgentDetalizationLevel.AllLocationsWithoutCurrency) != PayingAgentDetalizationLevel.AllLocationsWithoutCurrency)
                return response;

            var locationRepository = _repositoryFactory.GetInstance<IAgentLocationRepository>();

            //Get location(s)
            response.Locations = await locationRepository.GetLocations(agentId, locationId, true);

            //Get location currency information only if location ID  > 0 - only for one location
            if ((detalizationLevel & PayingAgentDetalizationLevel.LocationWithCurrency) != PayingAgentDetalizationLevel.LocationWithCurrency)
                return response;

            var location = response.Locations.FirstOrDefault(p => p.Id == locationId);
            if (location == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService, SubSystemError.AgentServiceLocationIsNotFound, locationId, agentId);

            //Get currency details for particular location
            location.Currency = await locationRepository.GetLocationCurrency(locationId, currencySymbol);

            return response;
        }


        public async Task<ReceivingAgentModel> GetReceivingAgent(int agentId, int locationId, ReceivingAgentDetalizationLevel detalizationLevel)
        {
            var agentRepository = _repositoryFactory.GetInstance<IReceivingAgentRepository>();

            //Get agent details
            var response = await agentRepository.GetAgent(agentId);

            return response;
        }

        #endregion

    }
}