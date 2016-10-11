
using CES.CoreApi.Payout.Service.Contract.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace CES.CoreApi.Payout.Models
{
    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class GetTransactionInfoResponse : BaseResponse
    {

        public GetTransactionInfoResponse(int errorCode, string errorMessage)
        {
            ReturnInfo = new ReturnInfo() { ErrorCode = errorCode, ErrorMessage = errorMessage };
            Message = errorMessage;
        }

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public int PersistenceID { get; set; }
     
        [DataMember(EmitDefaultValue = false)]
        public string OrderID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public DateTime TransferDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string TransferStatus { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Money SendAmount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Money PayoutAmount { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ExchangeRate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Money Comission { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Money ReceiverComission { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Money SenderComission { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string RecAgentID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CountryFrom { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CountryTo { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PayDataMessage { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string SenderIsResident { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ReceiverIsResident { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public DateTime PayDataNotAfterDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool PayDataNotAfterDateSpecified { get; set; }
        [DataMember (EmitDefaultValue = false)]
        public DateTime PayDataNotBeforeDate { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool PayDataNotBeforeDateSpecified { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool OnLegalHold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ReceiverFullCountryName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string OrderPIN { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PASeqID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PayAgent { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PayAgentBranchName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string PayAgentBranchNo { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string DeliveryMethod { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public decimal BeneficiaryTax { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public decimal NetAmount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ReturnInfo ReturnInfo { get; set; }

        //customer service messages
        [DataMember(EmitDefaultValue = false)]
        public List<CustomerServiceMessages> CustomerServiceMessages { get; set; }

        //fields required
        [DataMember(EmitDefaultValue = false)]
        public List<PayoutFields> PayoutRequiredFields { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public SenderInfo SenderInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public BeneficiaryInfo BeneficiaryInfo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ProviderInfo ProviderInfo { get; set; }

        [DataMember]
        public bool IsValid
        {
            get
            {
                return ReturnInfo.ErrorCode == 0;
            }
        }
    }
}
