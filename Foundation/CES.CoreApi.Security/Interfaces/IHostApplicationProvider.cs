using System.Threading.Tasks;

using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.Foundation.Security.Interfaces
{
    public interface IHostApplicationProvider
    {
        Task<IApplication> GetApplication();
    }
}