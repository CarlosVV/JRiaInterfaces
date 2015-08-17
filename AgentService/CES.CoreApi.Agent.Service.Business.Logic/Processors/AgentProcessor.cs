﻿using System.Linq;
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
            //Get agent details
            var response = await GetRepository<IPayingAgentRepository>().GetAgent(agentId);

            //Get agent currency information
            if ((detalizationLevel & PayingAgentDetalizationLevel.AgentCurrency) == PayingAgentDetalizationLevel.AgentCurrency)
                response.Currency = await GetRepository<IPayingAgentRepository>().GetAgentCurrency(agentId, currencySymbol);

            //Get agent location details
            if ((detalizationLevel & PayingAgentDetalizationLevel.Location) != PayingAgentDetalizationLevel.Location &&
                (detalizationLevel & PayingAgentDetalizationLevel.LocationWithCurrency) != PayingAgentDetalizationLevel.LocationWithCurrency &&
                (detalizationLevel & PayingAgentDetalizationLevel.AllLocationsWithoutCurrency) != PayingAgentDetalizationLevel.AllLocationsWithoutCurrency)
                return response;

            //Get location(s)
            response.Locations = await GetRepository<IPayingAgentLocationRepository>().GetLocations(agentId, locationId, true);

            //Get location currency information only if location ID  > 0 - only for one location
            if ((detalizationLevel & PayingAgentDetalizationLevel.LocationWithCurrency) != PayingAgentDetalizationLevel.LocationWithCurrency)
                return response;

            var location = response.Locations.FirstOrDefault(p => p.Id == locationId);
            if (location == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService, SubSystemError.AgentServiceLocationIsNotFound, locationId, agentId);

            //Get currency details for particular location
            location.Currency = await GetRepository<IPayingAgentLocationRepository>().GetLocationCurrency(locationId, currencySymbol);

            return response;
        }

        #endregion

        private T GetRepository<T>() where T:  class
        {
            return _repositoryFactory.GetInstance<T>();
        }
    }
}