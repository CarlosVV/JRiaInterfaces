using CES.CoreApi.Shared.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class PayoutTransactionRequestModel
    {
        public PayoutTransactionRequestModel()
        {
            RequesterInfo = new RequesterInfoModel();
            ComplianceRun = new ComplianceRunModel();
            Beneficiary = new BeneficiaryInfoModel();
            Sender = new SenderInfoModel();
        }


        public int PersistenceID { get; set; }//required
        public PersistenceModel Persistence { get; set; }

        public RequesterInfoModel RequesterInfo { get; set; }

        public ComplianceRunModel ComplianceRun { get; set; }
        public BeneficiaryInfoModel Beneficiary { get; set; }
        public SenderInfoModel Sender { get; set; }
        public long TellerDrawerInstanceID { get; set; }
        public long OrderID { get; set; } //required
        public string OrderPIN { get; set; }//required
        public string OrderLookupCode { get; set; }

        public int RecAgentID { get; set; }
        public int RecAgentLocID { get; set; }
        public string RecAgentBranch { get; set; }
        public string RecAgentAddress { get; set; }
        public string RecAgentCity { get; set; }
        public string RecAgentState { get; set; }
        public string RecAgentPostalCode { get; set; }
        public string RecAgentCountry { get; set; }

        public int PayAgentID { get; set; }
        public int PayAgentLocID { get; set; }
        public string PayAgentBranch { get; set; }
        public string PayAgentAddress { get; set; }
        public string PayAgentCity { get; set; }
        public string PayAgentState { get; set; }//required
        public string PayAgentPostalCode { get; set; }
        public string PayAgentCountry { get; set; }//required
        public int PayAgentCountryID { get; set; }

        public string SendCurrency { get; set; }
        public decimal SendAmount { get; set; }
        public decimal SendCharge { get; set; }
        public string PayoutCurrency { get; set; }
        public decimal PayoutAmount { get; set; }

        public int PayoutMethodID { get; set; }      
        public int CustomerRelationShipID { get; set; }
        public string CustomerRelationShip { get; set; }
        public int TransferReasonID { get; set; }
        public string TransferReason { get; set; }
        public int ApproverID { get; set; }
        public string ConvertedCurrency { get; set; }
        public decimal ConvertedRate { get; set; }
        public bool Override { get; set; }
        public int ProviderID { get; set; }



    }
}


