using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class SarValidationResponseModel
    {
        public int IssueId { get; set; }

        public string Action { get; set; }

        public string CustomerMessage { get; set; }

        public bool ShowCustomerMessage { get; set; }

        public bool RequireSourceOfFunds { get; set; }

        public bool RequireIdLevel1 { get; set; }

        public bool RequireIdLevel2 { get; set; }

        public bool GotSourceOfFunds { get; set; }

        public bool GotIdLevel1 { get; set; }

        public bool GotIdLevel2 { get; set; }
        public SarResponseType ResponseType { get; set; }
        public string ExceptionCode { get; set; }
    }
}