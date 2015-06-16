using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Business.Contract.Enumerations;
using CES.CoreApi.Customer.Service.Business.Contract.Interfaces;
using CES.CoreApi.Customer.Service.Business.Contract.Models;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Customer.Service.Data
{
    public class CustomerRepository : BaseGenericRepository, ICustomerRepository
    {
        #region Core

        public CustomerRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager) 
            : base(cacheProvider, monitorFactory, identityManager, DatabaseType.ReadOnly)
        {
        }

        #endregion

        #region ICustomerRepository implementation

        public CustomerModel GetCustomer(int customerId)
        {
            var request = new DatabaseRequest<CustomerModel>
            {
                ProcedureName = "ol_sp_lttblCustomers_Get_20100513",
                IsCacheable = true,
                Parameters = new Collection<SqlParameter>().Add("@fCustomerID", customerId),
                Shaper = reader => GetCustomerDetails(reader)
            };

            return Get(request);
        }

        public DatabasePingModel Ping()
        {
            return PingDatabase();
        }

        #endregion

        #region Private methods

        private static CustomerModel GetCustomerDetails(IDataReader reader)
        {
            if (reader.Read())
            {
                return new CustomerModel
                {
                    Id = reader.ReadValue<int>("fNameID"),
                    CustomerId = reader.ReadValue<string>("fCustIDNumber"),
                    ExternalCustomerId = reader.ReadValue<string>("fExternalCustID"),
                    AgentId = reader.ReadValue<int>("fNameIDAgent"),
                    AgentLocationId = reader.ReadValue<int>("fRecAgentLocID"),
                    ReferredBy = reader.ReadValue<string>("fReferred"),
                    Note = reader.ReadValue<string>("fNote"),
                    IsOnHold = reader.ReadValue<bool>("fOnHold"),
                    IsDisabled = reader.ReadValue<bool>("fDisabled"),
                    DateCreated = reader.ReadValue<DateTime>("fDateCreated"),
                    LastUsed = reader.ReadValue<DateTime>("fLastUsed"),
                    StatusId = reader.ReadValue<int>("fStatusID"), //should be an enum
                    Address = GetAddressDetails(reader),
                    Name = GetNameDetails(reader),
                    Contact = GetContactDetails(reader)
                };
            }
            return null;
        }

        private static NameModel GetNameDetails(IDataReader reader)
        {
            return new NameModel
            {
                FirstName = reader.ReadValue<string>("fNameFirst"),
                MiddleName = reader.ReadValue<string>("fNameMid"),
                LastName1 = reader.ReadValue<string>("fNameLast1"),
                Name = reader.ReadValue<string>("fName"),
            };
        }

        private static AddressModel GetAddressDetails(IDataReader reader)
        {
            return new AddressModel
            {
                Address = reader.ReadValue<string>("fAddress"),
                City = reader.ReadValue<string>("fCity"),
                State = reader.ReadValue<string>("fState"),
                Country = reader.ReadValue<string>("fCountry"),
                PostalCode = reader.ReadValue<string>("fPostalCode"),
                UnitNumber = reader.ReadValue<string>("fUnitNumber"),
                Location = new LocationModel
                {
                    Latitude = reader.ReadValue<double>("fLatitude"),
                    Longitude = reader.ReadValue<double>("fLongitude"),
                },
                ValidationResult = reader.ReadValue<AddressValidationResult>("fAddressValidationStatusID")
            };
        }

        private static ContactModel GetContactDetails(IDataReader reader)
        {
            var model = new ContactModel
            {
                PhoneList = new Collection<TelephoneModel>(),
                Email = reader.ReadValue<string>("fEmailAddress", true),
                NoSms = reader.ReadValue<bool>("fCellNo_SMS_Receive")
            };

            AddPhone(reader, model.PhoneList, TelephoneKind.Home, "fTel_Number");
            AddPhone(reader, model.PhoneList, TelephoneKind.Fax, "fFaxNo");
            AddPhone(reader, model.PhoneList, TelephoneKind.Cell, "fCellNo");
            AddPhone(reader, model.PhoneList, TelephoneKind.Work, "fTelNo_Work");
            
            return model;
        }

        private static void AddPhone(IDataReader reader, ICollection<TelephoneModel> phoneList, TelephoneKind phoneType, string fieldName)
        {
            var phoneNumber = reader.ReadValue<string>(fieldName);
            if (string.IsNullOrEmpty(phoneNumber))
                return;

            var phone = new TelephoneModel
            {
                Number = phoneNumber,
                Type = phoneType
            };

            if (phoneType == TelephoneKind.Home)
            {
                phone.CountryCode = reader.ReadValue<string>("fTel_CountryCode");
                phone.AreaCode = reader.ReadValue<string>("fTel_AreaCode");
            }

            phoneList.Add(phone);

        }

        #endregion
    }
}
