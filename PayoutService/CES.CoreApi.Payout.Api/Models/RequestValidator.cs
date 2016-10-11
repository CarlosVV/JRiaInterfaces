using CES.CoreApi.Payout.Service.Contract.Interfaces;
using CES.CoreApi.Payout.Models;
using System;
using System.Data.SqlTypes;

namespace CES.CoreApi.Payout.Facade.Utilities
{

    /// <summary>
    /// Validate Requests
    /// </summary>
    public class RequestValidator : IRequestValidator
    {
        //NOTE!!: This Code is exactly duplicated in CES.CoreApi.Payout.Service.Utilities.RequestValidator
        //This means that all changes to one need to be made to both files.

        public GetTransactionInfoResponse Validate(GetTransactionInfoRequest request)
        {
            
                if (request == null) { return new GetTransactionInfoResponse(1001, "ValidateRequest: incoming request is null"); }
      
                //Value checks:
                if (request.RequesterInfo.AgentID < 1) { return new GetTransactionInfoResponse(1001, "missing AgentID."); }
                if (request.RequesterInfo.AgentLocID < 0) { return new GetTransactionInfoResponse(1002, "missing AgentLocID."); }
                if (!IsValidString(request.OrderPIN)) { return new GetTransactionInfoResponse(1003, "missing OrderPIN."); }
                if (request.RequesterInfo.UserID < 1) { return new GetTransactionInfoResponse(1004, "missing UserID."); }
                if (request.RequesterInfo.UserLoginID < 1) { return new GetTransactionInfoResponse(1005, "missing UserLoginID."); }
                if (request.CountryTo.Length < 1) { return new GetTransactionInfoResponse(1006, "missing CountryTo."); }

                //Make sure no values are null:
                request.CountryTo = request.CountryTo ?? "";
                request.OrderID = request.OrderID; //int so must have value
                request.OrderPIN = request.OrderPIN ?? "";
                request.PersistenceID = request.PersistenceID; //int so must have value
                request.RequesterInfo = ValidateRequesterInfo(request.RequesterInfo);
                request.StateTo = request.StateTo ?? "";

                //If everything succeeded, then return an empty response object.
                return new GetTransactionInfoResponse(0, string.Empty);
            
        }

