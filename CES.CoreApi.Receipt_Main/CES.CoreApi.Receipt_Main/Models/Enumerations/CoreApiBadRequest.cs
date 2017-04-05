using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models.Enumerations
{
    public enum CoreApiBadRequest
    {
        PersistenceID = 4100,
        TellerDrawerInstanceID = 4101,
        OrderPIN = 4102,
        PayoutCurrency = 4103,
        PayoutAmount = 4104,
        PayoutMethodID = 4105,
        RequesterInfoAppID = 4001,
        RequesterInfoAppObjectID = 4002,
        RequesterInfoAgentID = 4003,
        RequesterInfoAgentLocID = 4004,
        RequesterInfoLocalTime = 4005,
        RequesterInfoAgentCountry = 4006,
        RequesterInfoAgentState = 4007,
        BeneGender = 4020,
        BeneName = 4021,
        BeneFirstName = 4022,
        BeneLastName1 = 4023,
        BeneCountry = 4024

    }
}