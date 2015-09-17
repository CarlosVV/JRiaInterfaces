using System;

namespace CES.CoreApi.CustomerVerification.Business.Contract.Models
{
    public class VerifyCustomerIdentityRequestModel
    {
        public CustomerNameModel Name { get; set; }

        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CurrentAddress { get; set; }
    }
}
