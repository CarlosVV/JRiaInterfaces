using System;

namespace CES.CoreApi.Shared.Business.Contract.Models
{
    public class BirthInformationModel
    {
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string StateOfBirth { get; set; }
    }
}