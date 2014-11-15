using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Models
{
    public class GetMapResponseModel
    {
        public byte[] MapData { get; set; }

        /// <summary>
        /// Address geocode status.
        /// Returns true if map was found,
        /// otherwise false
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Specify data provider used to get map
        /// </summary>
        public DataProviderType DataProvider { get; set; }
    }
}
