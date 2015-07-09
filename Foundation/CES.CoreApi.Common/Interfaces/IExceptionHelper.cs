using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IExceptionHelper
    {
        string GenerateExceptionCode(Organization organization, TechnicalSystem system,
            TechnicalSubSystem subSystem, SubSystemError error);

        string GenerateExceptionCode(TechnicalSubSystem subSystem, SubSystemError error);
        string GenerateMessage(SubSystemError error, params object[] parameters);
    }
}