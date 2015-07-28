using System.ComponentModel;

namespace CES.CoreApi.Common.Enumerations
{
    public enum TechnicalSubSystem
    {
        [Description("UNDEF")] Undefined = 0,
        [Description("CAPI")] CoreApi = 1,
        [Description("AUTH")] Authorization = 2,
        [Description("AUTHT")] Authentication = 3,
        [Description("CAPID")] CoreApiData = 4,
        [Description("GLCS")] GeoLocationService = 5,
        [Description("OVLDS")] OrderValidationService = 6,
        [Description("MTTRS")] MtTransactionService = 7,
        [Description("CUSTS")] CustomerService = 8,
        [Description("SETS")] SettingsService = 9,
        [Description("RDTS")] ReferenceDataService = 10,
        [Description("LVRFS")] LimitVerificationService = 11,
        [Description("AGNTS")] AgentService = 12,
        [Description("ACNTS")] AccountingService = 13,
    }
}