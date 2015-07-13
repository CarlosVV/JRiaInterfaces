using System;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class SarValidationRequestModel
    {
        public SarValidationRequestBeneficiaryModel Beneficiary { get; set; }
        public SarValidationRequestAmountModel Amount { get; set; }
        public SarValidationRequestCorrespondentModel Correspondent { get; set; }
        public int ReceivingAgentId { get; set; }
        public int EnteredById { get; set; }
        public int CustomerId { get; set; }
        public int DeliveryMethod { get; set; }
        public int ReceivingAgentLocationId { get; set; }
        public string EntryMethod { get; set; }
        public int TransferReasonId { get; set; }
        public bool ShowRequiredFields { get; set; }
        public string UserLanguage { get; set; }
        public int BankCountryId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}