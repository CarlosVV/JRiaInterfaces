using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Builders
{
    public class AddressQueryBuilder : IAddressQueryBuilder
    {
        #region Core

        private const string QueryWithAdministrativeAreaTemplate = "{0} {1} {2}";
        private const string QueryWithoutAdministrativeAreaTemplate = "{0} {1}";
        private const string CompositeAddressTemplate = "{0} {1}";

        #endregion

        #region Public methods

        public string Build(string address, string administrativeArea, string country)
        {
            if (string.IsNullOrEmpty(address))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "address");
            if (string.IsNullOrEmpty(country))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "country");
            
            var query = string.IsNullOrEmpty(administrativeArea)
                ? string.Format(CultureInfo.InvariantCulture, QueryWithoutAdministrativeAreaTemplate, address, country)
                : string.Format(CultureInfo.InvariantCulture, QueryWithAdministrativeAreaTemplate, address, administrativeArea, country);

            return HttpUtility.UrlPathEncode(query);
        }

        public string Build(string address1, string address2)
        {
            if (string.IsNullOrEmpty(address1))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "address1");

            var query = string.IsNullOrEmpty(address2)
                ? address1
                : string.Format(CultureInfo.InvariantCulture, CompositeAddressTemplate, address1, address2);

            return query.Trim();
        }

        #endregion
    }
}
