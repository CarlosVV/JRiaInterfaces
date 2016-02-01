using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;
namespace CES.CoreApi.Compliance.Service.Business.Contract.Interfaces
{
    public interface  ICheckPayoutServiceProvider
    {
        CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request, CheckPayoutProviderType providerType);
    }
}
