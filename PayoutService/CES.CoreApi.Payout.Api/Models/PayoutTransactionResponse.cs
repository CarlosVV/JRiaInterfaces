using System;

using CES.CoreApi.Payout.Service.Contract.Constants;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace CES.CoreApi.Payout.Models
{
    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class PayoutTransactionResponse : BaseResponse
    {

        public PayoutTransactionResponse(int errorCode, string errorMessage)
        {
            ReturnInfo = new  ReturnInfo() { ErrorCode = errorCode, ErrorMessage=errorMessage };
            Message = errorMessage;
        }

       
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public int PersistenceID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ConfirmationNumber { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Money BeneficiaryFee { get; set; }
        [DataMember]
        public ReturnInfo ReturnInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<PayoutFields> PayoutRequiredFields { get; set; }

        [DataMember]
        public bool IsValid
        {
            get
            {
                return ReturnInfo.ErrorCode == 0;
            }
        }

        [DataMember]
        public string PIN { get; set; }
        [DataMember]
        public long OrderID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Money PayoutCommission { get; set; }
        [DataMember]
        public Money BeneficiaryPayout { get; set; }
        [DataMember]
        public DateTime ResponseDateTimeUTC { get; set; }
        [DataMember]
        public string OrderStatus { get; set; }


    }
}
