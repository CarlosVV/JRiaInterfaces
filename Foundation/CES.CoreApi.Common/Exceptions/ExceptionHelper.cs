using System.ComponentModel;
using System.Globalization;
using CES.CoreApi.Common.Attributes;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Tools;

namespace CES.CoreApi.Common.Exceptions
{
    public class ExceptionHelper : IExceptionHelper
    {
        private const string ErrorCodeTemplate = "{0}|{1}|{2}|{3}";

        public string GenerateExceptionCode(Organization organization, TechnicalSystem system,
            TechnicalSubSystem subSystem, SubSystemError error)
        {
            return string.Format(CultureInfo.InvariantCulture,
                ErrorCodeTemplate,
                organization.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                system.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                subSystem.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                error.GetAttributeValue<SubSystemErrorNumberAttribute, string>(x => x.ErrorNumber));
        }

        public string GenerateExceptionCode(TechnicalSubSystem subSystem, SubSystemError error)
        {
            return string.Format(CultureInfo.InvariantCulture,
                ErrorCodeTemplate,
                Organization.Ria.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                TechnicalSystem.CoreApi.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                subSystem.GetAttributeValue<DescriptionAttribute, string>(x => x.Description),
                error.GetAttributeValue<SubSystemErrorNumberAttribute, string>(x => x.ErrorNumber));
        }

        public string GenerateMessage(SubSystemError error, params object[] parameters)
        {
            return string.Format(CultureInfo.InvariantCulture,
                error.GetAttributeValue<ErrorMessageAttribute, string>(x => x.Message), parameters);
        }
    }
}