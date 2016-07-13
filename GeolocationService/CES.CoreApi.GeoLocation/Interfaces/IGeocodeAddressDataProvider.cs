using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IGeocodeAddressDataProvider
    {
        GeocodeAddressResponseModel Geocode(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence);
        GeocodeAddressResponseModel Geocode(string address, DataProviderType providerType, LevelOfConfidence acceptableConfidence);
        GeocodeAddressResponseModel ReverseGeocode(LocationModel location, DataProviderType dataProviderType, LevelOfConfidence acceptableConfidence);
    }
}
