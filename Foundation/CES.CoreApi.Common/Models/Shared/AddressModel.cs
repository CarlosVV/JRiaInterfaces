using CES.CoreApi.Common.Enumerations.Shared;

namespace CES.CoreApi.Common.Models.Shared
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
