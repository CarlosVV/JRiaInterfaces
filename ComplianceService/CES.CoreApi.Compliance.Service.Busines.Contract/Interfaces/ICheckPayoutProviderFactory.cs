using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Compliance.Service.Business.Contract.Interfaces
{
	public interface ICheckPayoutProviderFactory
    {
        T GetInstance<T>(CheckPayoutProviderType providerType) where T : class;
    }
}
