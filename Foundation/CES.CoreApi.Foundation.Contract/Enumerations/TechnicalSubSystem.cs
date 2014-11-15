using System.ComponentModel;

namespace CES.CoreApi.Foundation.Contract.Enumerations
{
    public enum TechnicalSubSystem
    {
        [Description(null)] Undefined = 0,
        [Description("CAPI")] CoreApi = 1,
        [Description("AUTH")] Authorization = 2,
        [Description("AUTHT")] Authentication = 3,
        [Description("CAPID")] CoreApiData = 4,
        [Description("GLCS")] GeoLocationService = 5
    }
}
