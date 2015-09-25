using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.AddressVerification.Business.Contract.Models
{
    public class ValidationResult
    {
        public long Id { get; set; }

        public Confidence Confidence { get; set; }

        public bool IsValid { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
    }
}
