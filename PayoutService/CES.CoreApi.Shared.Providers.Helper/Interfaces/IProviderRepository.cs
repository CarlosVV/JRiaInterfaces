using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums;
namespace CES.CoreApi.Shared.Providers.Helper.Interfaces
{
    public interface IProviderRepository
    {
        Task<IEnumerable<ProviderModel>> GetProvidersByTypeAsync(ProviderType type);
        IEnumerable<ProviderModel> GetProvidersByType(ProviderType type);
    }
}
