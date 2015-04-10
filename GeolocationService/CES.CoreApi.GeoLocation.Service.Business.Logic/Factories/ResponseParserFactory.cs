using System;
using System.Collections.Generic;
using System.Globalization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Factories
{
    public class ResponseParserFactory: Dictionary<string, Func<BaseDataResponseParser>>, IResponseParserFactory
    {
        #region Core

        private const string ResponseParserRegistrationNameTemplate = "I{0}ResponseParser";

        #endregion

        #region Public methods

        public T GetInstance<T>(DataProviderType providerType) where T : class
        {
            var name = GetRegistrationName(providerType);
            return this[name]() as T;
        }

        #endregion

        #region private methods

        private static string GetRegistrationName(DataProviderType providerType)
        {
            if (providerType == DataProviderType.Undefined)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralInvalidParameterValue,
                    "providerType", providerType);

            return string.Format(
                CultureInfo.InvariantCulture,
                ResponseParserRegistrationNameTemplate,
                providerType);
        }


        #endregion
    }
}
