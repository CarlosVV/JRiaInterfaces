using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IGeocodeServiceRequestProcessor
    {
        GeocodeAddressResponseModel GeocodeAddress(AddressModel address, LevelOfConfidence confidence);
        GeocodeAddressResponseModel GeocodeAddress(string formattedAddress, string country, LevelOfConfidence confidence);
        GeocodeAddressResponseModel ReverseGeocodePoint(LocationModel location, string country, LevelOfConfidence confidence);
    }
}
