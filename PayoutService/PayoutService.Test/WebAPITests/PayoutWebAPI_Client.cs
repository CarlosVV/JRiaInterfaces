using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CES.CoreApi.Payout.Models;
using System.Threading.Tasks;

namespace PayoutService.Test.WebAPITests
{
    public class PayoutWebAPI_Client
    {
        private const string PAYOUT_SVC_URI_BASE = "http://localhost:55555";
        private const string PAYOUT_SVC_URI_GETTRANS = "moneytransfer/payout/getTransactionInfo";
        private const string PAYOUT_SVC_URI_PAYTRANS = "moneytransfer/payout/payoutTransaction";

        private static GetTransactionInfoResponse m_gtiResp = new GetTransactionInfoResponse(0,string.Empty);
        private static PayoutTransactionResponse m_ptResp = new PayoutTransactionResponse(0, string.Empty);


        public static GetTransactionInfoResponse GetTransactionInfoRequest(GetTransactionInfoRequest incomingRequest)
        {
            RunAsyncGetTransInfo(incomingRequest).Wait();
            return m_gtiResp;
        }

        private static async Task RunAsyncGetTransInfo(GetTransactionInfoRequest incomingRequest)
        {
            ReturnInfo ri = new ReturnInfo();
            m_gtiResp.ReturnInfo = ri;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PAYOUT_SVC_URI_BASE);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("ApplicationId", "501");
                    // HTTP POST

                    var request = new GetTransactionInfoRequest()
                    {
                        PersistenceID = incomingRequest.PersistenceID,
                        RequesterInfo = new RequesterInfo
                        {
                            AppID = incomingRequest.RequesterInfo.AppID,
                            AppObjectID = incomingRequest.RequesterInfo.AppObjectID,
                            AgentID = incomingRequest.RequesterInfo.AgentID,
                            AgentLocID = incomingRequest.RequesterInfo.AgentLocID,
                            UserID = incomingRequest.RequesterInfo.UserID,
                            UserLoginID = incomingRequest.RequesterInfo.UserLoginID,
                            TerminalID = incomingRequest.RequesterInfo.TerminalID,
                            TerminalUserID = incomingRequest.RequesterInfo.TerminalUserID,
                            LocalTime = incomingRequest.RequesterInfo.LocalTime,
                            UtcTime = incomingRequest.RequesterInfo.UtcTime,
                            Timezone = incomingRequest.RequesterInfo.Timezone,
                            TimezoneID = incomingRequest.RequesterInfo.TimezoneID,
                            Locale = incomingRequest.RequesterInfo.Locale,
                            Version = incomingRequest.RequesterInfo.Version,
                            AgentCountry = incomingRequest.RequesterInfo.AgentCountry,
                            AgentState = incomingRequest.RequesterInfo.AgentState
                        },
                        OrderPIN = incomingRequest.OrderPIN,
                        OrderID = incomingRequest.OrderID,
                        CountryTo = incomingRequest.CountryTo,
                        StateTo = incomingRequest.StateTo
                    };

                    var respMsg = await client.PostAsJsonAsync(PAYOUT_SVC_URI_GETTRANS, request);
                    if (respMsg.IsSuccessStatusCode)
                    {
                        var result = respMsg.Content.ReadAsStringAsync().Result;
                        try
                        {
                            //If the response was successful it will be in json and can be de-serialized
                            m_gtiResp = JsonConvert.DeserializeObject<GetTransactionInfoResponse>(result);
                        }
                        catch(Exception)
                        {
                            //If the response was an error code/message it may not have any other json, so get the error text.
                            m_gtiResp.ReturnInfo.ErrorCode = 919;
                            m_gtiResp.ReturnInfo.ErrorMessage = result;
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_gtiResp.ReturnInfo.AvailableForPayout = false;
                m_gtiResp.ReturnInfo.ErrorCode = 909;
                m_gtiResp.ReturnInfo.ErrorMessage = "ERROR: Could not get response from payout service: " + e.Message;
                return;
            }
        }
    
        public static GetTransactionInfoResponse NullGetTransactionInfoRequest(GetTransactionInfoRequest incomingRequest)
        {
            RunAsyncNullGetTransInfo(incomingRequest).Wait();
            return m_gtiResp;
        }

