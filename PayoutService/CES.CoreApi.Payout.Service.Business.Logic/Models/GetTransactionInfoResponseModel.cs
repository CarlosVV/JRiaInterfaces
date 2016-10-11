using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class GetTransactionInfoResponseModel
    {

        public GetTransactionInfoResponseModel()
        {
            
            BeneficiaryInfo = new BeneficiaryInfoModel();
            SenderInfo = new SenderInfoModel();
            CustomerServiceMessages = new List<CustomerServiceMessagesModel>();
            PayoutRequiredFields = new List<PayoutFieldsModel>();
            ProviderInfo = new ProviderInfoModel();
            ReturnInfo = new ReturnInfoModel();
            PersistenceID = -1;
        }

        public GetTransactionInfoResponseModel(int errorCode, string errorMessage, int persistenceID)
        {
            ReturnInfo = new ReturnInfoModel() { ErrorCode = errorCode, ErrorMessage = errorMessage};
            PersistenceID = persistenceID;

        }
        public int PersistenceID { get; set; }
      
        //Display info
        public string OrderID { get; set; }
        public DateTime TransferDate { get; set; }
        public string TransferStatus { get; set; }

        public MoneyModel SendAmount { get; set; }
        public MoneyModel PayoutAmount { get; set; }
        public string ExchangeRate { get; set; }
        public MoneyModel Comission { get; set; }
        public MoneyModel ReceiverComission { get; set; }
        public MoneyModel SenderComission { get; set; }

        public string RecAgentID { get; set; }
        public string CountryFrom { get; set; }
        public string CountryTo { get; set; }
        public string PayDataMessage { get; set; }
        public string SenderIsResident { get; set; }
        public string ReceiverIsResident { get; set; }
        public DateTime PayDataNotAfterDate { get; set; }
        public bool PayDataNotAfterDateSpecified { get; set; }
        public DateTime PayDataNotBeforeDate { get; set; }
        public bool PayDataNotBeforeDateSpecified { get; set; }
        public bool OnLegalHold { get; set; }


        public string ReceiverFullCountryName { get; set; }
        public string ReceiverPostalCode { get; set; }
        public string OrderPIN { get; set; }
        public string PASeqID { get; set; }
        public string PayAgent { get; set; }
        public string PayAgentBranchName { get; set; }
        public string PayAgentBranchNo { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal BeneficiaryTax { get; set; }
        public decimal NetAmount { get; set; }


        //return  Info
        public ReturnInfoModel ReturnInfo { get; set; }

        //customer service messages
        public List<CustomerServiceMessagesModel> CustomerServiceMessages { get; set; }

        //fields required
        public List<PayoutFieldsModel> PayoutRequiredFields { get; set; }


        public SenderInfoModel SenderInfo { get; set; }
        public BeneficiaryInfoModel BeneficiaryInfo { get; set; }

        public ProviderInfoModel ProviderInfo { get; set; }


    }
}
