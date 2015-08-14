using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class AgentCurrency: ExtensibleObject
    {
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public decimal Minimum { get; set; }
        [DataMember]
        public decimal Maximum { get; set; }
        [DataMember]
        public decimal DailyMaximum { get; set; }
    }
}