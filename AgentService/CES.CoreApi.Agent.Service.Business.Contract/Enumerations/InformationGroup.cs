using System;

namespace CES.CoreApi.Agent.Service.Business.Contract.Enumerations
{
    [Flags]
    public enum InformationGroup
    {
        Undefined = 0,
        Basic = 1,
        Location = 2,
        AgentCurrency = 4,
        LocationCurrency = 8,
        Full = Basic | Location | AgentCurrency | LocationCurrency,
        Medium = Basic | Location
    }
}
