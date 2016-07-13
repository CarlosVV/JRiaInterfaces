using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IAddressVerificationDataProvider
    {
        ValidateAddressResponseModel Verify(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence);
        ValidateAddressResponseModel Verify(string address, DataProviderType dataProviderType, LevelOfConfidence acceptableConfidence);
    }
}