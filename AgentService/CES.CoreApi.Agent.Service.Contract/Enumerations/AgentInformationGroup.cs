using System;
using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;

namespace CES.CoreApi.Agent.Service.Contract.Enumerations
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    [Flags]
    public enum AgentInformationGroup
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        Basic = 1,
        [EnumMember]
        Location = 2,
        [EnumMember]
        AgentCurrency = 4,
        [EnumMember]
        LocationWithCurrency = 8,
        [EnumMember]
        AllLocationsWithoutCurrency = 16,
        [EnumMember]
        Full = Basic | Location | AgentCurrency | AllLocationsWithoutCurrency,
        [EnumMember]
        Medium = Basic | LocationWithCurrency
    }
}
