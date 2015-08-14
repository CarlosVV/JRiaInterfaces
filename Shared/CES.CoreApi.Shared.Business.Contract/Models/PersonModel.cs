using System.Collections.Generic;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models.Common;

namespace CES.CoreApi.Shared.Business.Contract.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public NameModel Name { get; set; }
        public AddressModel Address { get; set; }
        public ContactModel Contact { get; set; }
        public BirthInformationModel BirthInformation { get; set; }
        public TaxInformationModel TaxInformation { get; set; }
        public string Nationality { get; set; }
        public GenderEnum Gender { get; set; }
        public ICollection<IdentificationModel> Identification { get; set; }
        public bool? IsOnHold { get; set; }
        public bool? IsDisabled { get; set; }
        public string Note { get; set; }
    }
}