﻿using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Business.Contract.Models;

namespace CES.CoreApi.Customer.Service.Business.Contract.Interfaces
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomer(int customerId);
       
        DatabaseType DatabaseType { get; }
        DatabasePingModel Ping();
    }
}