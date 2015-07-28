using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class Identification : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string IdNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IdType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime ExpirationDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime IssuedDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IssuedBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IssuedCountry { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IssuedState { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IdTaxCountry { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsChanged { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsPrimaryId { get; set; }
    }
}