using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
    public class Customer : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Name Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Address Address { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Contact Contact { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public BirthInformation BirthInformation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public TaxInformation TaxInformation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Nationality { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Gender { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ICollection<Identification> Identification { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? IsOnHold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? IsDisabled { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Note { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? IsChanged { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime LastUsed { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime DateCreated { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CustomerId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ExternalCustomerId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ReferredBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int AgentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int StatusId { get; set; }
    }
}