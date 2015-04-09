using System.Security.Principal;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ClientApplicationIdentity : IIdentity
    {
        #region Core

        private readonly Application _application;

        public ClientApplicationIdentity(Application application)
        {
            if (application == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "application");
            _application = application;
        }

        #endregion

        #region Public properties

        public int ApplicationId
        {
            get { return _application.Id; }
        }

        public string Name
        {
            get { return _application.Name; }
        }

        public string AuthenticationType
        {
            get { return "Database"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        #endregion
    }
}
