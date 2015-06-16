using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class OfacValidationRequestModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string MiddleName { get; set; }
        public CustomerValidationEntityType EntityType { get; set; }
    }
}