using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Enumerations;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Data.Repositories
{
    public class TransactionRepository : BaseGenericRepository, ITransactionRepository
    {
        #region Core

        public TransactionRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        } 

        #endregion

        #region ITransactionRepository implementation

        public TransactionDetailsModel GetDetails(int orderId, int databaseId = 0)
        {
            var request = new DatabaseRequest<TransactionDetailsModel>
            {
                ProcedureName = "ol_sp_oltblOrdersToPost_GetByOrderID",
                IsCacheable = true,
                DatabaseType = databaseId != 0
                    ? DatabaseType.FrontEnd
                    : DatabaseType.Main,
                DatabaseId = databaseId,
                Parameters = new Collection<SqlParameter>()
                    .Add("@orderId", orderId),
                Shaper = reader => GetTransactionDetails(reader)
            };

            return Get(request);
        }
        
        #endregion

        #region Private methods

        private TransactionDetailsModel GetTransactionDetails(IDataReader reader)
        {
            return new TransactionDetailsModel
            {
                Id = reader.ReadValue<int>("fOrderID"),
                TransactionDate = reader.ReadValue<DateTime>("fDate"),
                TransactionNumber = reader.ReadValue<string>("fOrderNo"),
                Status = GetStatus(reader),
                Customer = GetCustomerDetails(reader)
            };
        }

        private CustomerModel GetCustomerDetails(IDataReader reader)
        {
            return new CustomerModel
            {
                IsChanged = reader.ReadValue<bool>("fCustInfoChanged"),
                Id = reader.ReadValue<int>("fNameIDCustomer"),
                Name = GetNameDetails(reader),
                Address = GetAddressDetails(reader),
                Contact = GetContactDetails(reader)
            };
        }

        private static ContactModel GetContactDetails(IDataReader reader)
        {
            return new ContactModel
            {
                PhoneList = new Collection<TelephoneModel>
                {
                    new TelephoneModel
                    {
                        Number = reader.ReadValue<string>("CustomerTelNo"),
                        PhoneType = TelephoneType.Home
                    },
                    new TelephoneModel
                    {
                        Number = reader.ReadValue<string>("fCustCellNo"),
                        PhoneType = TelephoneType.Cell
                    }
                }
            };
        }

        private static NameModel GetNameDetails(IDataReader reader)
        {
            return new NameModel
            {
                FirstName = reader.ReadValue<string>("CustomerNameFirst"),
                LastName1 = reader.ReadValue<string>("CustomerNameLast1"),
                LastName2 = reader.ReadValue<string>("CustomerNameLast2"),
                FullName = reader.ReadValue<string>("CustomerName")
            };
        }

        private static AddressModel GetAddressDetails(IDataReader reader)
        {
            return new AddressModel
            {
                Address1 = reader.ReadValue<string>("fAddress"),
                City = reader.ReadValue<string>("fCity"),
                State = reader.ReadValue<string>("fState"),
                PostalCode = reader.ReadValue<string>("fPostalCode"),
                Country = reader.ReadValue<string>("fCountry")
            };
        }

        private static TransactionStatusEnum GetStatus(IDataReader reader)
        {
            if (reader.ReadValue<int>("fCanceledBy") > 0)
                return TransactionStatusEnum.Canceled;

            return reader.ReadValue<bool>("fPosted")
                ? TransactionStatusEnum.Posted
                : TransactionStatusEnum.Pending;
        }

        #endregion
    }
}
