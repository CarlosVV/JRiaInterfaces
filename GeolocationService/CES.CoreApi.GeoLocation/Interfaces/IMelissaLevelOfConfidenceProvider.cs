using CES.CoreApi.GeoLocation.Enumerations;


namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IMelissaLevelOfConfidenceProvider
    {
        /// <summary>
        /// Gets confidence level based on AV response verification code
        /// </summary>
        /// <param name="responseCodes"></param>
        /// <returns></returns>
        LevelOfConfidence GetLevelOfConfidence(string responseCodes);
    }
}