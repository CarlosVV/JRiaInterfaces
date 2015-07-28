namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Models
{
    public class ProcessingInformationModel
    {
        public int DatabaseId { get; set; }
        public int TerminalId { get; set; }
        public bool IsIndefinite { get; set; }
        public int DelayMinutes { get; set; }
        public int EnteredByLoginId { get; set; }
    }
}