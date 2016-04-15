using System.Threading.Tasks;


namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IHostApplicationProvider
    {
        Task<IApplication> GetApplication();
    }
}