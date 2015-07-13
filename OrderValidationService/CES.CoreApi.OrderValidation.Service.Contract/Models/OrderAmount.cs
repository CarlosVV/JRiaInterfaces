using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.OrderValidation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.OrderValidation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderValidationServiceDataContractNamespace)]
    public class OrderAmount : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public decimal LocalAmount { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public decimal ForeignAmount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CurrencyFrom { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CurrencyTo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal CustomerRate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal CustomerFee { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal SurchargeFee { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal Tax { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool ManualCustomerFee { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal TotalAmount
        {
            get { return LocalAmount + CustomerFee + SurchargeFee + Tax; }
        }
    }
}