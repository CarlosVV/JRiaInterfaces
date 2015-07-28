using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class BirthInformation : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public DateTime DateOfBirth { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PlaceOfBirth { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CountryOfBirth { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string StateOfBirth { get; set; }
    }
}