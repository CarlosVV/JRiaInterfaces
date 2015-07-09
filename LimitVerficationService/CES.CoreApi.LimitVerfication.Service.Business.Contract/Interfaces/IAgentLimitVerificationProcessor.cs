﻿using CES.CoreApi.LimitVerfication.Service.Business.Contract.Models;

namespace CES.CoreApi.LimitVerfication.Service.Business.Contract.Interfaces
{
    public interface IAgentLimitVerificationProcessor
    {
        CheckPayingAgentLimitsModel CheckPayingAgentLimits(int agentId, int agentLocationId, string currency, decimal amount);
        CheckReceivingAgentLimitsModel CheckReceivingAgentLimits(int agentId, int userId, string currency, decimal amount);
    }
}