        public PayoutTransactionResponse Validate(PayoutTransactionRequest request)
        {
            
                if (request == null) { return new PayoutTransactionResponse(2001, "ValidateRequest: incoming request is null."); }

                //Value checks:
                if (request.OrderPIN.Length < 1) { return new PayoutTransactionResponse(2002, "missing OrderPIN."); }
                if (request.RequesterInfo.AgentID < 1) { return new PayoutTransactionResponse(2003, "missing AgentID."); }
                if (request.RequesterInfo.AgentLocID < 0) { return new PayoutTransactionResponse(2004, "missing AgentLocID."); }
                if (request.RequesterInfo.UserID < 1) { return new PayoutTransactionResponse(2005, "missing UserID."); }
                if (request.RequesterInfo.UserLoginID< 1) { return new PayoutTransactionResponse(2006, "missing UserLoginID."); }
                if (request.Beneficiary == null) { return new PayoutTransactionResponse(2007, "ValidateRequest: Beneficiary data is missing."); }
                if (!IsValidString(request.Beneficiary.PhoneNumber)) { return new PayoutTransactionResponse(2008, "missing BeneficiaryPhoneNumber."); }
                if (!IsValidString(request.Beneficiary.Address)) { return new PayoutTransactionResponse(2009, "missing BeneficiaryAddress."); }
                if (!IsValidString(request.Beneficiary.City)) { return new PayoutTransactionResponse(2010, "missing BeneficiaryCity."); }
                if (!IsValidString(request.Beneficiary.Country)) { return new PayoutTransactionResponse(2011, "missing BeneficiaryCountry."); }
                if (!IsValidString(request.Beneficiary.Name)) { return new PayoutTransactionResponse(2012, "missing Beneficiary Full Name."); }
                if (!IsValidString(request.PayAgentState)) { return new PayoutTransactionResponse(2013, "missing PayAgentState."); }
                if (request.Sender == null) { return new PayoutTransactionResponse(2013, "ValidateRequest: Sender data is missing."); }
                if (!IsValidString(request.Sender.FirstName)) { return new PayoutTransactionResponse(2016, "missing Sender FirstName."); }
                if (!IsValidString(request.Sender.LastName1)) { return new PayoutTransactionResponse(2017, "missing Sender LastName1."); }
                if (!IsValidString(request.Beneficiary.FirstName)) { return new PayoutTransactionResponse(2016, "missing Beneficiary FirstName."); }
                if (!IsValidString(request.Beneficiary.LastName1)) { return new PayoutTransactionResponse(2017, "missing Beneficiary LastName1."); }

                //Make sure no values are null:
                DateTime minDate = (DateTime)SqlDateTime.MinValue;
                request.OrderPIN = request.OrderPIN ?? "";
                request.Beneficiary.Address = request.Beneficiary.Address ?? "";
                request.Beneficiary.BirthCity = request.Beneficiary.BirthCity ?? "";
                request.Beneficiary.BirthCityID = request.Beneficiary.BirthCityID; //int so must have value
                request.Beneficiary.BirthStateID = request.Beneficiary.BirthStateID; //int so must have value
                request.Beneficiary.City = request.Beneficiary.City ?? "";
                request.Beneficiary.CityID = request.Beneficiary.CityID; //int so must have value
                request.Beneficiary.Country = request.Beneficiary.Country ?? "";
                request.Beneficiary.CountryOfBirth = request.Beneficiary.CountryOfBirth ?? "";
                request.Beneficiary.CountryOfResidence = request.Beneficiary.CountryOfResidence ?? "";
                request.Beneficiary.County = request.Beneficiary.County ?? "";
                request.Beneficiary.Curp = request.Beneficiary.Curp ?? "";
                request.Beneficiary.CustRelationship = request.Beneficiary.CustRelationship ?? "";
                request.Beneficiary.CustRelationshipID = request.Beneficiary.CustRelationshipID; //int so must have value
                request.Beneficiary.DateOfBirth = (request.Beneficiary.DateOfBirth == null || request.Beneficiary.DateOfBirth < minDate) ? minDate: request.Beneficiary.DateOfBirth; 
                request.Beneficiary.District = request.Beneficiary.District ?? "";
                request.Beneficiary.DoesNotHaveATaxID = request.Beneficiary.DoesNotHaveATaxID; //bool so must have value
                request.Beneficiary.FirstName = request.Beneficiary.FirstName ?? "";
                request.Beneficiary.Gender = request.Beneficiary.Gender ?? "";
                request.Beneficiary.IDExpDate = (request.Beneficiary.IDExpDate == null || request.Beneficiary.IDExpDate < minDate) ? minDate : request.Beneficiary.IDExpDate;
                request.Beneficiary.IDIssuedBy = request.Beneficiary.IDIssuedBy ?? "";
                request.Beneficiary.IDIssuedByCountry = request.Beneficiary.IDIssuedByCountry ?? "";
                request.Beneficiary.IDIssuedByState = request.Beneficiary.IDIssuedByState ?? "";
                request.Beneficiary.IDIssuedByStateID = request.Beneficiary.IDIssuedByStateID; //int so must have value
                request.Beneficiary.IDIssuedDate = (request.Beneficiary.IDIssuedDate == null || request.Beneficiary.IDIssuedDate < minDate) ? minDate : request.Beneficiary.IDIssuedDate;
                request.Beneficiary.IDIssuer = request.Beneficiary.IDIssuer ?? "";
                request.Beneficiary.IDNumber = request.Beneficiary.IDNumber ?? "";
                request.Beneficiary.IDSerialNumber = request.Beneficiary.IDSerialNumber ?? "";
                request.Beneficiary.IDType = request.Beneficiary.IDType ?? "";
                request.Beneficiary.IDTypeID = request.Beneficiary.IDTypeID; //int so must have value
                request.Beneficiary.LastName1 = request.Beneficiary.LastName1 ?? "";
                request.Beneficiary.LastName2 = request.Beneficiary.LastName2 ?? "";
                request.Beneficiary.MiddleName = request.Beneficiary.MiddleName ?? "";
                request.Beneficiary.Name = request.Beneficiary.Name ?? "";
                request.Beneficiary.Nationality = request.Beneficiary.Nationality ?? "";
                request.Beneficiary.Occupation = request.Beneficiary.Occupation ?? "";
                request.Beneficiary.OccupationID = request.Beneficiary.OccupationID; //int so must have value
                request.Beneficiary.PhoneNumber = request.Beneficiary.PhoneNumber ?? "";
                request.Beneficiary.EmailAddress = request.Beneficiary.EmailAddress ?? "";
                request.Beneficiary.PostalCode = request.Beneficiary.PostalCode ?? "";
                request.Beneficiary.SourceOfFunds = request.Beneficiary.SourceOfFunds ?? "";
                request.Beneficiary.State = request.Beneficiary.State ?? "";
                request.Beneficiary.StateID = request.Beneficiary.StateID; //int so must have value
                request.Beneficiary.TaxAmount = request.Beneficiary.TaxAmount; //decimal so must have value
                request.Beneficiary.TaxCountry = request.Beneficiary.TaxCountry ?? "";
                request.Beneficiary.TaxID = request.Beneficiary.TaxID ?? "";
                request.Sender.CustomerInternalID = request.Sender.CustomerInternalID; //int so must have value
                request.Sender.CustomerExternalID = request.Sender.CustomerExternalID; //int so must have value
                request.Sender.Name = request.Sender.Name ?? "";               
                request.Sender.LastName2 = request.Sender.LastName2 ?? "";               
                request.Sender.MiddleName = request.Sender.MiddleName ?? "";
                request.Sender.Address = request.Sender.Address ?? "";
                request.Sender.City = request.Sender.City ?? "";
                request.Sender.CityID = request.Sender.CityID; //int so must have value
                request.Sender.District = request.Sender.District ?? "";
                request.Sender.County = request.Sender.County ?? "";
                request.Sender.State = request.Sender.State ?? "";
                request.Sender.StateID = request.Sender.StateID; //int so must have value
                request.Sender.Country = request.Sender.Country ?? "";
                request.Sender.PostalCode = request.Sender.PostalCode ?? "";
                request.Sender.PhoneNumber = request.Sender.PhoneNumber ?? "";
                request.Sender.EmailAddress = request.Sender.EmailAddress ?? "";
                request.Sender.Gender = request.Sender.Gender ?? "";
                request.Sender.DateOfBirth = (request.Sender.DateOfBirth == null || request.Sender.DateOfBirth < minDate) ? minDate : request.Sender.DateOfBirth;
                request.Sender.Nationality = request.Sender.Nationality ?? "";
                request.Sender.CountryOfBirth = request.Sender.CountryOfBirth ?? "";
                request.Sender.BirthStateID = request.Sender.BirthStateID; //int so must have value
                request.Sender.BirthCityID = request.Sender.BirthCityID; //int so must have value
                request.Sender.BirthCity = request.Sender.BirthCity ?? "";
                request.Sender.CountryOfResidence = request.Sender.CountryOfResidence ?? "";
                request.Sender.TaxID = request.Sender.TaxID ?? "";
                request.Sender.DoesNotHaveATaxID = request.Sender.DoesNotHaveATaxID; //bool so must have value
                request.Sender.Curp = request.Sender.Curp ?? "";
                request.Sender.Occupation = request.Sender.Occupation ?? "";
                request.Sender.OccupationID = request.Sender.OccupationID; //int so must have value
                request.Sender.IDType = request.Sender.IDType ?? "";
                request.Sender.IDTypeID = request.Sender.IDTypeID; //int so must have value
                request.Sender.IDNumber = request.Sender.IDNumber ?? "";
                request.Sender.IDExpDate = (request.Sender.IDExpDate == null || request.Sender.IDExpDate < minDate) ? minDate : request.Sender.IDExpDate;
                request.Sender.IDIssuedDate = (request.Sender.IDIssuedDate == null || request.Sender.IDIssuedDate < minDate) ? minDate : request.Sender.IDIssuedDate;
                request.Sender.IDIssuedByCountry = request.Sender.IDIssuedByCountry ?? "";
                request.Sender.IDIssuedByState = request.Sender.IDIssuedByState ?? "";
                request.Sender.IDIssuedByStateID = request.Sender.IDIssuedByStateID; //int so must have value
                request.Sender.IDIssuedBy = request.Sender.IDIssuedBy ?? "";
                request.RequesterInfo = ValidateRequesterInfo(request.RequesterInfo);

                if (!IsValidString(request.PayAgentState)) { return new PayoutTransactionResponse(2014, "missing PayAgentState."); }
                if (request.PersistenceID < 1) { return new PayoutTransactionResponse(2015, "missing PersistenceID."); }

                
                //If everything succeeded, then return an empty response object.
                return new PayoutTransactionResponse(0,string.Empty);
           
        }


