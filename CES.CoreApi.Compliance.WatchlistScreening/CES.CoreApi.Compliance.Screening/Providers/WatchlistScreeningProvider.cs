//using eh.actimize.com;
using AutoMapper;
using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Utilities;
using eh.actimize.com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CES.CoreApi.Compliance.Screening.Providers
{
	/// <summary>
	/// This class handles all interaction with external providers e.g. Actimize WLF, RIA Database etc.
	/// </summary>
	public class WatchlistScreeningProvider
    {
		/// <summary>
		/// Performs transaction fiter by calling speficic provider, in this case Actimize WLF
		/// TODO: update parameters, below are just mocked!
		/// </summary>
		/// <returns>true if hits are found, otherwise false</returns>
		public virtual ActimizeResponse AllowTransaction(Request request, Party party, Rule rule)
		{
            return GetMsRealTimeWsProviderOne(request, party, rule);

			//return result.IsAlerted;
			//throw new NotImplementedException();
		}

        public ActimizeResponse GetMsRealTimeWsProviderOnePing(string originatorName, string originatorCountryCd)
        {

            var response = new ActimizeResponse();
            int score;
            bool isAlerted;
            bool hasHits;
            string alertId;
            MSRealTimeWSProvider_1_Hits_SetType hits = null;

            EHProxyClient client = new EHProxyClient();
            try
            {
                EHResult result = client.MSRealTimeWSProvider_1(
                    searchDefId: "TEST",
                    businessUnit: "All",
                    partyKey: "12345",
                    messageKey: "US123456",
                    messageInstanceNumber: "01",
                    messageRefNumber: "33333",
                    messageSourceType: "Online",
                    messageTypeCd: "Test",
                    messageDateTime: DateTime.Now.ToString("dd/MM/yyyy"),
                    amount: "",
                    currencyCd: "",
                    productKey: "",
                    messageDirection: "TEST",
                    messageInstructions: "",
                    additionalMessageInfo: "",
                    messageText: "",
                    originator: new MSRealTimeWSProvider_1_Originator_TupleType
                    {
                        originatorCountryCd = originatorCountryCd,
                        originatorName = originatorName,


                    },
                    originating: new MSRealTimeWSProvider_1_Originating_TupleType
                    {
                        originatingFICd = "",
                        originatingFICountryCd = "",
                        originatingFIOrgName = ""
                    },
                    beneficiary: new MSRealTimeWSProvider_1_Beneficiary_TupleType
                    {
                        beneficiaryAdditionalInfo = "",
                        beneficiaryAddressLine1 = "",
                        beneficiaryAddressLine2 = "",
                        beneficiaryAddressLine3 = "",
                        beneficiaryCity = "",
                        beneficiaryCountryCd = "",
                        beneficiaryFI = new MSRealTimeWSProvider_1_BeneficiaryFI_TupleType
                        {
                            beneficiaryFICd = "",
                            beneficiaryFICountryCd = "",
                            beneficiaryFIOrgName = "",
                            beneficiaryFIPartyTypeCd = ""
                        },

                        beneficiaryPOBox = "",
                        beneficiaryPrimaryName = "",
                        beneficiaryStateProvince = "",
                        beneficiaryZipCd = ""
                    },
                    intermediate: new MSRealTimeWSProvider_1_Intermediate_SetType
                    {
                    },
                    receiving: new MSRealTimeWSProvider_1_Receiving_TupleType { },
                    sending: new MSRealTimeWSProvider_1_Sending_TupleType { },
                    fiToFiInfo: "",
                    customFields: new MSRealTimeWSProvider_1_CustomFields_TupleType
                    {


                    },
                    alertId:out alertId,
                    score: out score,
                    isAlerted: out isAlerted,
                    hasHits: out hasHits,
                    hits: out hits
                   
                );

                response.ReturnCode = result.returnCode;
                response.Message = result.message;
                response.IsAlerted = isAlerted;
                response.HasHits = hasHits;
                response.Score = score;
                response.AlertId = alertId;



                if (hasHits && hits != null)
                {
                    if (hits.hits_InnerSet != null && hits.hits_InnerSet.Length  > 0)
                        response.Hits = Mapper.Map<List<Hit>>(hits.hits_InnerSet);
                }
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            return response;
        }
        private ActimizeResponse GetMsRealTimeWsProviderOne(Request request, Party party, Rule rule)
        {

            var response = new ActimizeResponse();
            int score;
            string alertId;
            bool isAlerted;
            bool hasHits;
            var originatorName = $"{party.FirstName} {party.MiddleName} {party.LastName1} {party.LastName2}";
            var originatorCountryCd = request.CountryTo;
            MSRealTimeWSProvider_1_Hits_SetType hits = null;
            MSRealTimeWSProvider_1_OriginatorPartyIds_InnerSet_TupleType[] IDs = null;
            var portalURL = string.Empty;     
                    

            //Evaluate ids of request
            if (party.IDs!=null && party.IDs.Any())
            {
                IDs = new MSRealTimeWSProvider_1_OriginatorPartyIds_InnerSet_TupleType[party.IDs.Count()];
                for (int index = 0; index < party.IDs.Count(); index++)
                {
                    var requestID = party.IDs.ElementAt(index);
                    IDs[index] = new MSRealTimeWSProvider_1_OriginatorPartyIds_InnerSet_TupleType()
                    {
                        idCountry = requestID.IdCountry,
                        idNumber = requestID.IdNumber,
                        idType = ((int)requestID.IdType).ToString()
                    };
                }
               
            }

            //Set PortalURL
            switch(party.Type)
            {
                case PartyType.Customer:
                    portalURL = WebUtility.HtmlDecode(string.Format(AppSettings.PortalURLCustomer,request.OrderId, party.Id));
                    break;

                case PartyType.Beneficiary:
                    portalURL = WebUtility.HtmlDecode(string.Format(AppSettings.PortalURLBeneficiary, request.OrderId, party.Id));
                    break;

                case PartyType.OnBehalfOf:
                    portalURL = WebUtility.HtmlDecode(string.Format(AppSettings.PortalURLOnBehalf, request.OrderId, party.Id));
                    break;
            }

            Logging.Log.Info($"PortalURL ({party.Type}): {portalURL}");

            var containPep = (rule.SearchDef ?? "").ToUpper().Contains("PEP");
            var urlServices = (containPep ? AppSettings.UrlPep : AppSettings.UrlSanctions);

            Logging.Log.Info($"UrlService: {urlServices} ({(containPep?"PEP": "Sanctions")})");

            EHProxyClient client = new EHProxyClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(urlServices);
            client.Endpoint.Behaviors.Add(new MessageInspectorBehavior());
            try
            {
                
                EHResult result = client.MSRealTimeWSProvider_1(
                    searchDefId: rule.SearchDef,
                    businessUnit: rule.BusinessUnit,
                    partyKey: party.Id.ToString(),
                    messageKey: request.OrderNo,
                    messageInstanceNumber: ((int)request.ServiceId).ToString(),
                    messageRefNumber: string.Empty,
                    messageSourceType: request.EntryType,
                    messageTypeCd: request.OrderPin ?? string.Empty,
                    messageDateTime: request.TransDateTime.ToString("dd/MM/yyyy"),
                    amount: request.SendTotalAmount.ToString(),
                    currencyCd: request.SendCurrency,
                    productKey: request.ProductId.ToString(),
                    messageDirection: party.Type.ToString(),
                    messageInstructions: rule.fActionID.ToString(),
                    additionalMessageInfo: party.Id.ToString(),
                    messageText: string.Empty,
                    originator: new MSRealTimeWSProvider_1_Originator_TupleType
                    {
                        originatorName = originatorName,
                        originatorPartyIds = new MSRealTimeWSProvider_1_OriginatorPartyIds_SetType
                        {
                            originatorPartyIds_InnerSet = IDs,

                        },
                        originatorAdditionalInfo = party.Gender,
                        originatorAddressLine1 = party.Address,
                        originatorAddressLine2 = request.TransferReason,
                        originatorAddressLine3 = party.Ocupation,
                        originatorCity = party.City,
                        originatorCountryCd = request.CountryTo,
                        originatorPartyTypeCd = "Person",
                        originatorPOBox = party.TelephoneNumber,
                        originatorStateProvince = party.State,
                        originatorZipCd = party.ZipCode,

                    },
                    originating: new MSRealTimeWSProvider_1_Originating_TupleType
                    {
                        originatingFICd = request.ReceivingAgent.PhoneNumber,
                        originatingFICountryCd = party.CountryOfBirth,
                        originatingFIOrgName = $"{request.ReceivingAgent.Branch} {request.ReceivingAgent.ID}",
                    },

                    beneficiary: new MSRealTimeWSProvider_1_Beneficiary_TupleType
                    {
                        beneficiaryAdditionalInfo = request.CallEvent.ToString(),
                        beneficiaryAddressLine1 = "",
                        beneficiaryAddressLine2 = "",
                        beneficiaryAddressLine3 = "",
                        beneficiaryCity = "",
                        beneficiaryCountryCd = "",
                        beneficiaryFI = new MSRealTimeWSProvider_1_BeneficiaryFI_TupleType
                        {
                            beneficiaryFICd = "",
                            beneficiaryFICountryCd = "",
                            beneficiaryFIOrgName = "",
                            beneficiaryFIPartyTypeCd = ""
                        },

                        beneficiaryPOBox = "",
                        beneficiaryPrimaryName = party.IdOnFile.ToString(),
                        beneficiaryStateProvince = "",
                        beneficiaryZipCd = ""
                    },
                    intermediate: new MSRealTimeWSProvider_1_Intermediate_SetType
                    {
                    },
                    receiving: new MSRealTimeWSProvider_1_Receiving_TupleType { },
                    sending: new MSRealTimeWSProvider_1_Sending_TupleType { },
                    fiToFiInfo: "",// TODO: Remove when Thuan fix Actimize URL Portal portalURL,
                    customFields: new MSRealTimeWSProvider_1_CustomFields_TupleType
                    {
                       
                    },
                    alertId: out alertId,
                    score: out score,
                    isAlerted: out isAlerted,
                    hasHits: out hasHits,
                    hits: out hits
                );

                response.ReturnCode = result.returnCode;
                response.Message = result.message;
                response.IsAlerted = isAlerted;
                response.HasHits = hasHits;
                response.Score = score;
                response.AlertId = alertId;

                if (hasHits && hits != null)
                {
                    if (hits.hits_InnerSet != null && hits.hits_InnerSet.Length > 0)
                        response.Hits = Mapper.Map<List<Hit>>(hits.hits_InnerSet);
                }
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            return response;
        }

    }
}