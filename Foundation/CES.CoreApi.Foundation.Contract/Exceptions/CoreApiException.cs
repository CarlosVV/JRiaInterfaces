using System;
using System.ComponentModel;
using System.Globalization;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Attributes;
using CES.CoreApi.Foundation.Contract.Enumerations;

namespace CES.CoreApi.Foundation.Contract.Exceptions
{
    [Serializable]
    public class CoreApiException: Exception
    {
        private const string ErrorCodeTemplate = "{0}|{1}|{2}|{3}";

        public CoreApiException(TechnicalSubSystem subSystem, SubSystemError subSystemError, Exception ex, params object[] parameters)
            : this(Organization.Ria, TechnicalSystem.CoreApi, subSystem, subSystemError, parameters)
        {
        }

        public CoreApiException(TechnicalSubSystem subSystem, SubSystemError subSystemError, params object[] parameters)
            : this(Organization.Ria, TechnicalSystem.CoreApi, subSystem, subSystemError, parameters)
        {
        }

        public CoreApiException(Organization organization, TechnicalSystem system, 
            TechnicalSubSystem subSystem, SubSystemError error, params object[] parameters)
        {
            ErrorCode = GetErrorCode(organization, system, subSystem, error);
            ClientMessage = string.Format(CultureInfo.InvariantCulture, error.GetAttributeValue<ErrorMessageAttribute, string>(x => x.Message), parameters);
            ErrorId = Guid.NewGuid();
            Organization = organization;
            System = system;
            SubSystem = subSystem;
            SubSystemError = error;
        }

        public CoreApiException(Exception exception)
        {
            Organization = Organization.Ria;
            System = TechnicalSystem.CoreApi;
            SubSystem = TechnicalSubSystem.Undefined;
            SubSystemError = SubSystemError.Undefined;
            ErrorCode = GetErrorCode(Organization, System, SubSystem, SubSystemError);
            ClientMessage = exception.Message;
            ErrorId = Guid.NewGuid();
        }

        public string ErrorCode { get; private set; }
        public string ClientMessage { get; private set; }
        public Guid ErrorId { get; private set; }
        public Organization Organization { get; private set; }
        public TechnicalSystem System { get; private set; }
        public TechnicalSubSystem SubSystem { get; private set; }
        public SubSystemError SubSystemError { get; private set; }

        private static string GetErrorCode(Organization organization, TechnicalSystem system,
            TechnicalSubSystem subSystem, SubSystemError error)
        {
            return string.Format(CultureInfo.InvariantCulture,
                ErrorCodeTemplate,
                organization.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                system.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                subSystem.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                error.GetAttributeValue<SubSystemErrorNumberAttribute, string>(x => x.ErrorNumber));
        }
    }
}