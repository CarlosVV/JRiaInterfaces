using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models
{
    public class Party
    {

        public int Id { get; set; }

        public int IdOnFile { get; set; }

        public string Number { get; set; }

        public PartyType Type { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName1 { get; set; }

        public string LastName2 { get; set; }

        public string Gender { get;set; }

        public string Ocupation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public string CountryOfBirth { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string TelephoneNumber { get; set; }

        public IEnumerable<ID> IDs { get; set; }
    }
}