namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class ComplianceInformationModel
    {
        public string TransferReason { get; set; }
        public int TransferReasonId { get; set; }
        public string ComplianceLines { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool QuestionAskedOnBehalfOf { get; set; }
        public string SourceOfFunds { get; set; }
    }
}