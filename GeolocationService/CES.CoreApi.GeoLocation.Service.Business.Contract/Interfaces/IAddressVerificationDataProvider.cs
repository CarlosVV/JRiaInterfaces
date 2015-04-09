using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IAddressVerificationDataProvider
    {
        ValidateAddressResponseModel Verify(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence);
        ValidateAddressResponseModel Verify(string address, DataProviderType dataProviderType, LevelOfConfidence acceptableConfidence);
    }
}