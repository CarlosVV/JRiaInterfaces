using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Foundation.Contract.Interfaces
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