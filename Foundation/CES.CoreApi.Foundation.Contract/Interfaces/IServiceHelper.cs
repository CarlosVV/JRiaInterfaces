using System;
using System.Threading.Tasks;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IServiceHelper
    {
        TResult Execute<TService, TResult>(Func<TService, TResult> serviceDelegate)
            where TService : class;

        Task<TResult> ExecuteAsync<TService, TResult>(Func<TService, TResult> serviceDelegate)
            where TService : class;
    }
}