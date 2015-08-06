using System.Threading.Tasks;
using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IHostApplicationProvider
    {
        Task<IApplication> GetApplication();
    }
}