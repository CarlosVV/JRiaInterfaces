using CES.CoreApi.Payout.Service.Business.Contract.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class PayoutTransactionResponseModel
    {
        public PayoutTransactionResponseModel()
        {

            PayoutRequiredFields = new List<PayoutFieldsModel>();
            BeneficiaryFee = new MoneyModel();
            ConfirmationNumber = string.Empty;
            PersistenceID = -1;
            ResponseDateTimeUTC = DateTime.UtcNow;

        }

        public PayoutTransactionResponseModel(int errorCode, string errorMessage, int persistenceID)
        {
            ReturnInfo = new ReturnInfoModel() { ErrorCode = errorCode, ErrorMessage = errorMessage };
            PersistenceID = persistenceID;

        }

        public int ReviewIssuesStatus { get; set; }
        public int PersistenceID { get; set; }
        public string ConfirmationNumber { get; set; }
        public MoneyModel BeneficiaryFee { get; set; }

        //return  Info
        public ReturnInfoModel ReturnInfo { get; set; }

        //fields required
        public List<PayoutFieldsModel> PayoutRequiredFields { get; set; }

        public string GetInfoFields()
        {
            var infoFields = new StringBuilder();
            if (PayoutRequiredFields != null)
            {
                foreach (var field in PayoutRequiredFields)
                {
                    infoFields.Append("Field=" + field.DisplayName + ", ValidData=" + !field.DataInvalid + ", Note=" + field.NoteOnData + ", ErrorCode=" + field.DataErrorCode + "; ");
                }
            }

            return infoFields.ToString();
        }

        public bool IssuesFound { get; set; }  

        public string PIN { get; set; }
        public long OrderID { get; set; }

        public MoneyModel PayoutCommission { get; set; }
        public MoneyModel BeneficiaryPayout { get; set; }
        public DateTime ResponseDateTimeUTC { get; set; }
        public string OrderStatus { get; set; }
        public List<IssueModel> Issues { get; set; }
        public int ActionID { get; set; }
        public bool Commit { get; set; }

    }
}
