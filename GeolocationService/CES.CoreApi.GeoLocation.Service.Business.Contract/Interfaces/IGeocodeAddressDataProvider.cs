using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IGeocodeAddressDataProvider
    {
        GeocodeAddressResponseModel Geocode(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence);
        GeocodeAddressResponseModel Geocode(string address, DataProviderType providerType, LevelOfConfidence acceptableConfidence);
        GeocodeAddressResponseModel ReverseGeocode(LocationModel location, DataProviderType dataProviderType, LevelOfConfidence acceptableConfidence);
    }
}
