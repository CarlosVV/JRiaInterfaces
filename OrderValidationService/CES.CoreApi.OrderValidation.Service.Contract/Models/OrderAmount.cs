using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.OrderValidation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.OrderValidation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderValidationServiceDataContractNamespace)]
    public class OrderAmount : ExtensibleObject
    {
        [DataMember(IsRequired = true)]
        public decimal LocalAmount { get; set; }

        [DataMember(IsRequired = true)]
        public decimal ForeignAmount { get; set; }

        [DataMember(IsRequired = true)]
        public string CurrencyFrom { get; set; }

        [DataMember(IsRequired = true)]
        public string CurrencyTo { get; set; }

        [DataMember(IsRequired = true)]
        public decimal CustomerRate { get; set; }

        [DataMember(IsRequired = true)]
        public decimal CustomerFee { get; set; }

        [DataMember(IsRequired = true)]
        public decimal SurchargeFee { get; set; }

        [DataMember(IsRequired = true)]
        public decimal Tax { get; set; }

        [DataMember(IsRequired = true)]
        public bool ManualCustomerFee { get; set; }

        //[DataMember(IsRequired = true)]
        //public decimal TotalAmount
        //{
        //    get { return LocalAmount + CustomerFee + SurchargeFee + Tax; }
        //}
    }
}