        private RequesterInfo ValidateRequesterInfo(RequesterInfo ri)
        {
            DateTime minDate = (DateTime)SqlDateTime.MinValue;
            RequesterInfo validatedRequesterInfo = new RequesterInfo();
            validatedRequesterInfo.AgentID = ri.AgentID; //int so must have value
            validatedRequesterInfo.AgentLocID = ri.AgentLocID; //int so must have value
            validatedRequesterInfo.AppID = ri.AppID; //int so must have value
            validatedRequesterInfo.AppObjectID = ri.AppObjectID; //int so must have value
            validatedRequesterInfo.Locale = ri.Locale ?? "";
            validatedRequesterInfo.LocalTime = (ri.LocalTime == null || ri.LocalTime < minDate) ? minDate : ri.LocalTime;
            validatedRequesterInfo.TerminalID = ri.TerminalID ?? "";
            validatedRequesterInfo.TerminalUserID = ri.TerminalUserID ?? "";
            validatedRequesterInfo.Timezone = ri.Timezone ?? "";
            validatedRequesterInfo.TimezoneID = ri.TimezoneID; //int so must have value
            validatedRequesterInfo.UserLoginID = ri.UserLoginID; //int so must have value
            validatedRequesterInfo.UserID = ri.UserID;
            validatedRequesterInfo.UtcTime = (ri.UtcTime == null || ri.UtcTime < minDate) ? minDate : ri.UtcTime;
            validatedRequesterInfo.Version = ri.Version ?? "";
            return validatedRequesterInfo;
        }

        private static  bool IsValidString(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value.Length < 1) return false;
            return true;
        }

    }
}
