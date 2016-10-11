using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;

namespace CES.CoreApi.Shared.Providers.Helper.Interfaces
{
    public interface IProviderHelper
    {
        ProviderModel GetPayoutProvider(string pin);
    }
}
