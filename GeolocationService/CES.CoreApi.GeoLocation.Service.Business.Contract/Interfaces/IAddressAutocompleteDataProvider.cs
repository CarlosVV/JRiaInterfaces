using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IAddressAutocompleteDataProvider
    {
        AutocompleteAddressResponseModel GetAddressHintList(AutocompleteAddressModel address, int maxRecords, DataProviderType providerType, LevelOfConfidence confidence);
    }
}