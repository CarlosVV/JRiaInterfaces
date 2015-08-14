﻿using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentProcessor
    {
        Task<PayingAgentModel> GetPayingAgent(int agentId, int locationId, string currencySymbol, InformationGroup detalizationLevel);
    }
}