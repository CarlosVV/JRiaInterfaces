
using CES.CoreApi.Payout.Service.Contract.Constants;
using System.Runtime.Serialization;


namespace CES.CoreApi.Payout.Models
{

    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class PayoutTransactionRequest 
    {

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public int PersistenceID { get; set; }//required
        [DataMember(EmitDefaultValue = true)]
        public RequesterInfo RequesterInfo { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public BeneficiaryInfo Beneficiary { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public SenderInfo Sender { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public long TellerDrawerInstanceID { get; set; }
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public long OrderID { get; set; } //required
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public string OrderPIN { get; set; }//required
        public string OrderLookupCode { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public int RecAgentID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public int RecAgentLocID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string RecAgentBranch { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string RecAgentAddress { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string RecAgentCity { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string RecAgentState { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string RecAgentPostalCode { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string RecAgentCountry { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public int PayAgentID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public int PayAgentLocID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string PayAgentBranch { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string PayAgentAddress { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string PayAgentCity { get; set; }
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public string PayAgentState { get; set; }//required
        [DataMember(EmitDefaultValue = true)]
        public string PayAgentPostalCode { get; set; }
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public string PayAgentCountry { get; set; }//required
        [DataMember(EmitDefaultValue = true)]
        public int PayAgentCountryID { get; set; }

        [DataMember(EmitDefaultValue = true)]
        public string SendCurrency { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public decimal SendAmount { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public decimal SendCharge { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string PayoutCurrency { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public decimal PayoutAmount { get; set; }

        [DataMember(EmitDefaultValue = true)]
        public int PayoutMethodID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string CustomerRelationShip { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public int CustomerRelationShipID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public int TransferReasonID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string TransferReason { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public int ApproverID { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public string ConvertedCurrency { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public decimal ConvertedRate { get; set; }
        [DataMember(EmitDefaultValue = true)]
        public bool Override { get; set; }


    }
}
