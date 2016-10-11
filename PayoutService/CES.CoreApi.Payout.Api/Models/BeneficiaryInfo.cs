using System;
using CES.CoreApi.Payout.Service.Contract.Constants;
using System.Runtime.Serialization;


namespace CES.CoreApi.Payout.Models
{
    [DataContract(Namespace = Namespaces.PayoutServiceDataContractNamespace)]
    public class BeneficiaryInfo
    {

        [DataMember(EmitDefaultValue = false)]
        public int BenInternalID { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string BenExternalID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string SourceOfFunds { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Gender { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TaxCountry { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string LastName1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string LastName2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IDType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int IDTypeID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IDNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IDSerialNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime IDExpDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime IDIssuedDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IDIssuedByCountry { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IDIssuedByState { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int IDIssuedByStateID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IDIssuedBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IDIssuer { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmailAddress { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Address { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string City { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int? CityID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Country { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string District { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string County { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string State { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? StateID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PostalCode { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public DateTime DateOfBirth { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Nationality { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CountryOfBirth { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? BirthStateID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? BirthCityID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string BirthCity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CountryOfResidence { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TaxID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Occupation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int OccupationID { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string Curp { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal TaxAmount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool DoesNotHaveATaxID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CustRelationshipID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CustRelationship { get; set; }


    }
}
