using System;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IServiceHelper
    {
        TResult Execute<TService, TResult>(Func<TService, TResult> serviceDelegate)
            where TService : class;
    }
}