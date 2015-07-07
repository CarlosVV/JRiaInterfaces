using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces
{
    public interface IValidationReadOnlyRepository
    {
        bool IsOfacWatchListMatched(OfacValidationRequestModel input);
        SarValidationResponseModel ValidateSar(SarValidationRequestModel input);
        bool IsBeneficiaryBlocked(int beneficiaryId, int correspondentId);
    }
}