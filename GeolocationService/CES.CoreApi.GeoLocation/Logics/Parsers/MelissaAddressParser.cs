using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Logic.Parsers
{
    public class MelissaAddressParser : IMelissaAddressParser
    {
        #region Public methods

        /// <summary>
        /// Parses XML address to AddressModel
        /// MelissaData does not populate country field for AutoComplete service call, that is why we need to pass it here.
        /// Also AutoComplete service response is not consistent with AddressVerification response, so we need to pass isAutocompleteService parameter
        /// </summary>
        /// <param name="addressElement"></param>
        /// <param name="xNamespace">Response namespace</param>
        /// <param name="country"></param>
        /// <param name="isAutocompleteService"></param>
        /// <returns></returns>
        public AddressModel ParseAddress(XElement addressElement, XNamespace xNamespace = null, string country = null, bool isAutocompleteService = false)
        {
            if (addressElement == null)
                return null;

            var responseNamespace = isAutocompleteService ? null : xNamespace;

            return new AddressModel
            {
                Address1 = addressElement.GetValue<string>(
                    isAutocompleteService
                        ? MelissaConstants.Address1
                        : MelissaConstants.AddressLine1, responseNamespace),
                City = addressElement.GetValue<string>(MelissaConstants.Locality, responseNamespace),
                AdministrativeArea = addressElement.GetValue<string>(MelissaConstants.AdministrativeArea, responseNamespace),
                Country = string.IsNullOrEmpty(country)
                    ? addressElement.GetValue<string>(MelissaConstants.Country, responseNamespace)
                    : country,
                PostalCode = addressElement.GetValue<string>(MelissaConstants.PostalCode, responseNamespace),

                FormattedAddress = addressElement.GetValue<string>(
                    isAutocompleteService
                        ? MelissaConstants.Address
                        : MelissaConstants.FormattedAddress, responseNamespace),
                UnitsOrApartments = PopulateUnitList(addressElement.GetValue<string>(MelissaConstants.SubBuilding)),
                UnitOrApartment = addressElement.GetValue<string>(MelissaConstants.SubPremises, responseNamespace)
            };
        }

        #endregion 

        #region Private methods

        /// <summary>
        /// Populates unit list for the address
        /// </summary>
        /// <param name="rawUnitList">Comma delimited list of units</param>
        /// <returns></returns>
        private static List<string> PopulateUnitList(string rawUnitList)
        {
            return string.IsNullOrEmpty(rawUnitList)
                ? null
                : rawUnitList.Split(',').ToList();
        }

        #endregion
    }
}