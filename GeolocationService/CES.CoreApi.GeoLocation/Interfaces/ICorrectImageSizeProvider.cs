using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface ICorrectImageSizeProvider
    {
        int GetCorrectImageSize(DataProviderType providerType, ImageDimension dimension, int requestValue);
    }
}