using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IAddressServiceRequestProcessor
    {
        ValidateAddressResponseModel ValidateAddress(AddressModel address, LevelOfConfidence confidence);
        ValidateAddressResponseModel ValidateAddress(string formattedAddress, string country, LevelOfConfidence confidence);
        AutocompleteAddressResponseModel GetAutocompleteList(AutocompleteAddressModel address, int maxRecords, LevelOfConfidence confidence);
    }
}