using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Models
{
    public class ValidateAddressResponseModel
    {
        /// <summary>
        /// Address validation status.
        /// Returns true if address is found with requested MinimumConfidence level,
        /// otherwise false
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Address geo location coordinates
        /// </summary>
        public LocationModel Location { get; set; }

        /// <summary>
        /// A string specifying the confidence of the result.
        /// </summary>
        public LevelOfConfidence Confidence { get; set; }

        /// <summary>
        /// Specify data provider used to verify address
        /// </summary>
        public DataProviderType AddressValidationProvider { get; set; }

        /// <summary>
        /// Specify data provider used to geocode address
        /// </summary>
        public DataProviderType GeocodingProvider { get; set; }

        /// <summary>
        /// Address which was really verified by data provider
        /// </summary>
        public AddressModel Address { get; set; }
    }
}