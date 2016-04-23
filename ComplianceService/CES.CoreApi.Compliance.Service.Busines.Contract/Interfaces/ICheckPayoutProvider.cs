using CES.CoreApi.Compliance.Service.Business.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Business.Contract.Interfaces
{
	public interface ICheckPayoutProvider
    {
        CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request);
    }
}
