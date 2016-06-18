using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IGoogleLevelOfConfidenceProvider
    {
        /// <summary>
        /// Gets confidence level based on location type field in response
        /// </summary>
        /// <param name="locationType"></param>
        /// <returns></returns>
        LevelOfConfidence GetLevelOfConfidence(string locationType);
    }
}