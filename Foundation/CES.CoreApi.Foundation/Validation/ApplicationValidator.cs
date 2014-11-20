using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;

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
        public bool Validate(IApplication application)
        {
            return application != null && application.IsActive;
        }

        #endregion
    }
}