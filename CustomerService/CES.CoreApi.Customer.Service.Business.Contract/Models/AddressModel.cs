using CES.CoreApi.Customer.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Customer.Service.Business.Contract.Models
{
    public class AddressModel
    {
        public string UnitNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public LocationModel Location { get; set; }
        public AddressValidationResult ValidationResult { get; set; }
    }
}