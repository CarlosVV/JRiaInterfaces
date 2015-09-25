using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CES.AddressVerification.Business.Contract.Interfaces;
using CES.AddressVerification.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.AddressVerification.Data
{
    public class AddressRepository: BaseGenericRepository, IAddressRepository
    {
        public AddressRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager, IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        }

        public async Task<IEnumerable<Address>> GetAddressList()
        {
            var request = new DatabaseRequest<Address>
            {
                ProcedureName = "",
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>(),
                Shaper = reader => GetAddress(reader)
            };

            return await Task.Run(() => GetList(request));
        }

        public async Task UpdateAddressList()
        {
            var request = new DatabaseRequest<Address>
            {
                ProcedureName = "",
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>(),
                Shaper = reader => GetAddress(reader)
            };

            return await Task.Run(() => GetList(request));
        }

        private static Address GetAddress(IDataReader reader)
        {
            return new Address
            {
                Address1 = reader.ReadValue<string>(""),
                State = reader.ReadValue<string>(""),
                City = reader.ReadValue<string>(""),
                ZipCode = reader.ReadValue<string>(""),
                Country = reader.ReadValue<string>("")
            };
        }
    }
}
