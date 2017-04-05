using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models.DTOs
{
    public class Subject
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Ocupation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string CountryOfBirth { get; set; }
        public IEnumerable<ID> IDs { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public int EconomicActivity { get; set; }
        public IEnumerable<Address> Address { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }       
    }

}