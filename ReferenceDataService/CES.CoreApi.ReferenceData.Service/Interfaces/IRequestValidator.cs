using CES.CoreApi.ReferenceData.Service.Contract.Models;

namespace CES.CoreApi.ReferenceData.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(GetIdentificationTypeListRequest request);
        void Validate(GetIdentificationTypeRequest request);
    }
}
