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
        LocationWithCurrency = 8,
        AllLocationsWithoutCurrency = 16,
        Full = Basic | AgentCurrency | AllLocationsWithoutCurrency,
        Medium = Basic | LocationWithCurrency
    }
}
