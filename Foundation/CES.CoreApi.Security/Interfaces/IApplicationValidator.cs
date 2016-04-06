

using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.Foundation.Security.Interfaces
{
    public interface IApplicationValidator
    {
        /// <summary>
        /// Validates application existence and active status
        /// </summary>
        /// <param name="application">Application instance to validate</param>
        /// <returns>True if application is existing and active,
        /// otherwise false</returns>
        bool Validate(IApplication application);
    }
}