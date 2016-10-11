using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Logic.Providers;

namespace CES.CoreApi.Payout.Service.Business.Logic.Factories
{
    public class PayoutProviderFactory : Dictionary<string, Func<BaseProviderProcess>>, IPayoutServiceProviderFactory
    {
        public T GetInstance<T>(string interfacePayoutName) where T : class
        {
            var name = interfacePayoutName;
         
            return this[name]() as T;
        }      
    }
}
