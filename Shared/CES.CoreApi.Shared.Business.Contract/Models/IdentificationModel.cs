using System;

namespace CES.CoreApi.Shared.Business.Contract.Models
{
    public class IdentificationModel
    {
        public string IdNumber { get; set; }
        public string IdType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssuedBy { get; set; }
        public string IssuedCountry { get; set; }
        public string IssuedState { get; set; }
        public string IdTaxCountry { get; set; }
        public bool IsChanged { get; set; }
        public bool IsPrimaryId { get; set; }
    }
}



//CustIdOccup = dbReader.GetString("fCustIDOccup"),

//Cust_PaymentMethod = dbReader.GetInt("fCust_PaymentMethod")


//----

//CustInfoChanged = dbReader.GetBool("fCustInfoChanged"),
//CustIdSSN = dbReader.GetString("fCustIDSSN"),
//TaxId = dbReader.GetString("fTaxID"),
//TaxCountry = dbReader.GetString("fTaxCountry"),

//Cust_Gender = dbReader.GetString("fCust_Gender"),
//Cust_PlaceOfBirth = dbReader.GetString("fCust_PlaceOfBirth"),
//Cust_CountryOfBirth = dbReader.GetString("fCust_CountryOfBirth"),
//Cust_StateOfBirth = dbReader.GetString("fCust_StateOfBirth"),
//CustNationality = dbReader.GetString("fCustNationality"),
//CustIdChanged = dbReader.GetBool("fCustIDChanged"),
//CustIdTaxCountry = dbReader.GetString("fCustIDTaxCountry"),
//CustIdDOB = dbReader.GetDateTime("fCustIDDOB"),

//First ID
//CustIdType = dbReader.GetString("fCustIDType"),
//CustIdNo = dbReader.GetString("fCustIDNo"),
//CustIdBy = dbReader.GetString("fCustIDBy"),
//CustIdExp = dbReader.GetDateTime("fCustIDExp"),
//IdIssuedDate = dbReader.GetDateTime("fIDIssuedDate"),
//CustIdIssuedByState = dbReader.GetString("fCustIDIssuedByState"),
//CustIdIssuedByCountry = dbReader.GetString("fCustIDIssuedByCountry"),

//Second ID
//CustId2Type = dbReader.GetString("fCustID2Type"),
//CustId2No = dbReader.GetString("fCustID2No"),
//CustId2By = dbReader.GetString("fCustID2By"),
//CustId2Exp = dbReader.GetDateTime("fCustID2Exp"),
//CustId2IssuedByState = dbReader.GetString("fCustID2IssuedByState"),
//CustId2IssuedByCountry = dbReader.GetString("fCustID2IssuedByCountry"),
//Id2IssuedDate = dbReader.GetDateTime("fID2IssuedDate"),

//Questions
//IdCreatedById = dbReader.GetInt("fIDCreatedByID"),
//IdCreatedDate = dbReader.GetDateTime("fIDCreatedDate"),
//CustIDOnBehalfOf = dbReader.GetInt("fCustIDOnBehalfOf"),
//IssueId = dbReader.GetInt("fIssueID"),
//RoutingCode = dbReader.GetString("fRoutingCode"),
//RoutingType = dbReader.GetInt("fRoutingType"),

//Duplicated and removed
//CustCellNo = dbReader.GetString("fCustCellNo"),