using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class CustomerValidationModel
    {
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string TaxCountry { get; set; }
        public bool IsOnHold { get; set; }
        public int CustomerId { get; set; }
        public CustomerValidationEntityType CustomerType { get; set; }
        public decimal CustomerAmount { get; set; }
        public decimal OrderIdAmount1Setting { get; set; }
        public decimal LocalAmountUsd { get; set; }
        public decimal GrandTotal { get; set; }
        public int CustomerIdType { get; set; }
        public string CustomerIdNumber { get; set; }
        public string ReceivingAgentCountry { get; set; }
        public string PayingAgentCountry { get; set; }
        public string ReceivingAgentState { get; set; }
        public int ImageId { get; set; }
        public bool RequireScannedId { get; set; }
        public bool IsIdentificationImageExempt { get; set; }
    }
}