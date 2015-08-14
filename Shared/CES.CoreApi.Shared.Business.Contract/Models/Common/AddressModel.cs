using CES.CoreApi.Shared.Business.Contract.Enumerations;

namespace CES.CoreApi.Shared.Business.Contract.Models.Common
{
    public class AddressModel
    {
        public string UnitNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public int? CityId { set; get; }
        public string State { get; set; }
        public int? StateId { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int? CountryId { get; set; }
        public GeolocationModel Geolocation { get; set; }
        public AddressValidationResult ValidationStatus { get; set; }
    }
}
