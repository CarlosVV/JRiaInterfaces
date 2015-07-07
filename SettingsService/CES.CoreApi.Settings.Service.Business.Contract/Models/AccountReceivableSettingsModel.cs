using System;
using CES.CoreApi.Settings.Service.Business.Contract.Attributes;
using CES.CoreApi.Settings.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Settings.Service.Business.Contract.Models
{
    public class AccountReceivableSettingsModel
    {
        private const int LimitDepositTimeHourPart = 14;

        public AccountReceivableSettingsModel()
        {
            LimitDisplayOption = AccountReceivableLimitDisplayOptionType.Nothing;
            LimitDepositTime = new DateTime(1, 1, 1, LimitDepositTimeHourPart, 0, 0);
        }

        [CountrySetting("ARLimitDisplayOption")]
        public AccountReceivableLimitDisplayOptionType LimitDisplayOption { get; set; }

        [CountrySetting("ARLimitDepositTime")]
        public DateTime LimitDepositTime { get; set; }

        [CountrySetting("ARFaxNo")]
        public string FaxNo { get; set; }
    }
}
