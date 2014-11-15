using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface ICorrectImageSizeProvider
    {
        int GetCorrectImageSize(DataProviderType providerType, ImageDimension dimension, int requestValue);
    }
}