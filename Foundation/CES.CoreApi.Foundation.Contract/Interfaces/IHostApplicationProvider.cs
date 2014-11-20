using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IHostApplicationProvider
    {
        IApplication GetApplication();
    }
}