using CES.CoreApi.Compliance.Screening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Utilities
{
    public static class ViewModelUtil
    {
        public static bool ValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        public static bool ValidCallEventType(CallEventType value)
        {
            return value != CallEventType.Undefined;
        }

        public static bool ValidServiceIdType(ServiceIdType value)
        {
            return value != ServiceIdType.Undefined;
        }

        public static bool ValidSDeliveryMethod(DeliveryMethod value)
        {
            return value != DeliveryMethod.Undefined;
        }
        public static bool ValidPartyType(PartyType  value)
        {
            return value != PartyType.Undefined;
        }

        public static PartyType GetPartyTypeByRuntime(int runTime)
        {
            switch (runTime)
            {
                case 1:
                    return PartyType.Customer;
                case 2:
                    return PartyType.Beneficiary;
                default:
                    return PartyType.Undefined;
            }

        }
    }
}