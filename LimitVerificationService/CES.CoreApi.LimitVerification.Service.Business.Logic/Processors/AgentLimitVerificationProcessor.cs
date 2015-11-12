﻿using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.LimitVerification.Service.Business.Contract.Enumerations;
using CES.CoreApi.LimitVerification.Service.Business.Contract.Interfaces;
using CES.CoreApi.LimitVerification.Service.Business.Contract.Models;

namespace CES.CoreApi.LimitVerification.Service.Business.Logic.Processors
{
    public class AgentLimitVerificationProcessor : IAgentLimitVerificationProcessor
    {
        private readonly IServiceHelper _serviceHelper;

        public AgentLimitVerificationProcessor(IServiceHelper serviceHelper)
        {
            if (serviceHelper == null)
                throw new CoreApiException(TechnicalSubSystem.LimitVerificationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "serviceHelper");

            _serviceHelper = serviceHelper;
        }

        public CheckPayingAgentLimitsModel CheckPayingAgentLimits(int agentId, int agentLocationId, string currency, decimal amount)
        {
            var result = new CheckPayingAgentLimitsModel();
            
            var agentCurrency = GetAgentCurrency(agentId, currency);

            
            return null;
            
        }

      

        public CheckReceivingAgentLimitsModel CheckReceivingAgentLimits(int agentId, int userId, string currency, decimal amount)
        {
            throw new System.NotImplementedException();
        }


        private PayingAgentCurrency GetAgentCurrency(int agentId, string currency)
        {
            //var request = new GetAgentCurrencyRequest { AgentId = agentId, Currency = currency };
            //var agentCurrency = _serviceHelper
            //    .Execute<IAgentCurrencyService, GetAgentCurrencyResponse>(p => p.GetAgentCurrency(request))
            //    .PayingAgentCurrencyResponse;

            //return agentCurrency;
            return null;
        }

        private Payability ProcessPayingAgentLimits(PayingAgentCurrency currency, decimal amount)
        {
            if (currency == null)
                return Payability.CurrencyNotPaid;


            //if (amount < currency.Minimum)
            //    return Payability.BelowTransactionMinimum;

            //if (amount > currency.Maximum)
            //    return Payability.AboveTransactionMaximum;

            //if (currency.DailyMaximum > 0)
            //{

            //}
            return Payability.Approved;
        }
    }

    internal class PayingAgentCurrency
    {
    }
}