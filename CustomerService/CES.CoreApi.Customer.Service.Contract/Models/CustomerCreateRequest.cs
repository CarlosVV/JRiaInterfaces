using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class CustomerCreateRequest: BaseRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CustomerId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ExternalCustomerId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CustomerAddress CustomerAddress { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CustomerName CustomerName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CustomerContact Contact { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime LastUsed { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime DateCreated { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ReferredBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int AgentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int AgentLocationId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsOnHold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsDisabled { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Note { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int StatusId { get; set; }
    }
}
