using System.Data;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Data.Materializers
{
    public class PayingAgentMaterializer : IPayingAgentMaterializer
    {
        public PayingAgentModel Materialize(IDataReader reader, int agentId)
        {
            return new PayingAgentModel
            {
                Id = agentId,
                IsOnHold = reader.ReadValue<bool>("fOnHold"),
                OnHoldReason = reader.ReadValue<string>("fOnHoldReason"),
                Status = reader.ReadValue<PayingAgentStatus>("fStatus"),
                IsBeneficiaryLastName2Required = reader.ReadValue<bool>("fReqBenLastName2"),
                Name = reader.ReadValue<string>("fName"),
                Number = reader.ReadValue<string>("fExtNo")
            };
        }
    }
}