        private static async Task RunAsyncNullGetTransInfo(GetTransactionInfoRequest incomingRequest)
        {
            ReturnInfo ri = new ReturnInfo();
            m_gtiResp.ReturnInfo = ri;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PAYOUT_SVC_URI_BASE);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("ApplicationId", "501");
                    // HTTP POST

                    GetTransactionInfoRequest request = null;

                    var respMsg = await client.PostAsJsonAsync(PAYOUT_SVC_URI_GETTRANS, request);
                    if (respMsg.IsSuccessStatusCode)
                    {
                        var result = respMsg.Content.ReadAsStringAsync().Result;
                        try
                        {
                            //If the response was successful it will be in json and can be de-serialized
                            m_gtiResp = JsonConvert.DeserializeObject<GetTransactionInfoResponse>(result);
                        }
                        catch (Exception)
                        {
                            //If the response was an error code/message it may not have any other json, so get the error text.
                            m_gtiResp.ReturnInfo.ErrorCode = 919;
                            m_gtiResp.ReturnInfo.ErrorMessage = result;
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_gtiResp.ReturnInfo.AvailableForPayout = false;
                m_gtiResp.ReturnInfo.ErrorCode = 909;
                m_gtiResp.ReturnInfo.ErrorMessage = "ERROR: Could not get response from payout service: " + e.Message;
                return;
            }
        }


        public static PayoutTransactionResponse PayoutTransactionRequest(PayoutTransactionRequest incomingRequest)
        {
            RunAsyncPayTrans(incomingRequest).Wait();
            return m_ptResp;
        }

