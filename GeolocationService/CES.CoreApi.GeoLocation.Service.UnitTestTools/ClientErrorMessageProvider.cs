using System.Globalization;
using CES.CoreApi.Common.Attributes;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.UnitTestTools
{
    public class ClientErrorMessageProvider
    {
        public static string GetMessage(SubSystemError subSystemError, params object[] parameters)
        {
            return string.Format(CultureInfo.InvariantCulture,
                subSystemError.GetAttributeValue<ErrorMessageAttribute, string>(x => x.Message),
                parameters);
        }
    }
}
