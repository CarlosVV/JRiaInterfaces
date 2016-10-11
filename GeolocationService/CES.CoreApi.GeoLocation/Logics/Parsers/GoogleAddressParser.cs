using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Logic.Parsers
{
    public class GoogleAddressParser : IGoogleAddressParser
    {
        #region

        private const string Address1Template = "{0} {1}";

        #endregion

        #region Public methods

        public AddressModel ParseAddress(XElement address)
        {
            if (address == null)
                return null;

            var addressComponents = address.Elements(GoogleConstants.AddressComponent)
                .Select(component => component)
                .ToList();
            
            return new AddressModel
            {
                Address1 = string.Format(CultureInfo.InvariantCulture,
                    Address1Template,
                    GetAddressComponent(GoogleConstants.StreetNumber, addressComponents),
                    GetAddressComponent(GoogleConstants.Street, addressComponents)),
                City = GetAddressComponent(GoogleConstants.City, addressComponents),
                AdministrativeArea = GetAddressComponent(GoogleConstants.AdministrativeArea, addressComponents),
                Country = GetAddressComponent(GoogleConstants.Country, addressComponents),
                PostalCode = GetAddressComponent(GoogleConstants.PostalCode, addressComponents),
                FormattedAddress = address.GetValue<string>(GoogleConstants.FormattedAddress),
                UnitOrApartment = GetAddressComponent(GoogleConstants.SubPremise, addressComponents)
            };
        }

        #endregion

        #region private methods

        private static string GetAddressComponent(string componentType, IEnumerable<XElement> addressComponents)
        {
            return (from component in addressComponents
                    let type = component.GetValue<string>(GoogleConstants.Type)
                    where type.Equals(componentType, StringComparison.OrdinalIgnoreCase)
                    select component.GetValue<string>(GoogleConstants.ShortName))
                .FirstOrDefault();
        }

        #endregion
    }
}
