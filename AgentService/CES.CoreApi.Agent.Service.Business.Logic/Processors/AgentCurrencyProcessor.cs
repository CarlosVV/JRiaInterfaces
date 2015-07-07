using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;

namespace CES.CoreApi.Agent.Service.Business.Logic.Processors
{
    public class AgentCurrencyProcessor : IAgentCurrencyProcessor
    {
        #region Core

        private readonly IAgentCurrencyRepository _repository;

        public AgentCurrencyProcessor(IAgentCurrencyRepository repository)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            _repository = repository;
        }

        #endregion

        #region MyRegion

        public PayingAgentCurrencyModel GetAgentCurrent(int correspondentId, string symbol)
        {
            return _repository.Get(correspondentId, symbol);
        } 

        #endregion
    }
}