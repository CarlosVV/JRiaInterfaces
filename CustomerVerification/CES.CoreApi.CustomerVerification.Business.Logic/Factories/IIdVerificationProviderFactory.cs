using CES.CoreApi.CustomerVerification.Business.Logic.Providers;

namespace CES.CoreApi.CustomerVerification.Business.Logic.Factories
{
    public interface IIdVerificationProviderFactory
    {
        BaseCustomerIdVerificationProvider GetInstance(string country);
    }
}