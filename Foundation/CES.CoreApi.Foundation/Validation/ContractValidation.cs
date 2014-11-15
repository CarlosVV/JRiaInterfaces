using System;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;

namespace CES.CoreApi.Foundation.Validation
{
    public class ContractValidation
    {
        public static void Requires(bool predicate, TechnicalSubSystem subsystem, SubSystemError subSystemError, params object[] parameters)
        {
            if (!predicate)
            {
                throw new CoreApiException(Organization.Ria, TechnicalSystem.CoreApi, subsystem, subSystemError, parameters);
            }
        }

        public static void Requires<TException>(bool predicate)
            where TException : Exception, new()
        {
            if (!predicate)
            {
                throw new TException();
            }
        }
    }
}
