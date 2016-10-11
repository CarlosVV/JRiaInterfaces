using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CES.CoreApi.Shared.Providers.Helper.Interfaces;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums;

namespace CES.CoreApi.Shared.Providers.Helper.Business
{
    public class ProviderHelper: IProviderHelper
    {
        #region Core
        private readonly IProviderRepository _repository;

        public ProviderHelper(IProviderRepository repository)
        {
            //if (repository == null)
            //    throw new CoreApiException(TechnicalSubSystem.Authentication,
            //        SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            _repository = repository;
        }

        #endregion

        #region Public methods
        public ProviderModel GetPayoutProvider(string pin)
        {
            return GetProviderByPin(ProviderType.Payout,  pin);
        }

   

        #endregion

        #region Private methods
        private ProviderModel GetProviderByPin(ProviderType type, string pin)
        {
            //Make DB call to get Providers:
            var reponses = _repository.GetProvidersByType(type);

            if (reponses == null)
            {
                //var exception = new CoreApiException(TechnicalSubSystem.CoreApiData,
                //    SubSystemError.Undefined, "CheckPayout");
                //throw exception;
                
                //TODO: Log and error or info here to note that there were no providers at all found?
            }

         
            //Check the input PIN to see if it matches one of the providers Pin formats.
            var pattern = string.Empty;
            Regex regex = null;
            foreach (var response in reponses)
            {
                pattern = response.GetConfiguration<string>(ConfigurationProviderKeys.PinRegExp);

                try
                {
                    regex = new Regex(pattern);
                    var match = regex.Match(pin);
                    if (match.Success)
                    {
                        return response;
                    }
                }
                catch
                {
                    continue;
                }
               
            }

            //For now default to Ria if none found - too many patterns are not set for it yet.
            //TODO: Fix this:
            foreach (var response in reponses)
            {
                if (response.Name == "Ria")
                {
                    return response;
                }
            }



            return null;
        }
        #endregion

    }

}
