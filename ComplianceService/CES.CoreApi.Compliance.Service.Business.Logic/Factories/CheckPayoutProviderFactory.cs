using System.Collections.Generic;
using System.Globalization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Logic.Provider;
using System.Collections;
using System;

namespace CES.CoreApi.Compliance.Service.Business.Logic.Factories
{
    public class CheckPayoutProviderFactory : Dictionary<string, Func<BaseCheckProvider>>, ICheckPayoutProviderFactory
    {
        #region Core

        private const string CheckPayoutProviderRegistrationNameTemplate = "I{0}CheckPayoutProvider";

        public T GetInstance<T>(CheckPayoutProviderType providerType) where T : class
        {
            var name = GetRegistrationName(providerType);
            return this[name]() as T;
        }

        #endregion

        #region private methods

        private static string GetRegistrationName(CheckPayoutProviderType providerType)
        {
            if (providerType == CheckPayoutProviderType.Undefined)
                throw new CoreApiException(TechnicalSubSystem.ComplianceService,
                    SubSystemError.GeneralInvalidParameterValue,
                    "providerType", providerType);

            return string.Format(
                CultureInfo.InvariantCulture,
                CheckPayoutProviderRegistrationNameTemplate,
                providerType);
        }

      

        #endregion
    }
}
