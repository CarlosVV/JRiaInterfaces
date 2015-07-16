using System.Collections.Generic;

namespace CES.CoreApi.Shared.Business.Contract.Models
{
    public class ContactModel
    {
        public ICollection<TelephoneModel> PhoneList { get; set; }

        public string Email { get; set; }

        public bool? NoSms { get; set; }
    }
}
