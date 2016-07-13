using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IAddressServiceRequestProcessor
    {
        ValidateAddressResponseModel ValidateAddress(AddressModel address, LevelOfConfidence confidence);
        ValidateAddressResponseModel ValidateAddress(string formattedAddress, string country, LevelOfConfidence confidence);
        AutocompleteAddressResponseModel GetAutocompleteList(AutocompleteAddressModel address, int maxRecords, LevelOfConfidence confidence);
    }
}