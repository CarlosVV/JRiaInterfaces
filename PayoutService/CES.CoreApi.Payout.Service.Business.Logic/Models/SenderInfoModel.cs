using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class SenderInfoModel
    {
        public int CustomerInternalID { get; set; }
        public string CustomerExternalID { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? CityID { get; set; }
        public string District { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public int? StateID { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string CountryOfBirth { get; set; }
        public int? BirthStateID { get; set; }
        public int? BirthCityID { get; set; }
        public string BirthCity { get; set; }
        public string CountryOfResidence { get; set; }
        public string TaxID { get; set; }
        public bool DoesNotHaveATaxID { get; set; }
        public string Curp { get; set; }
        public string Occupation { get; set; }
        public int OccupationID { get; set; }
        public string IDType { get; set; }
        public int IDTypeID { get; set; }
        public string IDNumber { get; set; }
        public DateTime IDExpDate { get; set; }
        public DateTime IDIssuedDate { get; set; }
        public string IDIssuedByCountry { get; set; }
        public string IDIssuedByState { get; set; }
        public int IDIssuedByStateID { get; set; }
        public string IDIssuedBy { get; set; }
        public string IDSerialNumber { get; set; }

    }
}
