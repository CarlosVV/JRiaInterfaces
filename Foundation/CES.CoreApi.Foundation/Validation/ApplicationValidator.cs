using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Validation
{
    public class ApplicationValidator : IApplicationValidator
    {
        #region Public methods

        /// <summary>
        /// Validates application existence and active status
        /// </summary>
        /// <param name="application">Application instance to validate</param>
        /// <returns>True if application is existing and active,
        /// otherwise false</returns>
        public bool Validate(Application application)
        {
            return application != null && application.IsActive;
        }

        #endregion
    }
}