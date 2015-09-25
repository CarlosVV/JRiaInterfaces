using System.Collections.Generic;
using System.Threading.Tasks;
using CES.AddressVerification.Business.Contract.Models;

namespace CES.AddressVerification.Business.Contract.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressList();
    }
}