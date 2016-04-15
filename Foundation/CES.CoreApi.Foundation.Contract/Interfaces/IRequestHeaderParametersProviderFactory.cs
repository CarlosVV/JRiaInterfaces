using CES.CoreApi.Foundation.Contract.Enumerations;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IRequestHeaderParametersProviderFactory
	{
        T GetInstance<T>(string hostServiceType) where T : class;
    }
}