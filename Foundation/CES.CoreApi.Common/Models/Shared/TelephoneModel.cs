using CES.CoreApi.Common.Enumerations.Shared;

namespace CES.CoreApi.Common.Models.Shared
{
    public class TelephoneModel
    {
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string Number { get; set; }
        public TelephoneKind Type { get; set; }
    }
}
