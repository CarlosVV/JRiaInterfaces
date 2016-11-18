using System;
using System.Collections.Generic;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using CES.CoreApi.Payout.Service.Business.Logic.Exceptions;
using System.Collections;
using System.Data.SqlTypes;
using CES.CoreApi.Data.Base;
using CES.CoreApi.Data.Models;
using CES.CoreApi.Data.Enumerations;
using System.Linq;

namespace CES.CoreApi.Payout.Service.Business.Logic.Data
{
    public class RiaRepository : BaseGenericRepository, IRiaRepository
    {
        private static readonly Log4NetProxy _logger = new Log4NetProxy();

      
        #region GetCurrencyCodeData
        public CurrencyCodeModel GetCurrencyCodeData(int codeNum, string codeText)
        {
            
            //Only to set default value to parameter
            //var isoCurrencyCode = string.Empty;            
            //var isoCurrencyNumber = 0;
            //var statusCode = 0;
            //var statusDesc = string.Empty;

            //var outputISOCurrencyCodeParam = new SqlParameter() { ParameterName = "@OutputISOCurrencyCode", SqlDbType = SqlDbType.VarChar, Size = 75, Value = isoCurrencyCode, Direction = ParameterDirection.Output };
            //var outputISOCurrencyNumberParam = new SqlParameter() { ParameterName = "@OutputISOCurrencyNumber", SqlDbType = SqlDbType.Int, Value = isoCurrencyNumber, Direction = ParameterDirection.Output };
            //var statusCodeParam = new SqlParameter() { ParameterName = "@statusCode", SqlDbType = SqlDbType.Int, Value = statusCode, Direction = ParameterDirection.Output };
            //var statusDescParam = new SqlParameter() { ParameterName = "@statusDesc", SqlDbType = SqlDbType.VarChar, Size = 255, Value = statusDesc, Direction = ParameterDirection.Output };

            var request = new DatabaseRequest<CurrencyCodeModel>
            {
                ProcedureName = StoreProcedureConstants.GetCurrencyCodeAndNumber,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@isoCurrencyCode", codeText),
                    new SqlParameter("@isoCurrencyNumber", codeNum) ,
                }
                .AddVarCharOut("@OutputISOCurrencyCode", 75)
                .AddIntOut("@OutputISOCurrencyNumber")
                .AddVarCharOut("@statusDesc",255)
                .AddIntOut("@statusCode"),


                OutputFuncShaper = parameters => GetResponseCurrency(parameters)

            };
            //LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database (GetCurrencyCodeData): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (GetCurrencyCodeData): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (GetCurrencyCodeData): " + e.Message);
            }
        }

        private static CurrencyCodeModel GetResponseCurrency(System.Data.Common.DbParameterCollection parameters)
        {
            //Initialize response instance

            return new CurrencyCodeModel()
            {
                IsoCodeText = parameters.ReadValue<string>("@OutputISOCurrencyCode"),
                IsoCodeNum = parameters.ReadValue<int>("@OutputISOCurrencyNumber")

            };

        }

        #endregion

        #region GetCountryCode
        public  CountryCodeModel GetCountryCodeData(int lookupType, string countryCode)
        {

            //Only to set default value to parameter        
            var statusCode = 9999;
            var statusDesc = string.Empty;
            var countryCodeData = new CountryCodeModel();

            var statusCodeParam = new SqlParameter() { ParameterName = "@statusCode", SqlDbType = SqlDbType.Int, Value = statusCode, Direction = ParameterDirection.Output };
            var statusDescParam = new SqlParameter() { ParameterName = "@statusDesc", SqlDbType = SqlDbType.VarChar, Size = 255, Value = statusDesc, Direction = ParameterDirection.Output };

            var request = new DatabaseRequest<CountryCodeModel>
            {
                ProcedureName = StoreProcedureConstants.GetCountryCode,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fNeedAbbrev_ISO", lookupType),
                    new SqlParameter("@CountryCode", countryCode) ,
                    statusCodeParam,
                    statusDescParam,
                },

                Shaper = reader => GetCountryCode(reader, countryCodeData),
                //OutputFuncShaper = parameters => GetCountryCodeOutputParams(parameters, countryCodeData)

            };
            //LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return  Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (GetCountryCodeData): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (GetCountryCodeData): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (GetCountryCodeData): " + e.Message);
            }
        }

        public IEnumerable<RegCountryModel> GetAllCountries(int languageID)
        {
            
            var request = new DatabaseRequest<RegCountryModel>
            {
                ProcedureName = StoreProcedureConstants.GetCountryAll,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fLanguageID",languageID),

                },
                Shaper = reader => GetRegCountry(reader)
            };
            return GetList(request);
        }

    

        private static CountryCodeModel GetCountryCode(IDataReader reader, CountryCodeModel countryCodeData)
        {
            countryCodeData.Char2CountryCode = reader.ReadValue<string>("fAbbrev");
            countryCodeData.Char3ISOCountryCode = reader.ReadValue<string>("fISOCode_Alpha3");
            countryCodeData.ISONumericCode = reader.ReadValue<int>("fISOCode");
            countryCodeData.CountryDescription = reader.ReadValue<string>("fCountry");
            countryCodeData.ResultSetHasRows = true;
            return countryCodeData;
        }

        private static RegCountryModel GetRegCountry(IDataReader reader)
        {
            RegCountryModel regCountry = new RegCountryModel();
            regCountry.CountryID = reader.ReadValue<int>("fCountryID");
            regCountry.Country = reader.ReadValue<string>("fCountry");
            regCountry.Abbrev = reader.ReadValue<string>("fAbbrev");
            regCountry.Code = reader.ReadValue<string>("fCode");
            regCountry.ISOCode = reader.ReadValue<string>("fISOCode");
            regCountry.Note = reader.ReadValue<string>("fNote");
            regCountry.Order = reader.ReadValue<int>("fOrder");
            regCountry.UseCityList = reader.ReadValue<bool>("fUseCityList");
            regCountry.ISOCode_Alpha3 = reader.ReadValue<string>("fISOCode_Alpha3");
            regCountry.CountryCurrency = reader.ReadValue<string>("fCountryCurrency");
            return regCountry;
        }

        private CountryCodeModel  GetCountryCodeOutputParams(System.Data.Common.DbParameterCollection parameters, CountryCodeModel countryCodeData)
        {      //OUTPUT PARAMETERS:
            int statusCode = 9999;
            try
            {
                statusCode  = parameters.ReadValue<int>("@statusCode");
              
            }
            catch (Exception)
            {
                throw new StatusCodeReturnedException(9999, "Null Status Code Returned.");
            }
          
            string statusDesc = parameters.ReadValue<string>("@statusCode");

            if (statusCode != 0)
            {
                throw new InvalidDataException(
                    Messages.S_GetMessage("ErrorInvalidCountryCode")
                    + statusDesc);
            }

            if (!countryCodeData.ResultSetHasRows)
            {
                throw new InvalidDataException(
                        Messages.S_GetMessage("ErrorInvalidCountryCode"));
            }

       
            return countryCodeData;

        }
       
        #endregion

        #region GetOrderInfo
       
        public GetTransactionInfoResponseModel GetOrderInfo(GetTransactionInfoRequestModel requestModel)
        {
            var request = new DatabaseRequest<GetTransactionInfoResponseModel>
            {
                ProcedureName = StoreProcedureConstants.mt_sp_Payout_OrderInfo_Get,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lAgentID",requestModel.RequesterInfo.AgentID),
                    new SqlParameter("@lAgentLocID",requestModel.RequesterInfo.AgentLocID),
                    new SqlParameter("@lUserNameID",requestModel.RequesterInfo.UserLoginID),
                    new SqlParameter("@sLocale",requestModel.RequesterInfo.Locale),
                    new SqlParameter("@lOrderID",requestModel.OrderID),
                    new SqlParameter("@OrderRefNo",requestModel.OrderPIN),
                    new SqlParameter("@OrderLookupCode",DBNull.Value),
                    new SqlParameter("@AgentCountry",requestModel.RequesterInfo.AgentCountry.ToSafeDbString()),
                    new SqlParameter("@AgentState",requestModel.RequesterInfo.AgentState.ToSafeDbString())
                },
                Shaper = reader => GetOrderInfo(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch(Exception e)
            {
                _logger.PublishError("Error Calling Database: (GetOrderInfo): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (GetOrderInfo): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (GetOrderInfo): " + e.Message);
            }
        }


        /// <summary>
        /// Call the Order Info SP for third party/ external correspondents.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public GetTransactionInfoResponseModel GetOrderInfoExternal(
            GetTransactionInfoRequestModel originalRequest,
            GetTransactionInfoResponseModel providerCallResponse,
            string xmlRequiredFields)
        {
            var request = new DatabaseRequest<GetTransactionInfoResponseModel>
            {
                ProcedureName = StoreProcedureConstants.PayoutGetOrderInfoExternal,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lAgentID",originalRequest.RequesterInfo.AgentID),
                    new SqlParameter("@lAgentLocID",originalRequest.RequesterInfo.AgentLocID),
                    new SqlParameter("@lUserNameID",originalRequest.RequesterInfo.UserID),
                    new SqlParameter("@sLocale",originalRequest.RequesterInfo.Locale),
                    new SqlParameter("@TellerDrawerInstanceID", -1),//TODO: Can we get this?
                    new SqlParameter("@AgentCountry", originalRequest.CountryTo),
                    new SqlParameter("@lAgentCountryID", -1), //TODO: Can we get this?
                    new SqlParameter("@AgentState", originalRequest.StateTo),
                    new SqlParameter("@Payout_LocalTime", originalRequest.RequesterInfo.LocalTime),
                    new SqlParameter("@SendAgentID", providerCallResponse.ProviderInfo.ProviderID),
                    new SqlParameter("@SendAgentLocID", -1),// providerCallResponse.RecAgentID),//TODO: This is the location returned from provider (GC uses a string, but this field is an INT)
                    new SqlParameter("@SendAgentBranchNo",  ""),//TODO: Can we get this?
                    new SqlParameter("@SendAgentCountry", providerCallResponse.CountryFrom),
                    new SqlParameter("@SendAgentState", ""),//TODO: Can we get this?
                    new SqlParameter("@SendAgentRequiredFields", xmlRequiredFields),
                    new SqlParameter("@lOrderID", originalRequest.OrderID),
                    new SqlParameter("@OrderLookupCode", DBNull.Value),
                    new SqlParameter("@PIN", originalRequest.OrderPIN),
                    new SqlParameter("@OrderNo", ""),
                    new SqlParameter("@OrderDate", providerCallResponse.TransferDate),
                    new SqlParameter("@OrderStatus", providerCallResponse.TransferStatus),
                    new SqlParameter("@OrderStatusTime", ""),//TODO: Can we get this?
                    new SqlParameter("@bAvailableForPayout", (providerCallResponse.ReturnInfo.AvailableForPayout)),
                    new SqlParameter("@PayoutCurrency", providerCallResponse.PayoutAmount.CurrencyCode),
                    new SqlParameter("@PayoutAmount", providerCallResponse.PayoutAmount.Amount),
                    new SqlParameter("@PayoutCountry", originalRequest.CountryTo),
                    new SqlParameter("@SendCurrency", providerCallResponse.PayoutAmount.CurrencyCode),
                    new SqlParameter("@SendAmount", providerCallResponse.PayoutAmount.Amount),
                    new SqlParameter("@SendCharge", 0.01m),//Use 0 since we don't know what the transaction amounts are yet.
                    new SqlParameter("@SenderInternalID", 0),//Use 0 since we don't know who the sender is yet.
                    new SqlParameter("@SenderExternalNo", "0"),//Use 0 since we don't know who the sender is yet.
                    new SqlParameter("@SenderNameFirst", providerCallResponse.SenderInfo.FirstName),
                    new SqlParameter("@SenderNameMid", providerCallResponse.SenderInfo.MiddleName),
                    new SqlParameter("@SenderNameLast1", providerCallResponse.SenderInfo.LastName1),
                    new SqlParameter("@SenderNameLast2", providerCallResponse.SenderInfo.LastName2),
                    new SqlParameter("@SenderTelNo", providerCallResponse.SenderInfo.PhoneNumber),
                    new SqlParameter("@SenderAddress", providerCallResponse.SenderInfo.Address),
                    new SqlParameter("@SenderCity", providerCallResponse.SenderInfo.City),
                    new SqlParameter("@SenderState", providerCallResponse.SenderInfo.State),
                    new SqlParameter("@SenderPostalCode", providerCallResponse.SenderInfo.PostalCode),
                    new SqlParameter("@SenderCountry", providerCallResponse.SenderInfo.Country),
                    new SqlParameter("@BenInternalNameID", 0),//Use 0 since we don't know who the beneficiary is yet on Ria or GC systems.
                    new SqlParameter("@BenInternalID", 0),//Use 0 since we don't know who the beneficiary is yet on Ria or GC systems.
                    new SqlParameter("@BenExternalNo", "0"),//Use 0 since we don't know who the beneficiary is yet on Ria or GC systems.
                    new SqlParameter("@BenNameFirst", providerCallResponse.BeneficiaryInfo.FirstName),
                    new SqlParameter("@BenNameMid", providerCallResponse.BeneficiaryInfo.MiddleName),
                    new SqlParameter("@BenNameLast1", providerCallResponse.BeneficiaryInfo.LastName1),
                    new SqlParameter("@BenNameLast2", providerCallResponse.BeneficiaryInfo.LastName2),
                    new SqlParameter("@BenTelNo", providerCallResponse.BeneficiaryInfo.PhoneNumber),
                    new SqlParameter("@BenAddress", providerCallResponse.BeneficiaryInfo.Address),
                    new SqlParameter("@BenCity", providerCallResponse.BeneficiaryInfo.City),
                    new SqlParameter("@BenState", providerCallResponse.BeneficiaryInfo.State),
                    new SqlParameter("@BenZip", providerCallResponse.ReceiverPostalCode),
                    new SqlParameter("@BenCountry", providerCallResponse.BeneficiaryInfo.Country)
                },
                Shaper = reader => GetOrderInfo(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (GetOrderInfoExternal): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (GetOrderInfoExternal): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (GetOrderInfoExternal): " + e.Message);
            }
        }


        private static GetTransactionInfoResponseModel GetOrderInfo(IDataReader reader)
        {

            //Initialize  ResultInfo with 1° result (rst_Result)
            var orderInfo = InitializeOrderInfoWithResult(reader);
            if (orderInfo == null)
            {
                return null;
            }

            //Fill  OrderInfo with 2° result (rst_OrderInfo)
            reader.NextResult();
            SetOrderInfo(reader, orderInfo);

            //Fill  CustomerMessages  with 3° result (rst_CustSvcMsgs)
            reader.NextResult();
            SetCustomerMessages(reader, orderInfo);

            //Fill  PayoutFields  with 4° result (rst_PayoutFields)
            reader.NextResult();
            SetPayoutFields(reader, orderInfo);

            //Fill  Beneficiary Info  with 5° result (rst_BenInfo)
            reader.NextResult();
            SetBeneficiaryInfo(reader, orderInfo);

            return orderInfo;
        }

        private static GetTransactionInfoResponseModel InitializeOrderInfoWithResult(IDataReader reader)
        {
            return new GetTransactionInfoResponseModel()
            {
                ReturnInfo = new ReturnInfoModel()
                {
                    ErrorCode =(reader.ReadValue<int>("ErrorCode") ==0?0: reader.ReadValue<int>("ErrorCode")),
                    ErrorMessage = reader.ReadValue<string>("ErrorMessage") ?? string.Empty,

                    //NOTE: The SP call here has 2 return values
                    //AvailableForPayout = Overall result of call for this agent. Return this value to the caller
                    //OrderStatus = This is the status of the order. It may be ready to pay out, but this specfic agent can't pay it now.
                    //From John: The returned flag AvailableForPayout is always the one that needs to be respected in terms of 
                    //whether or not someone is allowed to pay out the order.
                    AvailableForPayout = reader.ReadValue<bool>("AvailableForPayout"),
                    AllowUnusualOrderReporting = reader.ReadValue<string>("AllowUnusualOrderReporting") ?? string.Empty,
                    RemainingBalanceWarningMsg = reader.ReadValue<string>("RemainingBalanceWarningMsg") ?? string.Empty,
                }
            };
        }

        private static void SetOrderInfo(IDataReader reader, GetTransactionInfoResponseModel orderInfo)
        {
            if (reader.Read())
            {
                orderInfo.OrderID =  reader.ReadValue<string>("OrderId")??string.Empty;
            orderInfo.TransferDate = reader.ReadValue<DateTime>("OrderDate");
                orderInfo.TransferStatus = reader.ReadValue<string>("OrderStatus") ?? string.Empty;

            //NoData orderInfo.SendAmount = new MoneyModel(0,null);
            orderInfo.PayoutAmount = new MoneyModel(reader.ReadValue<decimal>("PayoutAmount"), reader.ReadValue<string>("PayoutCurrency"));
            //NoData orderInfo.ExchangeRate = null;
            //NoData orderInfo.Comission = new MoneyModel(0, null);
            //NoData orderInfo.ReceiverComission = new MoneyModel(0, null);
            //NoData orderInfo.SenderComission = new MoneyModel(0, null);

            //NoData orderInfo.FromAgentID = null;
                orderInfo.CountryFrom = reader.ReadValue<string>("CustCountry") ?? string.Empty;
                orderInfo.CountryTo = reader.ReadValue<string>("BenCountry") ?? string.Empty;
            //NoData orderInfo.PayDataMessage = null;
            //NoData orderInfo.SenderIsResident = null;
            //NoData orderInfo.ReceiverIsResident = null;

                orderInfo.SenderInfo.FirstName = reader.ReadValue<string>("CustomerNameFirst") ?? string.Empty;
                orderInfo.SenderInfo.MiddleName = reader.ReadValue<string>("CustomerNameMid") ?? string.Empty;
                orderInfo.SenderInfo.LastName1 = reader.ReadValue<string>("CustomerNameLast1") ?? string.Empty;
                orderInfo.SenderInfo.LastName2 = reader.ReadValue<string>("CustomerNameLast2") ?? string.Empty;
                orderInfo.SenderInfo.Name = string.Format("{0} {1} {2} {3}", orderInfo.SenderInfo.FirstName, orderInfo.SenderInfo.MiddleName, orderInfo.SenderInfo.LastName1, orderInfo.SenderInfo.LastName2);
                orderInfo.SenderInfo.Address = reader.ReadValue<string>("CustAddress") ?? string.Empty;
                orderInfo.SenderInfo.City = reader.ReadValue<string>("CustCity") ?? string.Empty;
                orderInfo.SenderInfo.State = reader.ReadValue<string>("CustState") ?? string.Empty;
                orderInfo.SenderInfo.Country = reader.ReadValue<string>("CustCountry") ?? string.Empty;
                orderInfo.SenderInfo.PhoneNumber = reader.ReadValue<string>("CustomerTelNo") ?? string.Empty;
                orderInfo.SenderInfo.PostalCode = reader.ReadValue<string>("CustPostalCode") ?? string.Empty;
                //NoData 
                orderInfo.SenderInfo.EmailAddress = string.Empty;
                //NoData 
                orderInfo.SenderInfo.IDTypeID = 0;
                //NoData 
                orderInfo.SenderInfo.IDType = string.Empty;
                //NoData 
                orderInfo.SenderInfo.IDNumber = string.Empty;
                //NoData 
                orderInfo.SenderInfo.IDIssuedBy = string.Empty;
                //NoData 
                orderInfo.SenderInfo.IDSerialNumber = string.Empty;


                orderInfo.BeneficiaryInfo.FirstName = reader.ReadValue<string>("BeneficiaryNameFirst") ?? string.Empty;
                orderInfo.BeneficiaryInfo.MiddleName = reader.ReadValue<string>("BeneficiaryNameMid") ?? string.Empty;
                orderInfo.BeneficiaryInfo.LastName1 = reader.ReadValue<string>("BeneficiaryNameLast1") ?? string.Empty;
                orderInfo.BeneficiaryInfo.LastName2 = reader.ReadValue<string>("BeneficiaryNameLast2") ?? string.Empty;
                orderInfo.BeneficiaryInfo.Name = string.Format("{0} {1} {2} {3}", orderInfo.BeneficiaryInfo.FirstName, orderInfo.BeneficiaryInfo.MiddleName, orderInfo.BeneficiaryInfo.LastName1, orderInfo.BeneficiaryInfo.LastName2);
                orderInfo.BeneficiaryInfo.Address = reader.ReadValue<string>("BenAddress") ?? string.Empty;
                orderInfo.BeneficiaryInfo.City = reader.ReadValue<string>("BenCity") ?? string.Empty;
                orderInfo.BeneficiaryInfo.State = reader.ReadValue<string>("BenState") ?? string.Empty;
                orderInfo.BeneficiaryInfo.Country = reader.ReadValue<string>("BenCountry") ?? string.Empty;
                orderInfo.ReceiverFullCountryName = reader.ReadValue<string>("BenCountryFullName") ?? string.Empty;
                orderInfo.ReceiverPostalCode = reader.ReadValue<string>("BenZip") ?? string.Empty;
                orderInfo.BeneficiaryInfo.PhoneNumber = reader.ReadValue<string>("BenTelNo") ?? string.Empty;
            //NoData orderInfo.ReceiverEmail = null;
            //NoData orderInfo.ReceiverIDTypeID = 0;
            //NoData orderInfo.ReceiverIDTypeText = null;
            orderInfo.BeneficiaryInfo.IDNumber = reader.ReadValue<string>("BenID"); //BenNameID ??
            //NoData orderInfo.ReceiverIDIssuer = null;
            //NoData orderInfo.ReceiverIDSerialNumber = null;

            //NoData orderInfo.PayDataNotAfterDate = new DateTime();
            //NoData orderInfo.PayDataNotAfterDateSpecified = false;
            //NoData orderInfo.PayDataNotBeforeDate = new DateTime();
            //NoData orderInfo.PayDataNotBeforeDateSpecified = false;

            //NoData orderInfo.OnLegalHold = false;

                orderInfo.OrderPIN = reader.ReadValue<string>("PIN") ?? string.Empty;
                orderInfo.PASeqID = reader.ReadValue<string>("PASeqID") ?? string.Empty;
                orderInfo.PayAgent = reader.ReadValue<string>("PayAgent") ?? string.Empty;
                orderInfo.PayAgentBranchName = reader.ReadValue<string>("PayAgentBranchName") ?? string.Empty;
                orderInfo.PayAgentBranchNo = reader.ReadValue<string>("PayAgentBranchNo") ?? string.Empty;
                orderInfo.DeliveryMethod = reader.ReadValue<string>("DeliveryMethod") ?? string.Empty;
                orderInfo.BeneficiaryTax = reader.ReadValue<decimal>("BeneficiaryTax") ;
            orderInfo.NetAmount = reader.ReadValue<decimal>("NetAmount");

        }
        }

        private static void SetCustomerMessages(IDataReader reader, GetTransactionInfoResponseModel orderInfo)
        {
            while (reader.Read())
            {
                orderInfo.CustomerServiceMessages.Add(new CustomerServiceMessagesModel()
                {
                    MessageID = reader.ReadValue<string>("MessageID") ?? string.Empty,
                    Category = reader.ReadValue<string>("Category") ?? string.Empty,
                    MessageTime = reader.ReadValue<DateTime>("MsgTime"),
                    EnteredBy = reader.ReadValue<string>("EnteredBy") ?? string.Empty,
                    MessageBody = reader.ReadValue<string>("MessageBody") ?? string.Empty,
                });

            }
        }

        private static void SetPayoutFields(IDataReader reader, GetTransactionInfoResponseModel orderInfo)
        {
            while (reader.Read())
            {
                orderInfo.PayoutRequiredFields.Add(new PayoutFieldsModel()
                {
                    FieldID = reader.ReadValue<int>("FieldID"),
                    FieldName = reader.ReadValue<string>("FieldName") ?? string.Empty,
                    DisplayName = reader.ReadValue<string>("DisplayName") ?? string.Empty,
                    FieldRequired = reader.ReadValue<bool>("FieldRequired"),
                });

            }

            
        }

        private static void SetBeneficiaryInfo(IDataReader reader, GetTransactionInfoResponseModel orderInfo)
        {
            if (reader.Read())
            {
            orderInfo.BeneficiaryInfo = new BeneficiaryInfoModel()
            {
                    District = reader.ReadValue<string>("District") ?? string.Empty,
                    County = reader.ReadValue<string>("County") ?? string.Empty,
                DateOfBirth = reader.ReadValue<DateTime>("DateOfBirth"),
                    CountryOfBirth = reader.ReadValue<string>("CountryOfBirth") ?? string.Empty,
                    Nationality = reader.ReadValue<string>("Nationality") ?? string.Empty,
                    OccupationID = reader.ReadValue<int>("OccupationID"),
                    Occupation = reader.ReadValue<string>("Occupation") ?? string.Empty,
                    SourceOfFunds = reader.ReadValue<string>("SourceOfFunds") ?? string.Empty,
                    Gender = reader.ReadValue<string>("Gender") ?? string.Empty,
                    TaxCountry = reader.ReadValue<string>("TaxCountry") ?? string.Empty,
                    TaxID = reader.ReadValue<string>("TaxID") ?? string.Empty,
                    CountryOfResidence = reader.ReadValue<string>("CountryOfResidence") ?? string.Empty,
                };
            }

        }


        #endregion

        #region PayoutOrder
        public PayoutTransactionResponseModel PayoutOrder(PayoutTransactionRequestModel requestModel)
        {

            var request = new DatabaseRequest<PayoutTransactionResponseModel>
            {
                ProcedureName = StoreProcedureConstants.PayoutConfirm,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lAgentID",requestModel.RequesterInfo.AgentID),
                    new SqlParameter("@lAgentLocID",requestModel.RequesterInfo.AgentLocID),
                    new SqlParameter("@lUserNameID",requestModel.RequesterInfo.UserID),
                    new SqlParameter("@lUserLoginID",requestModel.RequesterInfo.UserLoginID),
                    new SqlParameter("@sLocale",requestModel.RequesterInfo.Locale),
                    new SqlParameter("@TellerDrawerInstanceID",requestModel.TellerDrawerInstanceID),
                    new SqlParameter("@lOrderID",requestModel.OrderID),
                    new SqlParameter("@OrderLookupCode",requestModel.OrderLookupCode),
                    new SqlParameter("@bCompliance_Run_WatchList_External",requestModel.ComplianceRun.WatchListExternal),
                    new SqlParameter("@bCompliance_Run_Filters_External",requestModel.ComplianceRun.FiltersExternal),
                    new SqlParameter("@bCompliance_Run_WatchList_Passed",requestModel.ComplianceRun.WatchListPassed),
                    new SqlParameter("@bCompliance_Run_Filters_Passed",requestModel.ComplianceRun.FiltersPassed),
                    new SqlParameter("@AgentCountry",requestModel.PayAgentCountry),
                    new SqlParameter("@lAgentCountryID",requestModel.PayAgentCountryID),
                    new SqlParameter("@AgentState",requestModel.PayAgentState),
                    new SqlParameter("@AgentCity",requestModel.PayAgentCity),
                    new SqlParameter("@Payout_LocalTime",requestModel.RequesterInfo.LocalTime),
                    new SqlParameter("@Payout_TimeZone",requestModel.RequesterInfo.Timezone),
                    new SqlParameter("@Payout_TimeZoneID",requestModel.RequesterInfo.TimezoneID),
                    new SqlParameter("@sBenName",requestModel.Beneficiary.Name),
                    new SqlParameter("@benAddress",requestModel.Beneficiary.Address),
                    new SqlParameter("@benCity",requestModel.Beneficiary.City),
                    new SqlParameter("@benDistrict",requestModel.Beneficiary.District),
                    new SqlParameter("@benState",requestModel.Beneficiary.State),
                    new SqlParameter("@benPostalCode",requestModel.Beneficiary.PostalCode),
                    new SqlParameter("@benCountry",requestModel.Beneficiary.Country),
                    new SqlParameter("@benStateID",requestModel.Beneficiary.StateID),
                    new SqlParameter("@benCityID",requestModel.Beneficiary.CityID),
                    new SqlParameter("@benTelNo",requestModel.Beneficiary.PhoneNumber),
                    new SqlParameter("@benGender",requestModel.Beneficiary.Gender),
                    new SqlParameter("@benDateOfBirth",requestModel.Beneficiary.DateOfBirth),
                    new SqlParameter("@benNationality",requestModel.Beneficiary.Nationality),
                    new SqlParameter("@benCountryOfBirth",requestModel.Beneficiary.CountryOfBirth),
                    new SqlParameter("@benBirthStateID",requestModel.Beneficiary.BirthStateID),
                    new SqlParameter("@benBirthCityID",requestModel.Beneficiary.BirthCityID),
                    new SqlParameter("@benBirthCity",requestModel.Beneficiary.City),
                    new SqlParameter("@benCountryOfResidence",requestModel.Beneficiary.CountryOfResidence),
                    new SqlParameter("@benTaxID",requestModel.Beneficiary.TaxID),
                    new SqlParameter("@benDoesNotHaveATaxID",requestModel.Beneficiary.DoesNotHaveATaxID),
                    new SqlParameter("@benCurp",requestModel.Beneficiary.Curp),
                    new SqlParameter("@benOccupationID",requestModel.Beneficiary.OccupationID),
                    new SqlParameter("@benOccupation",requestModel.Beneficiary.Occupation),
                    new SqlParameter("@benIDType",requestModel.Beneficiary.IDType),
                    new SqlParameter("@benIDTypeID",requestModel.Beneficiary.IDTypeID),
                    new SqlParameter("@benIDNo",requestModel.Beneficiary.IDNumber),
                    new SqlParameter("@benIDExpDate",requestModel.Beneficiary.IDExpDate),
                    new SqlParameter("@benIDIssuedDate",requestModel.Beneficiary.IDIssuedDate),
                    new SqlParameter("@benIDIssuedByCountry",requestModel.Beneficiary.IDIssuedByCountry),
                    new SqlParameter("@benIDIssuedByState",requestModel.Beneficiary.IDIssuedByState),
                    new SqlParameter("@benIDIssuedByStateID",requestModel.Beneficiary.IDIssuedByStateID),
                    new SqlParameter("@benIDIssuedBy",requestModel.Beneficiary.IDIssuedBy),
                    new SqlParameter("@benCustRelationshipID",requestModel.CustomerRelationShipID),
                    new SqlParameter("@benCustRelationship",requestModel.CustomerRelationShip),
                    new SqlParameter("@benTransferReasonID",requestModel.TransferReasonID),
                    new SqlParameter("@benTransferReason",requestModel.TransferReason),
                    new SqlParameter("@BenTax_Amount",requestModel.Beneficiary.TaxAmount)

                },
                Shaper = reader => PayoutOrderResponse(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (PayoutOrder): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (PayoutOrder): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (PayoutOrder): " + e.Message);
            }
        }


        private static PayoutTransactionResponseModel PayoutOrderResponse(IDataReader reader)
        {
            //Initialize  ResultInfo with 1° result (rst_Result)
            var payoutResponse = InitializePayoutResponseWithResult(reader);
            if (payoutResponse == null)
                return null;

        

            //Fill  PayoutFields  with 2° result (rst_PayoutFields)
            reader.NextResult();
            SetPayoutResponseFields(reader, payoutResponse);

          

            return payoutResponse;
          
        }

        private static PayoutTransactionResponseModel InitializePayoutResponseWithResult(IDataReader reader)
        {

            return new PayoutTransactionResponseModel()
            {
                ReturnInfo = new ReturnInfoModel()
                {
                    ErrorCode = reader.ReadValue<int>("ErrorCode"),
                    ErrorMessage = reader.ReadValue<string>("ErrorMessage"),
                    AvailableForPayout = reader.ReadValue<bool>("UsePayoutGateway")
                }
            };
        }
        private static void SetPayoutResponseFields(IDataReader reader, PayoutTransactionResponseModel payoutResponse)
        {
            while (reader.Read())
            {
                payoutResponse.PayoutRequiredFields.Add(new PayoutFieldsModel()
                {
                    FieldID = reader.ReadValue<int>("FieldID"),
                    FieldName = reader.ReadValue<string>("FieldName"),
                    DisplayName = reader.ReadValue<string>("DisplayName"),
                    FieldRequired = reader.ReadValue<bool>("FieldRequired"),
                    DataExists = reader.ReadValue<bool>("DataExists"),
                    DataInvalid = reader.ReadValue<bool>("DataInvalid"),
                    NoteOnData = reader.ReadValue<string>("NoteOnData"),
                    DataErrorCode = reader.ReadValue<string>("DataErrCode"),
                });

        }


        }
        #endregion
        
        #region PayoutConfirmExternalValidate
        public PayoutTransactionResponseModel PayoutConfirmExternalValidate(PayoutTransactionRequestModel requestModel, ref string log)
        {
            //Manage SQL dates
            DateTime minDate = (DateTime)SqlDateTime.MinValue;
            
            DateTime localtime = (requestModel.RequesterInfo.LocalTime == null || requestModel.RequesterInfo.LocalTime < minDate) ? minDate : requestModel.RequesterInfo.LocalTime;
            DateTime sendDOB = (requestModel.Sender.DateOfBirth == null || requestModel.Sender.DateOfBirth < minDate) ? minDate : requestModel.Sender.DateOfBirth;
            DateTime sendIDExp = (requestModel.Sender.IDExpDate == null || requestModel.Sender.IDExpDate < minDate) ? minDate : requestModel.Sender.IDExpDate;
            DateTime sendIDIss = (requestModel.Sender.IDIssuedDate == null || requestModel.Sender.IDIssuedDate < minDate) ? minDate : requestModel.Sender.IDIssuedDate;
            DateTime benDOB = (requestModel.Beneficiary.DateOfBirth == null || requestModel.Beneficiary.DateOfBirth < minDate) ? minDate : requestModel.Beneficiary.DateOfBirth;
            DateTime benIDExp = (requestModel.Beneficiary.IDExpDate == null || requestModel.Beneficiary.IDExpDate < minDate) ? minDate : requestModel.Beneficiary.IDExpDate;
            DateTime benIDIss = (requestModel.Beneficiary.IDIssuedDate == null || requestModel.Beneficiary.IDIssuedDate < minDate) ? minDate : requestModel.Beneficiary.IDIssuedDate;

            var request = new DatabaseRequest<PayoutTransactionResponseModel>
            {
                ProcedureName = StoreProcedureConstants.PayoutConfirmExternalValidate,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lAgentID", requestModel.RequesterInfo.AgentID),
                    new SqlParameter("@lAgentLocID", requestModel.RequesterInfo.AgentLocID),
                    new SqlParameter("@lUserNameID", requestModel.RequesterInfo.UserID),
                    new SqlParameter("@lUserLoginID", requestModel.RequesterInfo.UserLoginID),
                    new SqlParameter("@sLocale", requestModel.RequesterInfo.Locale),
                    new SqlParameter("@TellerDrawerInstanceID", requestModel.TellerDrawerInstanceID),

                    new SqlParameter("@PayoutAgentCountry", requestModel.PayAgentCountry),
                    new SqlParameter("@PayoutAgentCountryID", requestModel.PayAgentCountryID),
                    new SqlParameter("@PayoutAgentState", requestModel.PayAgentState),
                    new SqlParameter("@PayoutAgentCity", requestModel.PayAgentCity),

                    new SqlParameter("@Payout_LocalTime", localtime),
                    new SqlParameter("@Payout_TimeZone", requestModel.RequesterInfo.Timezone),
                    new SqlParameter("@Payout_TimeZoneID", requestModel.RequesterInfo.TimezoneID),
                    new SqlParameter("@Payout_MethodID", requestModel.PayoutMethodID),

                    new SqlParameter("@SendAgentID", requestModel.RecAgentID),
                    new SqlParameter("@SendAgentLocID", requestModel.RecAgentLocID),
                    new SqlParameter("@SendAgentCountry", requestModel.RecAgentCountry),
                    new SqlParameter("@SendAgentCountryID", 0),//TODO: Get country/state IDs
                    new SqlParameter("@SendAgentState", requestModel.RecAgentState),
                    new SqlParameter("@SendAgentCity", requestModel.RecAgentCity),
                    new SqlParameter("@SendAgentRequiredFields", ""), //TODO: Get Agent Info

                    new SqlParameter("@lOrderID", requestModel.OrderID),
                    new SqlParameter("@PIN", requestModel.OrderPIN),
                    new SqlParameter("@ProviderPersistenceID", requestModel.PersistenceID),
                    new SqlParameter("@OrderLookupCode", requestModel.OrderLookupCode),

                    new SqlParameter("@senderName_Full", requestModel.Sender.Name),
                    new SqlParameter("@senderName_First", requestModel.Sender.FirstName),
                    new SqlParameter("@senderName_Mid", requestModel.Sender.MiddleName),
                    new SqlParameter("@senderName_Last1", requestModel.Sender.LastName1),
                    new SqlParameter("@senderName_Last2", requestModel.Sender.LastName2),

                    new SqlParameter("@senderID_Internal", requestModel.Sender.CustomerInternalID),
                    new SqlParameter("@senderID_External", requestModel.Sender.CustomerExternalID),

                    new SqlParameter("@senderAddress", requestModel.Sender.Address),
                    new SqlParameter("@senderCity", requestModel.Sender.City),
                    new SqlParameter("@senderDistrict", requestModel.Sender.District),
                    new SqlParameter("@senderCounty", requestModel.Sender.County),
                    new SqlParameter("@senderState", requestModel.Sender.State),
                    new SqlParameter("@senderPostalCode", requestModel.Sender.PostalCode),
                    new SqlParameter("@senderCountry", requestModel.Sender.Country),
                    new SqlParameter("@senderStateID", requestModel.Sender.StateID),
                    new SqlParameter("@senderCityID", requestModel.Sender.CityID),
                    new SqlParameter("@senderTelNo", requestModel.Sender.PhoneNumber),
                    new SqlParameter("@senderGender", requestModel.Sender.Gender),

                    new SqlParameter("@senderDateOfBirth", sendDOB),
                    new SqlParameter("@senderNationality", requestModel.Sender.Nationality),
                    new SqlParameter("@senderCountryOfBirth", requestModel.Sender.CountryOfBirth),
                    new SqlParameter("@senderBirthStateID", requestModel.Sender.BirthStateID),
                    new SqlParameter("@senderBirthCityID", requestModel.Sender.BirthCityID),
                    new SqlParameter("@senderBirthCity", requestModel.Sender.BirthCity),
                    new SqlParameter("@senderCountryOfResidence", requestModel.Sender.CountryOfResidence),

                    new SqlParameter("@senderTaxID", requestModel.Sender.TaxID),
                    new SqlParameter("@senderDoesNotHaveATaxID", requestModel.Sender.DoesNotHaveATaxID),
                    new SqlParameter("@senderCurp", requestModel.Sender.Curp),
                    new SqlParameter("@senderOccupationID", requestModel.Sender.OccupationID),
                    new SqlParameter("@senderOccupation", requestModel.Sender.Occupation),

                    new SqlParameter("@senderIDType", requestModel.Sender.IDType),
                    new SqlParameter("@senderIDTypeID", requestModel.Sender.IDTypeID),
                    new SqlParameter("@senderIDNo", requestModel.Sender.IDNumber),
                    new SqlParameter("@senderIDExpDate", sendIDExp), 
                    new SqlParameter("@senderIDIssuedDate", sendIDIss), 
                    new SqlParameter("@senderIDIssuedByCountry", requestModel.Sender.IDIssuedByCountry),
                    new SqlParameter("@senderIDIssuedByState", requestModel.Sender.IDIssuedByState),
                    new SqlParameter("@senderIDIssuedByStateID", requestModel.Sender.IDIssuedByStateID),
                    new SqlParameter("@senderIDIssuedBy", requestModel.Sender.IDIssuedBy),

                    new SqlParameter("@benName_Full", requestModel.Beneficiary.Name),
                    new SqlParameter("@benName_First", requestModel.Beneficiary.FirstName),
                    new SqlParameter("@benName_Mid", requestModel.Beneficiary.MiddleName),
                    new SqlParameter("@benName_Last1", requestModel.Beneficiary.LastName1),
                    new SqlParameter("@benName_Last2", requestModel.Beneficiary.LastName2),

                    new SqlParameter("@benID_Internal", requestModel.Beneficiary.BenInternalID),
                    new SqlParameter("@benID_External", requestModel.Beneficiary.BenExternalID),

                    new SqlParameter("@benAddress", requestModel.Beneficiary.Address),
                    new SqlParameter("@benCity", requestModel.Beneficiary.City),
                    new SqlParameter("@benDistrict", requestModel.Beneficiary.District),
                    new SqlParameter("@benCounty", requestModel.Beneficiary.County),
                    new SqlParameter("@benState", requestModel.Beneficiary.State),
                    new SqlParameter("@benPostalCode", requestModel.Beneficiary.PostalCode),
                    new SqlParameter("@benCountry", requestModel.Beneficiary.Country),
                    new SqlParameter("@benStateID", requestModel.Beneficiary.StateID),
                    new SqlParameter("@benCityID", requestModel.Beneficiary.CityID),
                    new SqlParameter("@benTelNo", requestModel.Beneficiary.PhoneNumber),
                    new SqlParameter("@benGender", requestModel.Beneficiary.Gender),

                    new SqlParameter("@benDateOfBirth", benDOB),
                    new SqlParameter("@benNationality", requestModel.Beneficiary.Nationality),
                    new SqlParameter("@benCountryOfBirth", requestModel.Beneficiary.CountryOfBirth),
                    new SqlParameter("@benBirthStateID", requestModel.Beneficiary.BirthStateID),
                    new SqlParameter("@benBirthCityID", requestModel.Beneficiary.BirthCityID),
                    new SqlParameter("@benBirthCity", requestModel.Beneficiary.BirthCity),
                    new SqlParameter("@benCountryOfResidence", requestModel.Beneficiary.CountryOfResidence),
                    new SqlParameter("@benTaxID", requestModel.Beneficiary.TaxID),
                    new SqlParameter("@benDoesNotHaveATaxID", requestModel.Beneficiary.DoesNotHaveATaxID),
                    new SqlParameter("@benCurp", requestModel.Beneficiary.Curp),
                    new SqlParameter("@benOccupationID", requestModel.Beneficiary.OccupationID),
                    new SqlParameter("@benOccupation", requestModel.Beneficiary.Occupation),

                    new SqlParameter("@benIDType", requestModel.Beneficiary.IDType),
                    new SqlParameter("@benIDTypeID", requestModel.Beneficiary.IDTypeID),
                    new SqlParameter("@benIDNo", requestModel.Beneficiary.IDNumber),
                    new SqlParameter("@benIDExpDate", benIDExp),
                    new SqlParameter("@benIDIssuedDate", benIDIss),
                    new SqlParameter("@benIDIssuedByCountry", requestModel.Beneficiary.IDIssuedByCountry),
                    new SqlParameter("@benIDIssuedByState", requestModel.Beneficiary.IDIssuedByState),
                    new SqlParameter("@benIDIssuedByStateID", requestModel.Beneficiary.IDIssuedByStateID),
                    new SqlParameter("@benIDIssuedBy", requestModel.Beneficiary.IDIssuedBy),
                    new SqlParameter("@benCustRelationshipID", requestModel.Beneficiary.CustRelationshipID),
                    new SqlParameter("@benCustRelationship", requestModel.Beneficiary.CustRelationship),
                    new SqlParameter("@benTransferReasonID", requestModel.TransferReasonID),
                    new SqlParameter("@benTransferReason", requestModel.TransferReason),
                    new SqlParameter("@BenTax_Amount", requestModel.Beneficiary.TaxAmount)
                },
                Shaper = reader => PayoutConfirmExternalValidateResponse(reader)
            };
            log = LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (ValidatePayoutExternal): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (ValidatePayoutExternal): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (ValidatePayoutExternal): " + e.Message);
            }
        }


        private static PayoutTransactionResponseModel PayoutConfirmExternalValidateResponse(IDataReader reader)
        {
            //Initialize  ResultInfo with 1° result (rst_Result)
            var validateResponse = InitializePayoutConfirmExternalValidateResponseWithResult(reader);
            if (validateResponse == null)
            {
                return null;
            }
            
            //Fill  PayoutFields  with 2° result (rst_PayoutFields)
            reader.NextResult();
            SetPayoutConfirmExternalValidateResponseFields(reader, validateResponse);

            return validateResponse;
        }

        private static PayoutTransactionResponseModel InitializePayoutConfirmExternalValidateResponseWithResult(IDataReader reader)
        {
            return new PayoutTransactionResponseModel()
            {
                ReturnInfo = new ReturnInfoModel()
                {
                    ErrorCode = reader.ReadValue<int>("ErrorCode"),
                    ErrorMessage = reader.ReadValue<string>("ErrorMessage"),
                }
            };
        }

        private static void SetPayoutConfirmExternalValidateResponseFields(IDataReader reader, PayoutTransactionResponseModel validateResponse)
        {
            while (reader.Read())
            {
                validateResponse.PayoutRequiredFields.Add(new PayoutFieldsModel()
                {
                    FieldID = reader.ReadValue<int>("FieldID"),
                    FieldName = reader.ReadValue<string>("FieldName"),
                    DisplayName = reader.ReadValue<string>("DisplayName"),
                    FieldRequired = reader.ReadValue<bool>("FieldRequired"),
                    DataExists = reader.ReadValue<bool>("DataExists"),
                    DataInvalid = reader.ReadValue<bool>("DataInvalid"),
                    NoteOnData = reader.ReadValue<string>("NoteOnData"),
                    DataErrorCode = reader.ReadValue<string>("DataErrCode"),
                });
            }
        }
        #endregion
        
        #region PayoutConfirmComplianceWatchlist
        public PayoutTransactionResponseModel PayoutConfirmComplianceWatchlist(PayoutTransactionRequestModel requestModel,ref string log)
        {
            //Manage SQL dates
            DateTime localtime = GetValidDateTime(requestModel.RequesterInfo.LocalTime);
            DateTime sendDOB = GetValidDateTime(requestModel.Sender.DateOfBirth);
            DateTime sendIDExp = GetValidDateTime(requestModel.Sender.IDExpDate);
            DateTime sendIDIss = GetValidDateTime(requestModel.Sender.IDIssuedDate);
            DateTime benDOB = GetValidDateTime(requestModel.Beneficiary.DateOfBirth);
            DateTime benIDExp = GetValidDateTime(requestModel.Beneficiary.IDExpDate);
            DateTime benIDIss = GetValidDateTime(requestModel.Beneficiary.IDIssuedDate);
            decimal totalAmt = requestModel.SendAmount + requestModel.SendCharge;


            var persistenceEvent = requestModel.Persistence.PersistenceEvents.FirstOrDefault(ev => ev.PersistenceEventTypeID == Shared.Persistence.Model.PersistenceEventType.PayoutPinRequestInfoResponse);
            var transactionInfo = persistenceEvent.GetPersistenceObject<GetTransactionInfoResponseModel>();

            var locInfo = GetLocInfoForExternalLocNo(new LocInfoForExternaLocNlRequestModel()
            {
                AppID = requestModel.RequesterInfo.AppID,
                AppObjectID = requestModel.RequesterInfo.AppObjectID,
                UserNameID = requestModel.RequesterInfo.UserID,
                CorrespID = 7884114, //Golden Crown CorrespID: 7884114
                RiaLocIDFromClient = 0,
                ExternalLocNo = ""
            });
            var request = new DatabaseRequest<PayoutTransactionResponseModel>
            {
                ProcedureName = StoreProcedureConstants.PayoutConfirmComplianceWatchList,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lUserNameID", requestModel.RequesterInfo.UserID),
                    new SqlParameter("@UserLocale", requestModel.RequesterInfo.Locale),
                    new SqlParameter("@lServiceID", 111), //Means MT
                    new SqlParameter("@lProductID", 0),
                    new SqlParameter("@lTransactionID", requestModel.OrderID),
                    new SqlParameter("@TransactionRefNo", requestModel.OrderPIN),
                    new SqlParameter("@lRunTimeTypeID", 2), //TODO: mail from Dipak Jun 24, 2016, at 19:42 (Payout=2)
                    new SqlParameter("@TransDate", localtime),
                    new SqlParameter("@CountryFrom", transactionInfo.CountryFrom),
                    new SqlParameter("@CountryTo", transactionInfo.CountryTo),
                    new SqlParameter("@SendAgentID", 7884114), //Golden Crown: 7884114
                    new SqlParameter("@SendAgentLocID", locInfo.RiaInternalLocID), //TODO: @RiaInternalLocID --> Call sp: mt_sp_GetLocInfoForExternalLocNo "FromAgentID": "AG00SYS" (@ExternalLocNo), 7884114 (@CorrespID)
                    new SqlParameter("@PayoutAgentID", requestModel.RequesterInfo.AgentID),
                    new SqlParameter("@PayoutAgentLocID", requestModel.RequesterInfo.AgentLocID),
                    new SqlParameter("@DeliveryMethodID", 1), //Office PickUp: 1, BankDeposit: 2
                    new SqlParameter("@EntryTypeID", 20), //select  * from [dbo].[systblConst2] where fkey1=7352
                    new SqlParameter("@SendCurrency",  transactionInfo.PayoutAmount.CurrencyCode),
                    new SqlParameter("@SendAmount", transactionInfo.PayoutAmount.Amount),
                    new SqlParameter("@SendTotal", transactionInfo.PayoutAmount.Amount),
                    new SqlParameter("@PayoutCurrency", transactionInfo.PayoutAmount.CurrencyCode),
                    new SqlParameter("@PayoutAmount", transactionInfo.PayoutAmount.Amount),
                    new SqlParameter("@SenderID", requestModel.Sender.CustomerInternalID),
                    new SqlParameter("@SenderFullName", GetValidString(requestModel.Sender.Name)),
                    new SqlParameter("@SenderFirstName", GetValidString(requestModel.Sender.FirstName)),
                    new SqlParameter("@SenderMiddleName", GetValidString(requestModel.Sender.MiddleName)),
                    new SqlParameter("@SenderLastName1", GetValidString(requestModel.Sender.LastName1)),
                    new SqlParameter("@SenderLastName2",GetValidString( requestModel.Sender.LastName2)),
                    new SqlParameter("@SenderCountry", requestModel.Sender.Country),
                    new SqlParameter("@SenderState", requestModel.Sender.State),
                    new SqlParameter("@SenderDateofBirth", sendDOB),
                    new SqlParameter("@SenderCountryofBirth", requestModel.Sender.CountryOfBirth),
                    new SqlParameter("@SenderNationality", requestModel.Sender.Nationality),
                    new SqlParameter("@SenderReasonForTransferID", ""), //Golden Crown is not sending: mail from  Alla Shelest 24-06-2016 7:50. "They don’t have Transfer Reason Option on their API"
                    new SqlParameter("@SenderID_OnBehalfOf",DBNull.Value), //mail from John 24-06-2016 4:04  "If you don't have them, leave them null."
                    new SqlParameter("@SenderFullName_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderFirstName_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderMiddleName_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderLastName1_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderLastName2_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderCountry_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderState_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderDateofBirth_OnBehalfOf", sendDOB),
                    new SqlParameter("@SenderCountryofBirth_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderNationality_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@SenderReasonForTransferID_OnBehalfOf", DBNull.Value),
                    new SqlParameter("@BenNameID", requestModel.Beneficiary.BenInternalID),
                    new SqlParameter("@BenFullName", GetValidString(requestModel.Beneficiary.Name)),
                    new SqlParameter("@BenFirstName", GetValidString(requestModel.Beneficiary.FirstName)),
                    new SqlParameter("@BenMiddleName", GetValidString(requestModel.Beneficiary.MiddleName)),
                    new SqlParameter("@BenLastName1", GetValidString(requestModel.Beneficiary.LastName1)),
                    new SqlParameter("@BenLastName2", GetValidString(requestModel.Beneficiary.LastName2)),
                    new SqlParameter("@BenCountry", requestModel.Beneficiary.Country),
                    new SqlParameter("@BenState", requestModel.Beneficiary.State),
                    new SqlParameter("@BenDateofBirth", benDOB),
                    new SqlParameter("@BenCountryofBirth", requestModel.Beneficiary.CountryOfBirth),
                    new SqlParameter("@BenNationality", requestModel.Beneficiary.Nationality),
                    new SqlParameter("@BenReasonForTransferID", requestModel.TransferReasonID),
                    new SqlParameter("@bRecordIssues", 1),  //write issue
                    new SqlParameter("@bRunOnLinkedServer",false),  //Not run on Liked Server
                },
                Shaper = reader => PayoutConfirmComplianceWatchlistResponse(reader)
            };
                log = LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (ComplianceWatchlist): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (ComplianceWatchlist): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (ComplianceWatchlist): " + e.Message);
            }
        }

        private static PayoutTransactionResponseModel PayoutConfirmComplianceWatchlistResponse(IDataReader reader)
        {
            //Initialize  ResultInfo with 1° result (rst_Result)
            var payoutResponse = InitializePayoutConfirmComplianceWatchlistResponseWithResult(reader);
            if (payoutResponse == null)
            {
                return null;
            }

           

            return payoutResponse;
        }

        private static PayoutTransactionResponseModel InitializePayoutConfirmComplianceWatchlistResponseWithResult(IDataReader reader)
        {
            
            var response = new PayoutTransactionResponseModel(0, string.Empty,0);
            response.ActionID = reader.ReadValue<int>("ActionID", true);

            // 0 - Ok
            //20 - Log Only
            var okStatus = new List<int>() { 0, 20 };
            response.IssuesFound = !okStatus.Contains(response.ActionID);

            if (response.IssuesFound)
            {
                response.ReturnInfo = new ReturnInfoModel()
                {
                    ErrorCode = 116,
                    ErrorMessage = string.Format("{0} {1}", response.ActionID, reader.ReadValue<string>("ActionDescription")),                    
                };                            
            }

            return response;
        }

        #endregion

        #region OrderCreateFromProviderData
        public CreateOrderFromProviderDataResponse OrderCreateFromProviderData(PayoutTransactionRequestModel requestModel)
        {
            var request = new DatabaseRequest<CreateOrderFromProviderDataResponse>
            {
                ProcedureName = StoreProcedureConstants.OrderCreateFromProviderData,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lUserNameID", requestModel.RequesterInfo.UserID),
                    new SqlParameter("@sLocale", requestModel.RequesterInfo.Locale),
                    new SqlParameter("@SendAgentID", requestModel.RecAgentID),
                    new SqlParameter("@SendAgentLocID", requestModel.RecAgentLocID),
                    new SqlParameter("@SendAgentBranchNo", requestModel.RecAgentBranch),
                    new SqlParameter("@SendAgentAddress", requestModel.RecAgentAddress),
                    new SqlParameter("@SendAgentCity", requestModel.RecAgentCity),
                    new SqlParameter("@SendAgentState", requestModel.RecAgentState),
                    new SqlParameter("@SendAgentPostalCode", requestModel.RecAgentPostalCode),
                    new SqlParameter("@SendAgentCountry", requestModel.RecAgentCountry),
                    new SqlParameter("@PayoutAgentID", requestModel.PayAgentID),
                    new SqlParameter("@PayoutAgentLocID", requestModel.PayAgentLocID),
                    new SqlParameter("@PayoutAgentAddress", requestModel.PayAgentBranch),
                    new SqlParameter("@PayoutAgentState", requestModel.PayAgentState),
                    new SqlParameter("@PayoutAgentPostalCode", requestModel.PayAgentPostalCode),
                    new SqlParameter("@PayoutAgentCountry", requestModel.PayAgentCountry),
                    new SqlParameter("@Payout_LocalTime", requestModel.RequesterInfo.LocalTime),
                    new SqlParameter("@OrderLookupCode", requestModel.OrderLookupCode),
                    new SqlParameter("@lPersistenceID", requestModel.PersistenceID),
                    new SqlParameter("@PIN", requestModel.OrderPIN),
                    new SqlParameter("@SendCurrency", requestModel.SendCurrency),
                    new SqlParameter("@SendAmount", requestModel.SendAmount),
                    new SqlParameter("@SendCharge", requestModel.SendCharge),
                    new SqlParameter("@SettlementCurrency", requestModel.PayoutCurrency),//TODO: Get this?
                    new SqlParameter("@SettlementAmount", requestModel.PayoutAmount),//TODO: Get this?
                    new SqlParameter("@SettlementCharge", requestModel.SendCharge),//TODO: Get this?
                    new SqlParameter("@PayoutCurrency", requestModel.PayoutCurrency),
                    new SqlParameter("@PayoutAmount", requestModel.PayoutAmount),
                    new SqlParameter("@PayoutCountry", requestModel.PayAgentCountry),
                    new SqlParameter("@PayoutState", requestModel.PayAgentState),
                    new SqlParameter("@SenderInternalID", requestModel.Sender.CustomerInternalID),
                    new SqlParameter("@SenderExternalNo", requestModel.Sender.CustomerExternalID),
                    new SqlParameter("@SenderNameFirst", requestModel.Sender.FirstName),
                    new SqlParameter("@SenderNameMid", requestModel.Sender.MiddleName),
                    new SqlParameter("@SenderNameLast1", requestModel.Sender.LastName1),
                    new SqlParameter("@SenderNameLast2", requestModel.Sender.LastName2),
                    new SqlParameter("@SenderTelNo", requestModel.Sender.PhoneNumber),
                    new SqlParameter("@SenderAddress", requestModel.Sender.Address),
                    new SqlParameter("@SenderCity", requestModel.Sender.City),
                    new SqlParameter("@SenderState", requestModel.Sender.State),
                    new SqlParameter("@SenderPostalCode", requestModel.Sender.PostalCode),
                    new SqlParameter("@SenderCountry", requestModel.Sender.Country),
                    new SqlParameter("@BenInternalNameID", requestModel.Beneficiary.BenInternalID),
                    new SqlParameter("@BenInternalID", requestModel.Beneficiary.BenInternalID),
                    new SqlParameter("@BenExternalNo", requestModel.Beneficiary.BenExternalID),
                    new SqlParameter("@BenNameFirst", requestModel.Beneficiary.FirstName),
                    new SqlParameter("@BenNameMid", requestModel.Beneficiary.MiddleName),
                    new SqlParameter("@BenNameLast1", requestModel.Beneficiary.LastName1),
                    new SqlParameter("@BenNameLast2", requestModel.Beneficiary.LastName2),
                    new SqlParameter("@BenTelNo", requestModel.Beneficiary.PhoneNumber),
                    new SqlParameter("@BenAddress", requestModel.Beneficiary.Address),
                    new SqlParameter("@BenCity", requestModel.Beneficiary.City),
                    new SqlParameter("@BenState", requestModel.Beneficiary.State),
                    new SqlParameter("@BenZip", requestModel.Beneficiary.PostalCode),
                    new SqlParameter("@BenCountry", requestModel.Beneficiary.Country),
                },
                Shaper = reader => OrderCreateProviderResponse(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (OrderCreateFromProviderData): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (OrderCreateFromProviderData): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (OrderCreateFromProviderData): " + e.Message);
            }
        }

     
        private static CreateOrderFromProviderDataResponse OrderCreateProviderResponse(IDataReader reader)
        {
            
            return new CreateOrderFromProviderDataResponse()
            {
                ReturnValue = reader.ReadValue<int>("retVal"),
                ReturnMessage = reader.ReadValue<string>("retMsg"),
                TransactionID = reader.ReadValue<long>("transactionID")
            };
          
        }
        #endregion
        
        #region SaveTransactionAsPaid
        public PayoutTransactionResponseModel SaveTransactionAsPaid(PayoutTransactionRequestModel requestModel)
        {
            var request = new DatabaseRequest<PayoutTransactionResponseModel>
            {
                ProcedureName = StoreProcedureConstants.SaveTransactionAsPaid,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lAgentID", requestModel.PayAgentID),
                    new SqlParameter("@lAgentLocID", requestModel.PayAgentLocID),
                    new SqlParameter("@lUserNameID", requestModel.RequesterInfo.UserID),
                    new SqlParameter("@lUserLoginID", requestModel.RequesterInfo.UserLoginID),
                    new SqlParameter("@sLocale", requestModel.RequesterInfo.Locale),
                    new SqlParameter("@TellerDrawerInstanceID", requestModel.TellerDrawerInstanceID),
                    new SqlParameter("@lOrderID", requestModel.OrderID),
                    new SqlParameter("@OrderLookupCode", requestModel.OrderLookupCode),
                    new SqlParameter("@ProviderID", requestModel.ProviderID),
                    new SqlParameter("@PersistenceID", requestModel.PersistenceID),
                    new SqlParameter("@AgentCountry", requestModel.PayAgentCountry),
                    new SqlParameter("@lAgentCountryID", requestModel.PayAgentCountryID),
                    new SqlParameter("@AgentState", requestModel.PayAgentState),
                    new SqlParameter("@AgentCity", requestModel.PayAgentCity),
                    new SqlParameter("@Payout_LocalTime", requestModel.RequesterInfo.LocalTime),
                    new SqlParameter("@Payout_TimeZone", requestModel.RequesterInfo.Timezone),
                    new SqlParameter("@Payout_TimeZoneID", requestModel.RequesterInfo.TimezoneID),
                    new SqlParameter("@Payout_MethodID", requestModel.PayoutMethodID),
                    new SqlParameter("@sBenName", requestModel.Beneficiary.Name),
                    new SqlParameter("@benAddress", requestModel.Beneficiary.Address),
                    new SqlParameter("@benCity", requestModel.Beneficiary.City),
                    new SqlParameter("@benDistrict", requestModel.Beneficiary.District),
                    new SqlParameter("@benCounty", requestModel.Beneficiary.County),
                    new SqlParameter("@benState", requestModel.Beneficiary.State),
                    new SqlParameter("@benPostalCode", requestModel.Beneficiary.PostalCode),
                    new SqlParameter("@benCountry", requestModel.Beneficiary.Country),
                    new SqlParameter("@benStateID", requestModel.Beneficiary.StateID),
                    new SqlParameter("@benCityID", requestModel.Beneficiary.CityID),
                    new SqlParameter("@benTelNo", requestModel.Beneficiary.PhoneNumber),
                    new SqlParameter("@benGender", requestModel.Beneficiary.Gender),
                    new SqlParameter("@benDateOfBirth", requestModel.Beneficiary.DateOfBirth),
                    new SqlParameter("@benNationality", requestModel.Beneficiary.Nationality),
                    new SqlParameter("@benCountryOfBirth", requestModel.Beneficiary.CountryOfBirth),
                    new SqlParameter("@benBirthStateID", requestModel.Beneficiary.BirthStateID),
                    new SqlParameter("@benBirthCityID", requestModel.Beneficiary.BirthCityID),
                    new SqlParameter("@benBirthCity", requestModel.Beneficiary.BirthCity),
                    new SqlParameter("@benCountryOfResidence", requestModel.Beneficiary.CountryOfResidence),
                    new SqlParameter("@benTaxID", requestModel.Beneficiary.TaxID),
                    new SqlParameter("@benDoesNotHaveATaxID", requestModel.Beneficiary.DoesNotHaveATaxID),
                    new SqlParameter("@benCurp", requestModel.Beneficiary.Curp),
                    new SqlParameter("@benOccupationID", requestModel.Beneficiary.OccupationID),
                    new SqlParameter("@benOccupation", requestModel.Beneficiary.Occupation),
                    new SqlParameter("@benIDType", requestModel.Beneficiary.IDType),
                    new SqlParameter("@benIDTypeID", requestModel.Beneficiary.IDTypeID),
                    new SqlParameter("@benIDNo", requestModel.Beneficiary.IDNumber),
                    new SqlParameter("@benIDExpDate", requestModel.Beneficiary.IDExpDate),
                    new SqlParameter("@benIDIssuedDate", requestModel.Beneficiary.IDIssuedDate),
                    new SqlParameter("@benIDIssuedByCountry", requestModel.Beneficiary.IDIssuedByCountry),
                    new SqlParameter("@benIDIssuedByState", requestModel.Beneficiary.IDIssuedByState),
                    new SqlParameter("@benIDIssuedByStateID", requestModel.Beneficiary.IDIssuedByStateID),
                    new SqlParameter("@benIDIssuedBy", requestModel.Beneficiary.IDIssuedBy),
                    new SqlParameter("@benCustRelationshipID", requestModel.Beneficiary.CustRelationshipID),
                    new SqlParameter("@benCustRelationship", requestModel.Beneficiary.CustRelationship),
                    new SqlParameter("@benTransferReasonID", requestModel.TransferReasonID),
                    new SqlParameter("@benTransferReason", requestModel.TransferReason),
                    new SqlParameter("@BenTax_Amount", requestModel.Beneficiary.TaxAmount),
                },
                Shaper = reader => SaveTransactionAsPaidResponse(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (SaveTransactionPaid): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (SaveTransactionPaid): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (SaveTransactionPaid): " + e.Message);
            }
        }


        private static PayoutTransactionResponseModel SaveTransactionAsPaidResponse(IDataReader reader)
        {
            //Initialize  ResultInfo with 1° result (rst_Result)
            var validateResponse = InitializeSaveTransactionAsPaidResponseWithResult(reader);
            if (validateResponse == null)
            {
                //TODO: A lot of these can return null. Maybe set an error code instead?
                return null;
            }
            return validateResponse;
        }

        private static PayoutTransactionResponseModel InitializeSaveTransactionAsPaidResponseWithResult(IDataReader reader)
        {
            return new PayoutTransactionResponseModel()
            {
                ReturnInfo = new ReturnInfoModel()
                {
                    ErrorCode = reader.ReadValue<int>("ErrorCode"),
                    ErrorMessage = reader.ReadValue<string>("ErrorMessage"),
                }
            };
        }
        #endregion

        #region ComplianceMatchRulesGet
        /// <summary>
        /// Currently just using this call to determine if the compliance will be done on Ria systems
        /// or on Actimize systems.
        /// Returns true if compliance should be done on Ria systems.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public ComplianceProviderResponseModel GetComplianceProvider(PayoutTransactionRequestModel requestModel)
        {
            var complianceMatchRules = ComplianceMatchRulesGet(requestModel);

            if (complianceMatchRules == null || complianceMatchRules.Count()==0) { return  new ComplianceProviderResponseModel(); }

            return new ComplianceProviderResponseModel()
            {
                UseRiaCompliance = complianceMatchRules.Where(r=>r.ProviderName=="RIA").Count()>0,
                UseActimizeCompliance = complianceMatchRules.Where(r => r.ProviderName == "Actimize").Count() > 0,
            };
        }
        public IEnumerable<WLFMatchRuleModel> ComplianceMatchRulesGet(PayoutTransactionRequestModel requestModel)
        {
            decimal totalAmt = requestModel.SendAmount + requestModel.SendCharge;

            var countries = GetAllCountries(0);
            int countryFromID = 0;
            int countryToID = 0;
            if (countries != null)
            {
                var countryTo = countries.FirstOrDefault(c => c.Abbrev == requestModel.RecAgentCountry);
                var countryFrom = countries.FirstOrDefault(c => c.Abbrev == requestModel.PayAgentCountry);

                if (countryTo!=null){ countryToID = countryTo.CountryID;}
                if (countryFrom != null){ countryFromID = countryFrom.CountryID;}
            }

            var request = new DatabaseRequest<WLFMatchRuleModel>
            {
                ProcedureName = StoreProcedureConstants.ComplianceMatchRulesGet,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@lAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@lAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lUserNameID", requestModel.RequesterInfo.UserID),
                    new SqlParameter("@TransDate", requestModel.RequesterInfo.LocalTime),//TODO: Get this value. TransTime not request Time.
                    new SqlParameter("@lRunTimeID", 2),//Payout
                    new SqlParameter("@lServiceID", 111),
                    new SqlParameter("@lProductID", 0),
                    new SqlParameter("@lCountryFromID", countryFromID),
                    new SqlParameter("@lCountryToID", countryToID),
                    new SqlParameter("@lRecAgentID", requestModel.RecAgentID),
                    new SqlParameter("@lRecAgentLocID", requestModel.RecAgentLocID),
                    new SqlParameter("@lPayAgentID", requestModel.PayAgentID),
                    new SqlParameter("@lPayAgentLocID", requestModel.PayAgentLocID),
                    new SqlParameter("@lDeliveryMethodID", requestModel.PayoutMethodID),
                    new SqlParameter("@lEntryTypeID", 7),//select  * from [dbo].[systblConst2] where fkey1=7352
                    new SqlParameter("@sCurrency", requestModel.SendCurrency),
                    new SqlParameter("@mOrderAmount", requestModel.SendAmount),
                    new SqlParameter("@mTotalAmount", totalAmt),
                },
                Shaper = reader => ComplianceMatchRulesGetResponse(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return GetList(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (ComplianceMatchRulesGet): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (ComplianceMatchRulesGet): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (ComplianceMatchRulesGet): " + e.Message);
            }
        }

        private static WLFMatchRuleModel ComplianceMatchRulesGetResponse(IDataReader reader)
        {
            return new WLFMatchRuleModel()
            {
                ProvideID=  reader.ReadValue<int>("ProviderID"),
                ProviderName = reader.ReadValue<string>("ProviderName"),
                RuleID = reader.ReadValue<int>("fRuleID"),
                ActionID = reader.ReadValue<int>("fActionID"),
                NameTypeID = reader.ReadValue<int>("fNameTypeID"),
                MatchTypeID = reader.ReadValue<int>("fMatchTypeID"),
                IssueItemID = reader.ReadValue<int>("fIssueItemID"),
            };
          
           
          
        }
        #endregion

        #region ComplianceWriteIssue
        public PayoutTransactionResponseModel ComplianceWriteIssue(PayoutTransactionRequestModel requestModel, string issueDesc)
        {
            decimal totalAmt = requestModel.SendAmount + requestModel.SendCharge;

            var request = new DatabaseRequest<PayoutTransactionResponseModel>
            {
                ProcedureName = StoreProcedureConstants.ComplianceWriteReviewIssue,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    //new SqlParameter("@lLogID", -1),//TODO: Get this value
                    new SqlParameter("@lLogTypeID", -1),//TODO: Get this value
                    new SqlParameter("@lServiceID", 111),
                    new SqlParameter("@lTransID", requestModel.OrderID),
                    new SqlParameter("@dLogDate", DateTime.Now),//TODO: Get this value
                    new SqlParameter("@sDescription", issueDesc),
                    new SqlParameter("@lStatusID", -1),//TODO: Get this value
                    new SqlParameter("@lReviewedByID", -1),//TODO: Get this value
                    new SqlParameter("@dReviewedByDate", DateTime.Now),//TODO: Get this value
                    new SqlParameter("@lUserID", requestModel.RequesterInfo.UserLoginID),
                    new SqlParameter("@bCreateReviewIssue", true),//TODO: Get this value
                    new SqlParameter("@lItemID", -1),//TODO: Get this value
                    new SqlParameter("@sReviewIssueDescription", issueDesc),
                    new SqlParameter("@lIssueTypeID", -1),//TODO: Get this value
                    new SqlParameter("@lIssueItemID", -1),//TODO: Get this value
                    new SqlParameter("@lNoteID", ""),//TODO: Get this value
                    new SqlParameter("@sNote", ""),//TODO: Get this value
                    //new SqlParameter("@lRetVal", -1),//TODO: Ouput var
                    //new SqlParameter("@sRetMsg", ""),//TODO: Ouput var
                    new SqlParameter("@bSendEmail", false),//TODO: Get this value
                    new SqlParameter("@fAppID", requestModel.RequesterInfo.AppID),
                    new SqlParameter("@fAppObjectID", requestModel.RequesterInfo.AppObjectID),
                    new SqlParameter("@bDebug", false),
                }
                .AddIntOut("@lRetVal")
                .AddVarCharOut("@sRetMsg", 250)
                .AddIntOut("@lLogID")
                ,
                Shaper = reader => ComplianceWriteIssueResponse(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (ComplianceWriteIssue): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (ComplianceWriteIssue): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (ComplianceWriteIssue): " + e.Message);
            }
        }


        private static PayoutTransactionResponseModel ComplianceWriteIssueResponse(IDataReader reader)
        {
            //Initialize  ResultInfo with 1° result (rst_Result)
            var validateResponse = InitializeComplianceWriteIssueResponseWithResult(reader);
            if (validateResponse == null)
            {
                return null;
            }

            //Fill  PayoutFields  with 2° result (rst_PayoutFields)
            reader.NextResult();
            SetComplianceWriteIssueResponseFields(reader, validateResponse);

            return validateResponse;
        }

        private static PayoutTransactionResponseModel InitializeComplianceWriteIssueResponseWithResult(IDataReader reader)
        {
            return new PayoutTransactionResponseModel()
            {
                ReturnInfo = new ReturnInfoModel()
                {
                    ErrorCode = reader.ReadValue<int>("ErrorCode"),
                    ErrorMessage = reader.ReadValue<string>("ErrorMessage"),
                }
            };
        }

        private static void SetComplianceWriteIssueResponseFields(IDataReader reader, PayoutTransactionResponseModel validateResponse)
        {
            while (reader.Read())
            {
                validateResponse.PayoutRequiredFields.Add(new PayoutFieldsModel()
                {
                    FieldID = reader.ReadValue<int>("FieldID"),
                    FieldName = reader.ReadValue<string>("FieldName"),
                    DisplayName = reader.ReadValue<string>("DisplayName"),
                    FieldRequired = reader.ReadValue<bool>("FieldRequired"),
                    DataExists = reader.ReadValue<bool>("DataExists"),
                    DataInvalid = reader.ReadValue<bool>("DataInvalid"),
                    NoteOnData = reader.ReadValue<string>("NoteOnData"),
                    DataErrorCode = reader.ReadValue<string>("DataErrCode"),
                });
            }
        }
        #endregion

        #region ComplianceListIssues
        public IEnumerable<IssueModel> ComplianceListIssues(PayoutTransactionRequestModel requestModel)
        {
            
            var request = new DatabaseRequest<IssueModel>
            {
                ProcedureName = StoreProcedureConstants.ComplianceListIssues,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fAppID",ConfigSettings.CoreAPIAppID()),
                    new SqlParameter("@fAppObjectID",ConfigSettings.CoreAPIAppObjectID()),
                    new SqlParameter("@lUserNameID",requestModel.RequesterInfo.UserID),
                    new SqlParameter("@fTransactionID", requestModel.OrderID)                  
                },
                Shaper = reader => ComplianceListIssuesResponse(reader)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return GetList(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: (ComplianceListIssues): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (ComplianceListIssues): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (ComplianceListIssues): " + e.Message);
            }
        }

        private static IssueModel ComplianceListIssuesResponse(IDataReader reader)
        {
            var issue = new IssueModel()
            {
                LogID = reader.ReadValue<int>("fLogID"),
                ItemID = reader.ReadValue<int>("fItemID"),
                IssueTypeID = reader.ReadValue<int>("fIssueTypeID"),
                IssueItemID = reader.ReadValue<int>("fIssueItemID"),
                LogDate = reader.ReadValue<DateTime>("fLogDate"),
                ReviewedDate = reader.ReadValue<DateTime>("fReviewedDate"),
                IssueType = reader.ReadValue<string>("sIssueType"),
                ReviewedByName = reader.ReadValue<string>("ReviewedByName"),
                Status = reader.ReadValue<string>("Status"),
                Description = reader.ReadValue<string>("fDesc"),
                PermVal = reader.ReadValue<string>("PermVal"),
                FilterType= reader.ReadValue<int>("fFilterType"),
                StatusID = reader.ReadValue<int>("fStatusID"),
            };          

            return issue;
        }
        #endregion

        #region Email
        public SendEmailResponseModel EnviarMail(SendEmailRequestModel requestModel, ref string log)
        {

            var request = new DatabaseRequest<SendEmailResponseModel>
            {
                ProcedureName = StoreProcedureConstants.CommServerMessage,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fMessage", requestModel.Message),
                    new SqlParameter("@fMessageType", requestModel.MessageType) ,
                    new SqlParameter("@fMessageFormat", requestModel.MessageFormat) ,
                    new SqlParameter("@fMessageFrom", requestModel.MessageFrom) ,
                    new SqlParameter("@fMessageTo", requestModel.MessageTo) ,
                    new SqlParameter("@fMessageCc", requestModel.MessageCc) ,
                    new SqlParameter("@fMessageBcc", requestModel.MessageBcc) ,
                    new SqlParameter("@fMessageSubject", requestModel.MessageSubject) ,
                    new SqlParameter("@fMessageSendMethod", requestModel.MessageSendMethod) ,
                    new SqlParameter("@fUserNameID", requestModel.UserNameID) 
                }
              .AddIntOut("@fRetVal")
              .AddIntOut("@lRetMessageID"),
               OutputFuncShaper = parameters => GetSendEmailResponse(parameters)

            };
            log = LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database (GetCurrencyCodeData): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (GetCurrencyCodeData): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (GetCurrencyCodeData): " + e.Message);
            }
        }

        private static SendEmailResponseModel GetSendEmailResponse(System.Data.Common.DbParameterCollection parameters)
        {
            //Initialize response instance

            return new SendEmailResponseModel()
            {
                ReturnValue = parameters.ReadValue<int>("@fRetVal"),
                ReturnMessageID = parameters.ReadValue<int>("@lRetMessageID")

            };

        }
        #endregion

        #region LegalHold
        public PlaceOnLegalHoldResponseModel PlaceLegalHoldPayout(PlaceOnLegalHoldRequestModel requestModel, ref string log)
        {
            var request = new DatabaseRequest<PlaceOnLegalHoldResponseModel>
            {
                ProcedureName = StoreProcedureConstants.TransactionPlaceLegalHoldPayout,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@fServiceID", requestModel.ServiceID),
                    new SqlParameter("@fTransID", requestModel.TransactionID) ,
                    new SqlParameter("@sStatusNote", requestModel.StatusNote) ,
                    new SqlParameter("@fUserID", requestModel.UserID) ,
                    new SqlParameter("@fModified", requestModel.Modified) ,
                    new SqlParameter("@fAppID", requestModel.AppID) ,
                    new SqlParameter("@fAppObjectiD", requestModel.AppObjtectID) 
                }
               .AddIntOut("@lRetVal")
               .AddVarCharOut("@sRetMsg",255),
                OutputFuncShaper = parameters => GetPlaceLegalHoldResponse(parameters)

            };
            log = LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database (GetCurrencyCodeData): " + e.Message);
                _logger.PublishInformation("Error Calling Database: (GetCurrencyCodeData): " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: (GetCurrencyCodeData): " + e.Message);
            }
        }

        private static PlaceOnLegalHoldResponseModel GetPlaceLegalHoldResponse(System.Data.Common.DbParameterCollection parameters)
        {
            //Initialize response instance

            return new PlaceOnLegalHoldResponseModel()
            {
                ReturnValue = parameters.ReadValue<int>("@lRetVal"),
                ReturnMessage = parameters.ReadValue<string>("@sRetMsg")

            };

        }
        #endregion

        #region LogSPCall
        /// <summary>
        /// Log the call to the SP with input values.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="cmd"></param>
        private string  LogSPCall(string spName, ICollection<SqlParameter> paramCollect)
        {
            var log = string.Empty;
            try
            {
                //if (ConfigSettings.LogStoredProcedureCalls())
                if (true)
                {
                    //Write out a SQL exec statement for use in testing:
                    string fullExec = "";
                    string sqlCmd = "exec " + spName + " ";
                    string declareStmt = "";
                    string paramList = "";

                    IEnumerator e = paramCollect.GetEnumerator();
                    while (e.MoveNext())
                    {
                        SqlParameter p = (SqlParameter)e.Current;
                        if (p.Direction == ParameterDirection.Output)
                        {
                            if (p.DbType == DbType.Int32)
                            {
                                declareStmt += "declare " + p + " int|";
                            }
                            else if (p.DbType == DbType.String || p.DbType == DbType.AnsiString)
                            {
                                declareStmt += "declare " + p + " varchar(100)|";
                            }
                            else if (p.DbType == DbType.DateTime)
                            {
                                declareStmt += "declare " + p + " datetime|";
                            }
                            else if (p.DbType == DbType.Boolean)
                            {
                                declareStmt += "declare " + p + " bit|";
                            }
                            else
                            {
                                declareStmt += "declare " + p + " " + p.DbType + "|";
                            }
                        }
                    }
                    //Write out the parameter list:
                    e = paramCollect.GetEnumerator();
                    while (e.MoveNext())
                    {
                        SqlParameter p = (SqlParameter)e.Current;
                        if (p.Direction == ParameterDirection.Output)
                        {
                            paramList += p + " = " + p + " output,";
                        }
                        else if (p.DbType == DbType.String
                            || p.DbType == DbType.AnsiString
                            || p.DbType == DbType.DateTime
                            || p.DbType == DbType.Boolean)
                        {
                            paramList += p + " = '" + p.Value + "',";
                        }
                        else
                        {
                            paramList += p + " = " + (p.Value??"null") + ",";
                        }
                    }
                    //Compose the statement:
                    fullExec = declareStmt + sqlCmd + paramList.Remove(paramList.LastIndexOf(","),1);
                    log = fullExec;
                    _logger.PublishInformation(log);                    
                }
            }
            catch (Exception e)
            {
                //Don't want to stop the application process if the SP call cannot be parsed, just log it:
                log = "Cannot parse and write out SP call logging." + e.Message;
                _logger.PublishError(log);
            }

            return log;
           

        }


        #endregion

        #region LocaInfo
        public LocInfoForExternalLocNResponseModel GetLocInfoForExternalLocNo(LocInfoForExternaLocNlRequestModel requestModel)
        {
            var request = new DatabaseRequest<LocInfoForExternalLocNResponseModel>
            {
                ProcedureName = StoreProcedureConstants.GetLocInfoForExternalLocNo,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                   new SqlParameter("@lAppID", requestModel.AppID),
                   new SqlParameter("@lAppObjectID", requestModel.AppObjectID),
                   new SqlParameter("@lUserNameID", requestModel.UserNameID),
                   new SqlParameter("@CorrespID", requestModel.CorrespID),
                   new SqlParameter("@RiaLocID_FromClient", requestModel.RiaLocIDFromClient),
                   new SqlParameter("@ExternalLocNo", requestModel.ExternalLocNo),
                   new SqlParameter("@IgnoreDisabledStatus", requestModel.IgnoreDisabledStatus),
                }
                .AddIntOut("@RiaInternalLocID")
                .AddBitOut("@LocCannotPayOrders")
                .AddBitOut("@LocCannotTakeOrders")
                .AddBitOut("@LocIsOnHold")
                .AddBitOut("@LocIsDisabled")
                .AddBitOut("@LocIsDeleted"),
                OutputFuncShaper = parameters => GetLocInfoExternal(parameters)
            };
            LogSPCall(request.ProcedureName, request.Parameters);
            try
            {
                return Get(request);
            }
            catch (Exception e)
            {
                _logger.PublishError("Error Calling Database: " + e.Message);
                _logger.PublishInformation("Error Calling Database: " + e.Message);//This will show it inline in the trace log as well - easier to track here.
                throw new DataException("Error Calling Database: " + e.Message);
            }
        }

        private static LocInfoForExternalLocNResponseModel GetLocInfoExternal(System.Data.Common.DbParameterCollection parameters)
        {
            return new LocInfoForExternalLocNResponseModel()
            {
                RiaInternalLocID = parameters.ReadValue<int>("@RiaInternalLocID"),
                LocCannotPayOrders = parameters.ReadValue<bool>("@LocCannotPayOrders"),
                LocCannotTakeOrders = parameters.ReadValue<bool>("@LocCannotTakeOrders"),
                LocIsOnHold = parameters.ReadValue<bool>("@LocIsOnHold"),
                LocIsDisabled = parameters.ReadValue<bool>("@LocIsDisabled"),
                LocIsDeleted = parameters.ReadValue<bool>("@LocIsDeleted")
            };
        }

        #endregion

        #region Utils

        /// <summary>
        /// Get a valid Datetime
        /// </summary>
        /// <param name="dateTime">if null or no parameter return min valid Datetime</param>
        /// <returns>Datetime</returns>
        private DateTime GetValidDateTime(DateTime? dateTime = null)
        {
            DateTime minDate = (DateTime)SqlDateTime.MinValue;

            return (dateTime == null || dateTime < minDate) ? minDate : dateTime ?? minDate;

        }

        private string GetValidString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }      
            return value;
        }
        #endregion
    }

}
