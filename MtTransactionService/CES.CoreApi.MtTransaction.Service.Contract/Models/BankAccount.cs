using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class BankAccount : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string AccountNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int AccountType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AccountTypeName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string UnitaryAccountNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string UnitaryAccountType { get; set; }
    }
}