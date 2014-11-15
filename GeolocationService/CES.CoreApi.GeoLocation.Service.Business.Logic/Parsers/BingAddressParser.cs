using System.Xml.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers
{
    public class BingAddressParser : IBingAddressParser
    {
        public AddressModel ParseAddress(XElement addressElement, XNamespace xNamespace)
        {
            if (addressElement == null)
                return null;

            return new AddressModel
            {
                Address1 = addressElement.GetValue<string>(BingConstants.AddressLine, xNamespace),
                City = addressElement.GetValue<string>(BingConstants.Locality, xNamespace),
                AdministrativeArea = addressElement.GetValue<string>(BingConstants.AdminDistrict, xNamespace),
                Country = addressElement.GetValue<string>(BingConstants.CountryRegion, xNamespace),
                PostalCode = addressElement.GetValue<string>(BingConstants.PostalCode, xNamespace),
                FormattedAddress = addressElement.GetValue<string>(BingConstants.FormattedAddress, xNamespace)
            };
        }
    }
}
