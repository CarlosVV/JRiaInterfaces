using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IMappingHelper
    {
        TOut ConvertTo<TIn, TOut>(TIn model);
        TOut ConvertToResponse<TIn, TOut>(TIn model);
    }
}