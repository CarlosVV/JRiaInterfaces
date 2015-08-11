using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;

namespace CES.CoreApi.Agent.Service.Contract.Enumerations
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
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
        LocationCurrency = 8,
        [EnumMember]
        Full = Basic | Location | AgentCurrency | LocationCurrency,
        [EnumMember]
        Medium = Basic | Location
    }
}
