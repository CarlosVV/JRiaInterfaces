using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IAddressServiceRequestProcessor
    {
        ValidateAddressResponseModel ValidateAddress(AddressModel address, LevelOfConfidence confidence);
        ValidateAddressResponseModel ValidateAddress(string formattedAddress, string country, LevelOfConfidence confidence);
        AutocompleteAddressResponseModel GetAutocompleteList(string country, string administrativeArea, string address, int maxRecords);
    }
}