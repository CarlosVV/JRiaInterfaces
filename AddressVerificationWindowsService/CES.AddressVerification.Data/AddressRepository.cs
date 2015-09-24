using System.Collections;
using System.Collections.Generic;
using CES.AddressVerification.Business.Contract.Interfaces;
using CES.AddressVerification.Business.Contract.Models;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.AddressVerification.Data
{
    public class AddressRepository: BaseGenericRepository, IAddressRepository
    {
        public AddressRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager, IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        }

        public IEnumerable<Address> GetAddressList()
        {
            return null;
        }
    }
}
