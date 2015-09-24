using System.Collections.Generic;
using CES.AddressVerification.Business.Contract.Models;

namespace CES.AddressVerification.Business.Contract.Interfaces
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAddressList();
    }
}