        private static async Task RunAsyncPayTrans(PayoutTransactionRequest incomingRequest)
        {
            //ReturnInfo ri = new ReturnInfo();
            //m_ptResp.ReturnInfo = ri;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(PAYOUT_SVC_URI_BASE);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Add("ApplicationId", "501");

                    // HTTP POST
                    //var request = new PayoutTransactionRequest();
                    //request.PersistenceID = incomingRequest.PersistenceID;
                    //RequesterInfo ri = new RequesterInfo();
                    //ri.AppID = incomingRequest.RequesterInfo.AppID;
                    //ri.AppObjectID = incomingRequest.RequesterInfo.AppObjectID;
                    //ri.AgentID = incomingRequest.RequesterInfo.AgentID;
                    //ri.AgentLocID = incomingRequest.RequesterInfo.AgentLocID;
                    //ri.UserID = incomingRequest.RequesterInfo.UserID;
                    //ri.UserLoginID = incomingRequest.RequesterInfo.UserLoginID;
                    //ri.TerminalID = incomingRequest.RequesterInfo.TerminalID;
                    //ri.TerminalUserID = incomingRequest.RequesterInfo.TerminalUserID;
                    //ri.LocalTime = incomingRequest.RequesterInfo.LocalTime;
                    //ri.UtcTime = incomingRequest.RequesterInfo.UtcTime;
                    //ri.Timezone = incomingRequest.RequesterInfo.Timezone;
                    //ri.TimezoneID = incomingRequest.RequesterInfo.TimezoneID;
                    //ri.Locale = incomingRequest.RequesterInfo.Locale;
                    //ri.Version = incomingRequest.RequesterInfo.Version;
                    //ri.AgentCountry = incomingRequest.RequesterInfo.AgentCountry;
                    //ri.AgentState = incomingRequest.RequesterInfo.AgentState;
                    //request.RequesterInfo = ri;
                    //BeneficiaryInfo b = new BeneficiaryInfo();
                    //b.BenInternalID = incomingRequest.Beneficiary.BenInternalID;
                    //b.BenExternalID = incomingRequest.Beneficiary.BenExternalID;
                    //b.SourceOfFunds = incomingRequest.Beneficiary.SourceOfFunds;
                    //b.Gender = incomingRequest.Beneficiary.Gender;
                    //b.TaxCountry = incomingRequest.Beneficiary.TaxCountry;
                    //b.Name = incomingRequest.Beneficiary.Name;
                    //b.FirstName = incomingRequest.Beneficiary.FirstName;
                    //b.LastName1 = incomingRequest.Beneficiary.LastName1;
                    //b.LastName2 = incomingRequest.Beneficiary.LastName2;
                    //b.MiddleName = incomingRequest.Beneficiary.MiddleName;
                    //b.IDType = incomingRequest.Beneficiary.IDType;
                    //b.IDTypeID = incomingRequest.Beneficiary.IDTypeID;
                    //b.IDNumber = incomingRequest.Beneficiary.IDNumber;
                    //b.IDSerialNumber = incomingRequest.Beneficiary.IDSerialNumber;
                    //b.IDExpDate = incomingRequest.Beneficiary.IDExpDate;
                    //b.IDIssuedDate = incomingRequest.Beneficiary.IDIssuedDate;
                    //b.IDIssuedByCountry = incomingRequest.Beneficiary.IDIssuedByCountry;
                    //b.IDIssuedByState = incomingRequest.Beneficiary.IDIssuedByState;
                    //b.IDIssuedByStateID = incomingRequest.Beneficiary.IDIssuedByStateID;
                    //b.IDIssuedBy = incomingRequest.Beneficiary.IDIssuedBy;
                    //b.IDIssuer = incomingRequest.Beneficiary.IDIssuer;
                    //b.PhoneNumber = incomingRequest.Beneficiary.PhoneNumber;
                    //b.EmailAddress = incomingRequest.Beneficiary.EmailAddress;
                    //b.Address = incomingRequest.Beneficiary.Address;
                    //b.City = incomingRequest.Beneficiary.City;
                    //b.CityID = incomingRequest.Beneficiary.CityID;
                    //b.Country = incomingRequest.Beneficiary.Country;
                    //b.District = incomingRequest.Beneficiary.District;
                    //b.County = incomingRequest.Beneficiary.County;
                    //b.State = incomingRequest.Beneficiary.State;
                    //b.StateID = incomingRequest.Beneficiary.StateID;
                    //b.PostalCode = incomingRequest.Beneficiary.PostalCode;
                    //b.DateOfBirth = incomingRequest.Beneficiary.DateOfBirth;
                    //b.Nationality = incomingRequest.Beneficiary.Nationality;
                    //b.CountryOfBirth = incomingRequest.Beneficiary.CountryOfBirth;
                    //b.BirthStateID = incomingRequest.Beneficiary.BirthStateID;
                    //b.BirthCityID = incomingRequest.Beneficiary.BirthCityID;
                    //b.BirthCity = incomingRequest.Beneficiary.BirthCity;
                    //b.CountryOfResidence = incomingRequest.Beneficiary.CountryOfResidence;
                    //b.TaxID = incomingRequest.Beneficiary.TaxID;
                    //b.Occupation = incomingRequest.Beneficiary.Occupation;
                    //b.OccupationID = incomingRequest.Beneficiary.OccupationID;
                    //b.Curp = incomingRequest.Beneficiary.Curp;
                    //b.TaxAmount = incomingRequest.Beneficiary.TaxAmount;
                    //b.DoesNotHaveATaxID = incomingRequest.Beneficiary.DoesNotHaveATaxID;
                    //b.CustRelationshipID = incomingRequest.Beneficiary.CustRelationshipID;
                    //b.CustRelationship = incomingRequest.Beneficiary.CustRelationship;
                    //request.Beneficiary = b;
                    //SenderInfo s = new SenderInfo();
                    //s.CustomerInternalID = incomingRequest.Sender.CustomerInternalID;
                    //s.CustomerExternalID = incomingRequest.Sender.CustomerExternalID;
                    //s.Name = incomingRequest.Sender.Name;
                    //s.FirstName = incomingRequest.Sender.FirstName;
                    //s.LastName1 = incomingRequest.Sender.LastName1;
                    //s.LastName2 = incomingRequest.Sender.LastName2;
                    //s.MiddleName = incomingRequest.Sender.MiddleName;
                    //s.Address = incomingRequest.Sender.Address;
                    //s.City = incomingRequest.Sender.City;
                    //s.CityID = incomingRequest.Sender.CityID;
                    //s.District = incomingRequest.Sender.District;
                    //s.County = incomingRequest.Sender.County;
                    //s.State = incomingRequest.Sender.State;
                    //s.StateID = incomingRequest.Sender.StateID;
                    //s.Country = incomingRequest.Sender.Country;
                    //s.PostalCode = incomingRequest.Sender.PostalCode;
                    //s.PhoneNumber = incomingRequest.Sender.PhoneNumber;
                    //s.EmailAddress = incomingRequest.Sender.EmailAddress;
                    //s.Gender = incomingRequest.Sender.Gender;
                    //s.DateOfBirth = incomingRequest.Sender.DateOfBirth;
                    //s.Nationality = incomingRequest.Sender.Nationality;
                    //s.CountryOfBirth = incomingRequest.Sender.CountryOfBirth;
                    //s.BirthStateID = incomingRequest.Sender.BirthStateID;
                    //s.BirthCityID = incomingRequest.Sender.BirthCityID;
                    //s.BirthCity = incomingRequest.Sender.BirthCity;
                    //s.CountryOfResidence = incomingRequest.Sender.CountryOfResidence;
                    //s.TaxID = incomingRequest.Sender.TaxID;
                    //s.DoesNotHaveATaxID = incomingRequest.Sender.DoesNotHaveATaxID;
                    //s.Curp = incomingRequest.Sender.Curp;
                    //s.Occupation = incomingRequest.Sender.Occupation;
                    //s.OccupationID = incomingRequest.Sender.OccupationID;
                    //s.IDType = incomingRequest.Sender.IDType;
                    //s.IDTypeID = incomingRequest.Sender.IDTypeID;
                    //s.IDNumber = incomingRequest.Sender.IDNumber;
                    //s.IDExpDate = incomingRequest.Sender.IDExpDate;
                    //s.IDIssuedDate = incomingRequest.Sender.IDIssuedDate;
                    //s.IDIssuedByCountry = incomingRequest.Sender.IDIssuedByCountry;
                    //s.IDIssuedByState = incomingRequest.Sender.IDIssuedByState;
                    //s.IDIssuedByStateID = incomingRequest.Sender.IDIssuedByStateID;
                    //s.IDIssuedBy = incomingRequest.Sender.IDIssuedBy;
                    //s.IDSerialNumber = incomingRequest.Sender.IDSerialNumber;
                    //request.Sender = s;
                    //ComplianceRun cr = new ComplianceRun();
                    //cr.WatchListExternal = incomingRequest.ComplianceRun.WatchListExternal;
                    //cr.WatchListPassed = incomingRequest.ComplianceRun.WatchListPassed;
                    //cr.FiltersExternal = incomingRequest.ComplianceRun.FiltersExternal;
                    //cr.FiltersPassed = incomingRequest.ComplianceRun.FiltersPassed;
                    //request.ComplianceRun = cr;
                    //request.TellerDrawerInstanceID = incomingRequest.TellerDrawerInstanceID;
                    //request.OrderID = incomingRequest.OrderID;
                    //request.OrderPIN = incomingRequest.OrderPIN;
                    //request.OrderLookupCode = incomingRequest.OrderLookupCode;
                    //request.RecAgentID = incomingRequest.RecAgentID;
                    //request.RecAgentLocID = incomingRequest.RecAgentLocID;
                    //request.RecAgentBranch = incomingRequest.RecAgentBranch;
                    //request.RecAgentAddress = incomingRequest.RecAgentAddress;
                    //request.RecAgentCity = incomingRequest.RecAgentCity;
                    //request.RecAgentState = incomingRequest.RecAgentState;
                    //request.RecAgentPostalCode = incomingRequest.RecAgentPostalCode;
                    //request.RecAgentCountry = incomingRequest.RecAgentCountry;
                    //request.PayAgentID = incomingRequest.PayAgentID;
                    //request.PayAgentLocID = incomingRequest.PayAgentLocID;
                    //request.PayAgentBranch = incomingRequest.PayAgentBranch;
                    //request.PayAgentAddress = incomingRequest.PayAgentAddress;
                    //request.PayAgentCity = incomingRequest.PayAgentCity;
                    //request.PayAgentState = incomingRequest.PayAgentState;
                    //request.PayAgentPostalCode = incomingRequest.PayAgentPostalCode;
                    //request.PayAgentCountry = incomingRequest.PayAgentCountry;
                    //request.PayAgentCountryID = incomingRequest.PayAgentCountryID;
                    //request.SendCurrency = incomingRequest.SendCurrency;
                    //request.SendAmount = incomingRequest.SendAmount;
                    //request.SendCharge = incomingRequest.SendCharge;
                    //request.PayoutCurrency = incomingRequest.PayoutCurrency;
                    //request.PayoutAmount = incomingRequest.PayoutAmount;
                    //request.PayoutMethodID = incomingRequest.PayoutMethodID;
                    //request.CustomerRelationShip = incomingRequest.CustomerRelationShip;
                    //request.CustomerRelationShipID = incomingRequest.CustomerRelationShipID;
                    //request.TransferReasonID = incomingRequest.TransferReasonID;
                    //request.TransferReason = incomingRequest.TransferReason;
                    //request.ApproverID = incomingRequest.ApproverID;
                    //request.ConvertedCurrency = incomingRequest.ConvertedCurrency;
                    //request.ConvertedRate = incomingRequest.ConvertedRate;
                    //request.Override = incomingRequest.Override;

                    var request = new PayoutTransactionRequest()
                    {
                        PersistenceID = incomingRequest.PersistenceID,
                        RequesterInfo = new RequesterInfo
                        {
                            AppID = incomingRequest.RequesterInfo.AppID,
                            AppObjectID = incomingRequest.RequesterInfo.AppObjectID,
                            AgentID = incomingRequest.RequesterInfo.AgentID,
                            AgentLocID = incomingRequest.RequesterInfo.AgentLocID,
                            UserID = incomingRequest.RequesterInfo.UserID,
                            UserLoginID = incomingRequest.RequesterInfo.UserLoginID,
                            TerminalID = incomingRequest.RequesterInfo.TerminalID,
                            TerminalUserID = incomingRequest.RequesterInfo.TerminalUserID,
                            LocalTime = incomingRequest.RequesterInfo.LocalTime,
                            UtcTime = incomingRequest.RequesterInfo.UtcTime,
                            Timezone = incomingRequest.RequesterInfo.Timezone,
                            TimezoneID = incomingRequest.RequesterInfo.TimezoneID,
                            Locale = incomingRequest.RequesterInfo.Locale,
                            Version = incomingRequest.RequesterInfo.Version,
                            AgentCountry = incomingRequest.RequesterInfo.AgentCountry,
                            AgentState = incomingRequest.RequesterInfo.AgentState
                        },
                        Beneficiary = new BeneficiaryInfo
                        {
                            BenInternalID = incomingRequest.Beneficiary.BenInternalID,
                            BenExternalID = incomingRequest.Beneficiary.BenExternalID,
                            SourceOfFunds = incomingRequest.Beneficiary.SourceOfFunds,
                            Gender = incomingRequest.Beneficiary.Gender,
                            TaxCountry = incomingRequest.Beneficiary.TaxCountry,
                            Name = incomingRequest.Beneficiary.Name,
                            FirstName = incomingRequest.Beneficiary.FirstName,
                            LastName1 = incomingRequest.Beneficiary.LastName1,
                            LastName2 = incomingRequest.Beneficiary.LastName2,
                            MiddleName = incomingRequest.Beneficiary.MiddleName,
                            IDType = incomingRequest.Beneficiary.IDType,
                            IDTypeID = incomingRequest.Beneficiary.IDTypeID,
                            IDNumber = incomingRequest.Beneficiary.IDNumber,
                            IDSerialNumber = incomingRequest.Beneficiary.IDSerialNumber,
                            IDExpDate = incomingRequest.Beneficiary.IDExpDate,
                            IDIssuedDate = incomingRequest.Beneficiary.IDIssuedDate,
                            IDIssuedByCountry = incomingRequest.Beneficiary.IDIssuedByCountry,
                            IDIssuedByState = incomingRequest.Beneficiary.IDIssuedByState,
                            IDIssuedByStateID = incomingRequest.Beneficiary.IDIssuedByStateID,
                            IDIssuedBy = incomingRequest.Beneficiary.IDIssuedBy,
                            IDIssuer = incomingRequest.Beneficiary.IDIssuer,
                            PhoneNumber = incomingRequest.Beneficiary.PhoneNumber,
                            EmailAddress = incomingRequest.Beneficiary.EmailAddress,
                            Address = incomingRequest.Beneficiary.Address,
                            City = incomingRequest.Beneficiary.City,
                            CityID = incomingRequest.Beneficiary.CityID,
                            Country = incomingRequest.Beneficiary.Country,
                            District = incomingRequest.Beneficiary.District,
                            County = incomingRequest.Beneficiary.County,
                            State = incomingRequest.Beneficiary.State,
                            StateID = incomingRequest.Beneficiary.StateID,
                            PostalCode = incomingRequest.Beneficiary.PostalCode,
                            DateOfBirth = incomingRequest.Beneficiary.DateOfBirth,
                            Nationality = incomingRequest.Beneficiary.Nationality,
                            CountryOfBirth = incomingRequest.Beneficiary.CountryOfBirth,
                            BirthStateID = incomingRequest.Beneficiary.BirthStateID,
                            BirthCityID = incomingRequest.Beneficiary.BirthCityID,
                            BirthCity = incomingRequest.Beneficiary.BirthCity,
                            CountryOfResidence = incomingRequest.Beneficiary.CountryOfResidence,
                            TaxID = incomingRequest.Beneficiary.TaxID,
                            Occupation = incomingRequest.Beneficiary.Occupation,
                            OccupationID = incomingRequest.Beneficiary.OccupationID,
                            Curp = incomingRequest.Beneficiary.Curp,
                            TaxAmount = incomingRequest.Beneficiary.TaxAmount,
                            DoesNotHaveATaxID = incomingRequest.Beneficiary.DoesNotHaveATaxID,
                            CustRelationshipID = incomingRequest.Beneficiary.CustRelationshipID,
                            CustRelationship = incomingRequest.Beneficiary.CustRelationship
                        },
                        Sender = new SenderInfo
                        {
                            CustomerInternalID = incomingRequest.Sender.CustomerInternalID,
                            CustomerExternalID = incomingRequest.Sender.CustomerExternalID,
                            Name = incomingRequest.Sender.Name,
                            FirstName = incomingRequest.Sender.FirstName,
                            LastName1 = incomingRequest.Sender.LastName1,
                            LastName2 = incomingRequest.Sender.LastName2,
                            MiddleName = incomingRequest.Sender.MiddleName,
                            Address = incomingRequest.Sender.Address,
                            City = incomingRequest.Sender.City,
                            CityID = incomingRequest.Sender.CityID,
                            District = incomingRequest.Sender.District,
                            County = incomingRequest.Sender.County,
                            State = incomingRequest.Sender.State,
                            StateID = incomingRequest.Sender.StateID,
                            Country = incomingRequest.Sender.Country,
                            PostalCode = incomingRequest.Sender.PostalCode,
                            PhoneNumber = incomingRequest.Sender.PhoneNumber,
                            EmailAddress = incomingRequest.Sender.EmailAddress,
                            Gender = incomingRequest.Sender.Gender,
                            DateOfBirth = incomingRequest.Sender.DateOfBirth,
                            Nationality = incomingRequest.Sender.Nationality,
                            CountryOfBirth = incomingRequest.Sender.CountryOfBirth,
                            BirthStateID = incomingRequest.Sender.BirthStateID,
                            BirthCityID = incomingRequest.Sender.BirthCityID,
                            BirthCity = incomingRequest.Sender.BirthCity,
                            CountryOfResidence = incomingRequest.Sender.CountryOfResidence,
                            TaxID = incomingRequest.Sender.TaxID,
                            DoesNotHaveATaxID = incomingRequest.Sender.DoesNotHaveATaxID,
                            Curp = incomingRequest.Sender.Curp,
                            Occupation = incomingRequest.Sender.Occupation,
                            OccupationID = incomingRequest.Sender.OccupationID,
                            IDType = incomingRequest.Sender.IDType,
                            IDTypeID = incomingRequest.Sender.IDTypeID,
                            IDNumber = incomingRequest.Sender.IDNumber,
                            IDExpDate = incomingRequest.Sender.IDExpDate,
                            IDIssuedDate = incomingRequest.Sender.IDIssuedDate,
                            IDIssuedByCountry = incomingRequest.Sender.IDIssuedByCountry,
                            IDIssuedByState = incomingRequest.Sender.IDIssuedByState,
                            IDIssuedByStateID = incomingRequest.Sender.IDIssuedByStateID,
                            IDIssuedBy = incomingRequest.Sender.IDIssuedBy,
                            IDSerialNumber = incomingRequest.Sender.IDSerialNumber
                        },
                        TellerDrawerInstanceID = incomingRequest.TellerDrawerInstanceID,
                        OrderID = incomingRequest.OrderID,
                        OrderPIN = incomingRequest.OrderPIN,
                        OrderLookupCode = incomingRequest.OrderLookupCode,
                        RecAgentID = incomingRequest.RecAgentID,
                        RecAgentLocID = incomingRequest.RecAgentLocID,
                        RecAgentBranch = incomingRequest.RecAgentBranch,
                        RecAgentAddress = incomingRequest.RecAgentAddress,
                        RecAgentCity = incomingRequest.RecAgentCity,
                        RecAgentState = incomingRequest.RecAgentState,
                        RecAgentPostalCode = incomingRequest.RecAgentPostalCode,
                        RecAgentCountry = incomingRequest.RecAgentCountry,
                        PayAgentID = incomingRequest.PayAgentID,
                        PayAgentLocID = incomingRequest.PayAgentLocID,
                        PayAgentBranch = incomingRequest.PayAgentBranch,
                        PayAgentAddress = incomingRequest.PayAgentAddress,
                        PayAgentCity = incomingRequest.PayAgentCity,
                        PayAgentState = incomingRequest.PayAgentState,
                        PayAgentPostalCode = incomingRequest.PayAgentPostalCode,
                        PayAgentCountry = incomingRequest.PayAgentCountry,
                        PayAgentCountryID = incomingRequest.PayAgentCountryID,
                        SendCurrency = incomingRequest.SendCurrency,
                        SendAmount = incomingRequest.SendAmount,
                        SendCharge = incomingRequest.SendCharge,
                        PayoutCurrency = incomingRequest.PayoutCurrency,
                        PayoutAmount = incomingRequest.PayoutAmount,
                        PayoutMethodID = incomingRequest.PayoutMethodID,
                        CustomerRelationShip = incomingRequest.CustomerRelationShip,
                        CustomerRelationShipID = incomingRequest.CustomerRelationShipID,
                        TransferReasonID = incomingRequest.TransferReasonID,
                        TransferReason = incomingRequest.TransferReason,
                        ApproverID = incomingRequest.ApproverID,
                        ConvertedCurrency = incomingRequest.ConvertedCurrency,
                        ConvertedRate = incomingRequest.ConvertedRate,
                        Override = incomingRequest.Override,
                    };

                    var respMsg = await client.PostAsJsonAsync(PAYOUT_SVC_URI_PAYTRANS, request);
                    if (respMsg.IsSuccessStatusCode)
                    {
                        var result = respMsg.Content.ReadAsStringAsync().Result;
                        try
                        {
                            //If the response was successful it will be in json and can be de-serialized
                            m_ptResp = JsonConvert.DeserializeObject<PayoutTransactionResponse>(result);
                        }
                        catch (Exception)
                        {
                            //If the response was an error code/message it may not have any other json, so get the error text.
                            m_ptResp.ReturnInfo.ErrorCode = 929;
                            m_ptResp.ReturnInfo.ErrorMessage = result;
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_ptResp.ReturnInfo.AvailableForPayout = false;
                m_ptResp.ReturnInfo.ErrorCode = 909;
                m_ptResp.ReturnInfo.ErrorMessage = "ERROR: Could not get response from payout service: " + e.Message;
                return;
            }
        }





    }
}
