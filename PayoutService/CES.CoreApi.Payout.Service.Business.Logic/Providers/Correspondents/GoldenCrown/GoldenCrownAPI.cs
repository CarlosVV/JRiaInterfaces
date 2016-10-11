using AutoMapper;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Service.Business.Logic.Exceptions;
using CES.CoreApi.Payout.Service.Business.Logic.GoldenCrownServiceReference;
using CES.CoreApi.Payout.Service.Business.Logic.Providers.RiaDatabase;
using CES.CoreApi.Payout.Service.Business.Logic.Utilities;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using System;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Shared.Persistence.Interfaces;

using CES.CoreApi.Shared.Persistence.Model;
using CES.CoreApi.Payout.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.GoldenCrown
{

    /// <summary>
    /// Client to the Golden Crown Service for paying out transactions.
    /// </summary>
    class GoldenCrownAPI 
    {
        private static readonly Log4NetProxy _logger = new Log4NetProxy();

        private string m_gcURL = string.Empty;
        private string m_gcClientCertSubject = string.Empty;
        private string m_gcServiceCertSubject = string.Empty;
        private float m_gcInterfaceVersion = 0;
        //private string m_gcRiaAgentID = string.Empty;

        //GC Registration element valid vaules for Type:
        private const string REGCOUNTRY = "REGCOUNTRY"; //Country of registration
        private const string REGCITY = "REGCITY"; //City of registration
        private const string REGADDRESS = "REGADDRESS"; //Address of registration
        private const string GC_EMPTY_COUNTRYCODE = "XXX";

        private readonly IDataHelper m_dataHelper;
        private readonly IPersistenceHelper m_persistenceHelper;
        private readonly IMapper m_mappingHelper;
        private readonly CountryCode m_countryCode;


        public GoldenCrownAPI(ProviderModel providerInfo, IDataHelper dataHelper, IPersistenceHelper persistenceHelper, IMapper mappingHelper) {
            m_gcURL = providerInfo.GetConfiguration<string>(ConfigurationProviderKeys.GoldenCrownServerURL);
            m_gcClientCertSubject = providerInfo.GetConfiguration<string>(ConfigurationProviderKeys.GoldenCrownClientCertSubject);
            m_gcServiceCertSubject = providerInfo.GetConfiguration<string>(ConfigurationProviderKeys.GoldenCrownServiceCertSubject);
            m_gcInterfaceVersion = providerInfo.GetConfiguration<float>(ConfigurationProviderKeys.GoldenCrownInterfaceVersion);
            //m_gcRiaAgentID = providerInfo.GetConfiguration<string>(ConfigurationProviderKeys.GoldenCrownRiaAgentID);
            //_logger.PublishInformation("GC Conn: " + m_gcURL + "|" + m_gcClientCertSubject + "|" + m_gcServiceCertSubject + "|" + m_gcInterfaceVersion + "|" + m_gcRiaAgentID);

            m_mappingHelper= mappingHelper; ;
            m_persistenceHelper = persistenceHelper;
            m_dataHelper = dataHelper;
            m_countryCode = new CountryCode(dataHelper);
        }
        public GoldenCrownAPI()
        {
          //NOOP:
        }

        /// <summary>
        /// GET TRANSACTION INFO:
        /// Searches the Golden Crown data for a transaction based on 
        /// order number, and returns the transaction details.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetTransactionInfoResponseModel GetTransactionInfo(GetTransactionInfoRequestModel request)
        {
            GetTransactionInfoResponseModel tid = new GetTransactionInfoResponseModel();
            ReturnInfoModel ri = new ReturnInfoModel();
            ri.ErrorCode = (int) PayoutMessageCode.Undefined;
            ri.ErrorMessage = "Golden Crown service call did not finish processing.";
            tid.ReturnInfo = ri;
            
            try
            {
                //Format the Request:
                ReqTxList searchTransactionReq = new ReqTxList();
                Filter filter = new Filter();
                filter.OID = request.OrderPIN;
                searchTransactionReq.Filter = filter;
                ExtensionInfo extInfo = new ExtensionInfo();
                extInfo.InterfaceVersionSpecified = true;
                extInfo.InterfaceVersion = m_gcInterfaceVersion;
                searchTransactionReq.ExtensionInfo = extInfo;
                searchTransactionReq.AgentID = request.RequesterInfo.AgentLocID.ToString();


                //Make the call to GC
                Tx[] searchTransactionResp = null;
                try
                {
                    _logger.PublishInformation("Calling Golden Crown...");

                    //Persistence (W2)
                    var persistenceEventGetTransactionInfoExternalRequest = new PersistenceEventModel(request.PersistenceID, request.ProviderID, PersistenceEventType.PayoutGetProviderInfoRequest);
                    persistenceEventGetTransactionInfoExternalRequest.SetContentObject<ReqTxList>(searchTransactionReq);
                    persistenceEventGetTransactionInfoExternalRequest.RequesterInfo = m_mappingHelper.Map<Contract.Models.RequesterInfoModel, Shared.Persistence.Model.RequesterInfoModel>(request.RequesterInfo);
                    m_persistenceHelper.CreatePersistenceEvent(persistenceEventGetTransactionInfoExternalRequest);
                    //End persistence (W2) 
                    using (ServicePortClient client = GetEndPointService_full(m_gcURL, m_gcClientCertSubject, m_gcServiceCertSubject))
                    {
                        searchTransactionResp = client.TxList(searchTransactionReq);
                    }
                        

                    //Persistence (W3)
                    var persistenceEventGetTransactionInfoExternalResponse = new PersistenceEventModel(request.PersistenceID, request.ProviderID, PersistenceEventType.PayoutGetProviderInfoResponse);
                    persistenceEventGetTransactionInfoExternalResponse.SetContentObject<Tx[]>(searchTransactionResp);
                    m_persistenceHelper.CreatePersistenceEvent(persistenceEventGetTransactionInfoExternalResponse);
                    //End persistence (W3) 
                }
                catch (FaultException e)
                {
                    string msg = e.Message;
                    MessageFault fault = e.CreateMessageFault();
                    if (fault.HasDetail == true)
                    {
                        System.Xml.XmlReader reader = fault.GetReaderAtDetailContents();
                        switch (reader.Name)
                        {
                            case "fault":
                                string gccode = fault.GetDetail<fault>().detail.commondetail.code;
                                string gcmsg = GC_ErrorCode.S_GetErrorMessageFromCode(gccode);
                                msg += gccode + ":" + gcmsg;
                                break;
                            default:
                                //while (reader.Read())
                                //{
                                //    if (reader.Name == "StackTraceString")
                                //    {
                                //        break;
                                //    }
                                //}
                                //msg += reader.ReadInnerXml();
                                break;
                        }
                    }
                    throw new Exception(Messages.S_GetMessage("GC_Call") + msg);
                }
                catch (Exception e)
                {
                    throw new Exception(Messages.S_GetMessage("GC_Call") + e.Message);
                }
                _logger.PublishInformation("Got response from Golden Crown server.");

                if (searchTransactionResp == null)
                {
                    throw new Exception(Messages.S_GetMessage("GCResponseNull"));
                }
                
                //Check that there is exactly 1 order returned for the PIN.
                if (searchTransactionResp.Count() == 0)
                {
                    throw new Exception(Messages.S_GetMessage("GCZeroOrdersForPIN"));
                }
                if (searchTransactionResp.Count() != 1)
                {
                    throw new Exception(Messages.S_GetMessage("GCNotExactlyOneOrderForPin") + "NumOrders:[" + searchTransactionResp.Count() + "]");
                }

                //Translate the response:
                Tx tx = searchTransactionResp[0];
                tid.BeneficiaryInfo = new BeneficiaryInfoModel();
                tid.SenderInfo = new SenderInfoModel();
                tid.CustomerServiceMessages = null;
                tid.PayoutRequiredFields = null;


                //Get GC Order Status
                GC_OrderStatus gcOrderStatus = new GC_OrderStatus(GC_OrderStatus.UNKNOWN_DESC);
                if (!string.IsNullOrEmpty(tx.TransferStatus))
                {
                    gcOrderStatus = new GC_OrderStatus(tx.TransferStatus);
                    tid.ReturnInfo.ErrorCode = gcOrderStatus.GetOrderStatus();
                    tid.ReturnInfo.ErrorMessage = gcOrderStatus.GetOrderStatusDescription();
                }

                //Set Order Status in Return Info for Available for Payout:
                if (gcOrderStatus.GetOrderStatus() == GC_OrderStatus.READY_FOR_PAYOUT)
                {
                    tid.ReturnInfo.AvailableForPayout = true;
                  
                }
                else
                {
                    tid.ReturnInfo.AvailableForPayout = false;
                }
                _logger.PublishInformation("GC Order Status: " + gcOrderStatus.GetOrderStatus() + ":" + gcOrderStatus.GetOrderStatusDescription() + " AvailPay=" + tid.ReturnInfo.AvailableForPayout);

                tid.OrderID = tx.OID;
                tid.TransferDate = tx.TransferDate;
                tid.TransferStatus = new GC_OrderStatus(tx.TransferStatus).GetOrderStatusDescription();

                //NOTE: If there is no payout amount, then return an error.
                if (tx.PayData.PayFunds != null)
                {
                    tid.PayoutAmount = S_CreateMoneyModelFromGoldenCrownFunds(tx.PayData.PayFunds);
                }
                else
                {
                    throw new InvalidDataException(Messages.S_GetMessage("MissingPayoutAmount"));
                }

                //NOTE: GC will not return the send amount.  So don't populate it.
                tid.SendAmount = null;
                //TODO: GC will change their API to include
                //send amount, send currency, destination country.
                //because Ria needs these to be able to do the transaction.
                //Add them when they are available.
                /**                
                 * info from Matvey of GC
                · Payout amount – AnsTxList.PayFunds
                · Payout currency -AnsTxList.PayFunds
                · Commission amount – AnsTxList.ReceiverComission
                · Commission Currency -AnsTxList.ReceiverComission
                · Country To – will be provided later
                · Settlement Amount (Amount GC will pay to RIA for the transaction) -AnsTxList.PayFunds + AnsTxList.ReceiverComission
                · Settlement Currency -AnsTxList.PayFunds.Cur
                · Customer Charge Amount and Customer Charge Currency: The reason we are asking for this information is we have payout correspondents who are getting commission based upon customer charge. – We can’t provide this.
                */

                tid.ExchangeRate = tx.ExchangeRate;
                if (tx.Comission != null)
                {
                    tid.Comission = S_CreateMoneyModelFromGoldenCrownFunds(tx.Comission);
                }
                if (tx.ReceiverComission != null)
                {
                    tid.ReceiverComission = S_CreateMoneyModelFromGoldenCrownFunds(tx.ReceiverComission);
                }
                if (tx.SenderComission != null)
                {
                    tid.SenderComission = S_CreateMoneyModelFromGoldenCrownFunds(tx.SenderComission);
                }

                tid.RecAgentID = tx.PayData.FromAgentID;
                tid.CountryFrom = m_countryCode.CreateFrom3CharCode(tx.PayData.FromCountryISO, false).Char2CountryCode;

                //Set Country To
                int index = 0;
                string countryToISO = "";
                if (tx.PayData.ItemsElementName != null)
                {
                    foreach (ItemsChoiceType3 itemType in tx.PayData.ItemsElementName)
                    {
                        if (itemType.Equals(ItemsChoiceType3.ToCountryISO))
                        {
                            countryToISO = tx.PayData.Items[index];
                        }
                        //if (itemType.Equals(ItemsChoiceType3.PayeeProfile)) //Only look for the ones we need.
                        //if (itemType.Equals(ItemsChoiceType3.ToCityID))
                        //if (itemType.Equals(ItemsChoiceType3.ToAgentID))
                        //if (itemType.Equals(ItemsChoiceType3.ToDirectionID))
                        index++;
                    }
                }
                //If not found then initialize an empty countryCode object.
                if (countryToISO != null)
                {
                    tid.CountryTo = m_countryCode.CreateFrom3CharCode(countryToISO, true).Char2CountryCode;
                }

                tid.PayDataMessage = tx.PayData.Message;
                tid.SenderIsResident = tx.PayerIsResident;
                tid.ReceiverIsResident = tx.PayeeIsResident;

                //SENDER and RECEIVER Info:
                Person payer = new Person();
                Person payee = new Person();
                try
                {
                    if (tx.PayData.Item != null)
                    {
                        payer = (Person)tx.PayData.Item;
                        //SENDER:
                        tid.SenderInfo = SetSenderInfo(payer);
                    }
                }
                catch (Exception)
                {
                    //TODO: In this case the 'item' is probably the PayDataPayerAuth and not a person. It could be either.
                    //Throw an error here?
                    _logger.PublishInformation("The Sender Information was not found.");
                }
                try
                {
                    if (tx.PayData.Item1 != null)
                    {
                        payee = (Person)tx.PayData.Item1;
                        //RECEIVER:
                        tid.BeneficiaryInfo = SetBeneficiaryInfo(payee); ;
                    }
                }
                catch (Exception)
                {
                    //TODO: In this case the 'item' is probably the PayDataPayeeAuth and not a person. It could be either.
                    //Throw an error here?
                    _logger.PublishInformation("The Receiver Information was not found.");
                }


            

                //REAL PAYEE:
                //NOTE: If order was already paid then this will show the actual data of the payee,
                // not the orig data given by sender.  If this exists, then use it to overwrite reciever data gathered above.
                if (tx.RealPayee != null)
                {
                    tid.BeneficiaryInfo.Name = tx.RealPayee.FullName;
                    foreach (ParameterType r in tx.RealPayee.Registry)
                    {
                        if (r.PNameID.Equals(REGADDRESS))
                        {
                            tid.BeneficiaryInfo.Address = r.PValue;
                        }
                        if (r.PNameID.Equals(REGCITY))
                        {
                            tid.BeneficiaryInfo.City = r.PValue;
                        }
                    }
                    tid.BeneficiaryInfo.State = "N/A";
                    if ((tx.RealPayee.CountryISO != null) && (!tx.RealPayee.CountryISO.Equals(GC_EMPTY_COUNTRYCODE)))
                    {
                        tid.BeneficiaryInfo.Country = m_countryCode.CreateFrom3CharCode(tx.RealPayee.CountryISO, false).Char2CountryCode;
                    }
                    tid.BeneficiaryInfo.PhoneNumber = tx.RealPayee.Phone;
                    tid.BeneficiaryInfo.EmailAddress = tx.RealPayee.Email;
                    IDType realRecID = IDType.S_CreateFromGoldenCrownIDType(tx.RealPayee.PaperCredentials.CType);
                    tid.BeneficiaryInfo.IDTypeID = realRecID.GetRiaIDTypeID();
                    tid.BeneficiaryInfo.IDType = realRecID.GetRiaIDTypeDesc();
                    tid.BeneficiaryInfo.IDNumber = tx.RealPayee.PaperCredentials.CNumber;
                    tid.BeneficiaryInfo.IDIssuer = tx.RealPayee.PaperCredentials.Issuer;
                    tid.BeneficiaryInfo.IDSerialNumber = tx.RealPayee.PaperCredentials.SerialNumber;
                }

                tid.PayDataNotAfterDate = tx.PayData.NotAfterDate;
                tid.PayDataNotAfterDateSpecified = tx.PayData.NotAfterDateSpecified;
                tid.PayDataNotBeforeDate = tx.PayData.NotBeforeDate;
                tid.PayDataNotBeforeDateSpecified = tx.PayData.NotBeforeDateSpecified;

                tid.OnLegalHold = false; //Always return false.  Ria system will mark it based on compliance later.
                
                //Return the data:
                //tid.ReturnInfo.ErrorCode = 0;
                //tid.ReturnInfo.ErrorMessage = "";
                return tid;
            }
            catch (Exception e)
            {
                _logger.PublishError("CG: Search Transaction Error: " + e.Message);
                throw new InvalidDataException(
                    Messages.S_GetMessage("GCServiceError") + 
                    e.Message);
            }
        }

        /// <summary>
        /// PAYOUT TRANSACTION CALL:
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PayoutTransactionResponseModel PayoutTransaction(PayoutTransactionRequestModel request)
        {
            string orderPin = request.OrderPIN;
            int agentID = request.RequesterInfo.AgentLocID;
            string beneficiaryFirstName = request.Beneficiary.FirstName ?? "";
            string beneficiaryMiddleName = request.Beneficiary.MiddleName ?? "";
            string beneficiaryLastName1 = request.Beneficiary.LastName1 ?? "";
            string beneficiaryLastName2 = request.Beneficiary.LastName2 ?? "";
            string IDTypeDesc = request.Beneficiary.IDType ?? "";
            string IDNumber = request.Beneficiary.IDNumber ?? "";
            string IDSerialNum = request.Beneficiary.IDSerialNumber ?? "";
            string IDIssuer = request.Beneficiary.IDIssuer ?? "";
            string beneficiaryPhoneNum = request.Beneficiary.PhoneNumber ?? "";
            string beneficiaryCountry = request.Beneficiary.Country ?? "";
            string beneficiaryCity = request.Beneficiary.City ?? "";
            string beneficiaryAddress = request.Beneficiary.Address ?? "";

            try
            {
                //NOTE: Golden Crown uses Last, First, Middle, Other Last Names.
                string beneficiaryFullName = "";
                if (beneficiaryLastName1.Length > 0)
                {
                    beneficiaryFullName += beneficiaryLastName1;
                }
                if (beneficiaryFirstName.Length > 0)
                {
                    beneficiaryFullName += " " + beneficiaryFirstName;
                }
                if (beneficiaryMiddleName.Length > 0)
                {
                    beneficiaryFullName += " " + beneficiaryMiddleName;
                }
                if (beneficiaryLastName2.Length > 0)
                {
                    beneficiaryFullName += " " + beneficiaryLastName2;
                }
                //Check Name data is there.
                if (beneficiaryFullName.Length < 1)
                {
                    throw new InvalidDataException(Messages.S_GetMessage("MissingBeneficiaryName"));
                }
                //TODO: Do we need to do an ID data check? GC will return an error if it does not get one. Is this enough?


                //Client Connection
                ServicePortClient client = GetEndPointService_full(m_gcURL, m_gcClientCertSubject, m_gcServiceCertSubject);

                //Format the Request:
                ReqReceive payoutReq = new ReqReceive();
                payoutReq.OID = orderPin;
                Person payee = new Person();
                payee.FullName = beneficiaryFullName;

                Credentials benIDdata = new Credentials();
                benIDdata.CType = IDType.S_CreateFromRiaIDType(IDTypeDesc, IDIssuer).GetGoldenCrownIDTypeDesc();
                benIDdata.CNumber = IDNumber;
                benIDdata.SerialNumber = IDSerialNum;
                benIDdata.Issuer = IDIssuer;
                payee.PaperCredentials = benIDdata;

                payee.Phone = beneficiaryPhoneNum;
                payee.CountryISO = m_countryCode.CreateFrom2CharCode(beneficiaryCountry, false).Char3ISOCountryCode;
                ParameterType country = new ParameterType();
                country.PNameID = REGCOUNTRY;
                country.PValue = m_countryCode.CreateFrom2CharCode(beneficiaryCountry, false).CountryDescription;
                ParameterType city = new ParameterType();
                city.PNameID = REGCITY;
                city.PValue = beneficiaryCity;
                ParameterType address = new ParameterType();
                address.PNameID = REGADDRESS;
                address.PValue = beneficiaryAddress;
                ParameterType[] paramList = { country, city, address };
                payee.Registry = paramList;
                payoutReq.RealPayee = payee;

                //NOTE: This agent ID is the a Ria AgentID, that Golden Crown has imported into their system. This data always needs to be in sync.
                payoutReq.AgentPayee = agentID.ToString();


                //Make the call to GC
                AnsReceive payoutResp = null;
                PayoutTransactionResponseModel ptd = new PayoutTransactionResponseModel();
                try
                {
                    //Persistence (W10)
                    var persistenceEventPayoutTransactionRequest = new PersistenceEventModel(request.PersistenceID, request.ProviderID, PersistenceEventType.PayoutTransactionRequest);
                    persistenceEventPayoutTransactionRequest.SetContentObject<ReqReceive>(payoutReq);                  
                    m_persistenceHelper.CreatePersistenceEvent(persistenceEventPayoutTransactionRequest);
                    //End persistence (W10) 

                    _logger.PublishInformation("Calling Golden Crown...");
                    payoutResp = client.Receive(payoutReq);
                }
                catch (FaultException e)
                {
                    string msg = e.Message;
                    MessageFault fault = e.CreateMessageFault();
                    if (fault.HasDetail == true)
                    {
                        System.Xml.XmlReader reader = fault.GetReaderAtDetailContents();
                        switch (reader.Name)
                        {
                            case "fault":
                                string gccode = fault.GetDetail<fault>().detail.commondetail.code;
                                string gcmsg = GC_ErrorCode.S_GetErrorMessageFromCode(gccode);
                                msg += gccode + ":" + gcmsg;
                                break;
                            default:
                                //while (reader.Read())
                                //{
                                //    if (reader.Name == "StackTraceString")
                                //    {
                                //        break;
                                //    }
                                //}
                                //msg += reader.ReadInnerXml();
                                break;
                        }
                    }
                    //Return a response with the GC server message in it.
                    _logger.PublishInformation("GC Response: " + msg);
                    ReturnInfoModel gcResp = new ReturnInfoModel();
                    gcResp.ErrorCode =(int) Contract.Enumerations.PayoutMessageCode.Undefined;
                    gcResp.ErrorMessage = msg;
                    gcResp.AvailableForPayout = false;
                    gcResp.AllowUnusualOrderReporting = "";
                    gcResp.RemainingBalanceWarningMsg = "";
                    gcResp.UsePayoutGateway = false;
                    ptd.PayoutRequiredFields = null;
                    ptd.PersistenceID = request.PersistenceID;
                    ptd.ReturnInfo = gcResp;
                    ptd.BeneficiaryFee = null;
                    ptd.ConfirmationNumber = "";
                    return ptd;
                }
                catch (Exception e)
                {
                    throw new Exception(Messages.S_GetMessage("GC_Call") + e.Message);
                }
                _logger.PublishInformation("Got response from Golden Crown server.");

                if (payoutResp == null)
                {
                    throw new Exception(Messages.S_GetMessage("GCResponseNull"));
                }

                //Persistence (W11)
                var persistenceEventPayoutTransactionResponse = new PersistenceEventModel(request.PersistenceID, request.ProviderID, PersistenceEventType.PayoutTransactionResponse);
                persistenceEventPayoutTransactionResponse.SetContentObject<AnsReceive>(payoutResp);
                m_persistenceHelper.CreatePersistenceEvent(persistenceEventPayoutTransactionResponse);
                //End persistence (W11) 

                //Translate the response:
                ReturnInfoModel rtnCode = new ReturnInfoModel();
                rtnCode.ErrorCode =(int) Contract.Enumerations.PayoutMessageCode.PaidSuccessful;
                rtnCode.ErrorMessage = "GC Payout Call Success.";
                rtnCode.AvailableForPayout = false;
                rtnCode.AllowUnusualOrderReporting = "";
                rtnCode.RemainingBalanceWarningMsg = "";
                rtnCode.UsePayoutGateway = false;
                ptd.PayoutRequiredFields = null;
                ptd.PersistenceID = request.PersistenceID;
                ptd.ReturnInfo = rtnCode;
                ptd.BeneficiaryFee = S_CreateMoneyModelFromGoldenCrownFunds(payoutResp.ReceiverComission);
                ptd.ConfirmationNumber = payoutResp.OID;
             
                
                //Return the response:
                return ptd;
            }
            catch (Exception e)
            {
                _logger.PublishError("CG: Payout Transaction Error: " + e.Message);
                throw new InvalidDataException(Messages.S_GetMessage("GCServiceError") + e.Message);
            }
        }


        /// <summary>
        /// CONFIRM PAYOUT CALL:
        /// </summary>
        /// <param name="orderPin"></param>
        /// <param name="payDocID"></param>
        /// <param name="payDocDate"></param>
        /// <returns></returns>
        public ConfirmPayoutResponseModel ConfirmPayout(
            int persistenceID,
            string orderPin,
            string payDocID,
            DateTime payDocDate, int providerID)
        {
            try
            {
                //Client Connection
                ServicePortClient client = GetEndPointService_full(m_gcURL, m_gcClientCertSubject, m_gcServiceCertSubject);

                //Format the Request:
                ReqConfirmation confirmReq = new ReqConfirmation();
                confirmReq.OID = orderPin;
                confirmReq.CreditPayDocID = payDocID;
                confirmReq.CreditPayDocDate = payDocDate;


                //TODO: Persistence (W12): Confirm Payout External

                //Make the call to GC
                AnsConfirmation confirmResp = null;
                ConfirmPayoutResponseModel cpd = new ConfirmPayoutResponseModel();
                try
                {
                    _logger.PublishInformation("Calling Golden Crown...");
                    //Persistence (W12)
                    var persistenceEventPayoutExternalConfirmationRequest = new PersistenceEventModel(persistenceID, providerID, PersistenceEventType.PayoutExternalConfirmationRequest);
                    persistenceEventPayoutExternalConfirmationRequest.SetContentObject<ReqConfirmation>(confirmReq);
                    m_persistenceHelper.CreatePersistenceEvent(persistenceEventPayoutExternalConfirmationRequest);
                    //End persistence (W12) 
                    confirmResp = client.Confirmation(confirmReq);
                }
                catch (FaultException e)
                {
                    string msg = e.Message;
                    MessageFault fault = e.CreateMessageFault();
                    if (fault.HasDetail == true)
                    {
                        System.Xml.XmlReader reader = fault.GetReaderAtDetailContents();
                        switch (reader.Name)
                        {
                            case "fault":
                                string gccode = fault.GetDetail<fault>().detail.commondetail.code;
                                string gcmsg = GC_ErrorCode.S_GetErrorMessageFromCode(gccode);
                                msg += gccode + ":" + gcmsg;
                                break;
                            default:
                                //while (reader.Read())
                                //{
                                //    if (reader.Name == "StackTraceString")
                                //    {
                                //        break;
                                //    }
                                //}
                                //msg += reader.ReadInnerXml();
                                break;
                        }
                    }

                    //Return a response with the GC server message in it.
                    _logger.PublishInformation("GC Response: " + msg);
                    cpd.Date= DateTime.Now;
                    cpd.PersistenceID = persistenceID;
                    cpd.TransactionStatusCode = 99;
                    cpd.TransactionStatusMesage = msg;
                    cpd.ConfirmationNumber = "";
                    return cpd;
                }
                catch (Exception e)
                {
                    throw new Exception(Messages.S_GetMessage("GC_Call") + e.Message);
                }
                _logger.PublishInformation("Got response from Golden Crown server.");

                if (confirmResp == null)
                {
                    throw new Exception(Messages.S_GetMessage("GCResponseNull"));
                }


                //Persistence (W13)
                var persistenceEventPayoutExternalConfirmationResponse = new PersistenceEventModel(persistenceID, providerID, PersistenceEventType.PayoutExternalConfirmationResponse);
                persistenceEventPayoutExternalConfirmationResponse.SetContentObject<AnsConfirmation>(confirmResp);
                m_persistenceHelper.CreatePersistenceEvent(persistenceEventPayoutExternalConfirmationResponse);
                //End persistence (W13) 

                //Translate the reponse:
                cpd.Date = DateTime.Now;
                cpd.PersistenceID = persistenceID;
                cpd.ConfirmationNumber = confirmResp.OID;
                cpd.TransactionStatusCode = confirmResp.Status.Code;
                cpd.TransactionStatusMesage = confirmResp.Status.Message;
                return cpd;
            }
            catch (Exception e)
            {
                _logger.PublishError("CG: Payout Transaction Error: " + e.Message);
                throw new InvalidDataException(Messages.S_GetMessage("GCServiceError") + e.Message);
            }
        }



        /// <summary>
        /// Copied directly from GC sample console app.
        /// </summary>
        /// <param name="anUrl"></param>
        /// <param name="aClientCertSubject"></param>
        /// <param name="aGcServiceSubject"></param>
        /// <returns></returns>
        private static ServicePortClient GetEndPointService_full(string anUrl, string aClientCertSubject, string aGcServiceSubject)
        {
            _logger.PublishInformation("Getting GC EndPoint...");

            const string DisableMultipleDNSEntriesInSANCertificate = @"Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate";
            AppContext.SetSwitch(DisableMultipleDNSEntriesInSANCertificate, true);
                        
            HttpsTransportBindingElement transportElement = null;
            transportElement = new HttpsTransportBindingElement();

            //the settings for this transport element are primarily default:
            transportElement.AllowCookies = false;

            //this was changed from default:
            transportElement.AuthenticationScheme = AuthenticationSchemes.Negotiate;

            transportElement.BypassProxyOnLocal = false;
            transportElement.DecompressionEnabled = true;
            transportElement.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            transportElement.KeepAliveEnabled = true;
            transportElement.ManualAddressing = false;
            transportElement.MaxBufferPoolSize = 524288;
            transportElement.MaxBufferSize = 65536;
            transportElement.MaxReceivedMessageSize = 65536;

            //this was changed from default: - to enable two-way authentication by certificates:
            transportElement.RequireClientCertificate = true;

            transportElement.TransferMode = TransferMode.Buffered;
            transportElement.UnsafeConnectionNtlmAuthentication = false;
            transportElement.UseDefaultWebProxy = true;


            //now we will try to establist correct security: will do this semi-authomatic, like follow:
            //create an empty BasicHttpBinding - it will do Soap11 by default:
            BasicHttpBinding binding1 = new BasicHttpBinding();
            
            //specify the level of signing for the basic security as Message 
            //(it will sign the body and timestamp - the only appropriate way)
            binding1.Security.Mode = BasicHttpSecurityMode.Message;

            //previous setting will require client credential flag to be set:
            binding1.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;

            //get whole binding collection from this new binding:
            BindingElementCollection elements = binding1.CreateBindingElements();
            //modify the message version to be exactly Soap11 (and no Addressing!) - just to be sure we have exact version information
            TextMessageEncodingBindingElement messageVersion = elements.Find<TextMessageEncodingBindingElement>();
            messageVersion.MessageVersion = MessageVersion.Soap11;

            //get the pre-configured for SOAP11 security:
            AsymmetricSecurityBindingElement security = elements.Find<AsymmetricSecurityBindingElement>();

            //change the preconfiguration as required for the particilar service:
            security.AllowSerializedSigningTokenOnReply = true;
            security.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            security.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
            security.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
            security.IncludeTimestamp = true;
            security.EnableUnsecuredResponse = false;
            security.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;
            security.RequireSignatureConfirmation = false;
            security.KeyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
            security.AllowInsecureTransport = false;

            //and finally create the custom binding with collection specified:
            CustomBinding binding = new CustomBinding(security, messageVersion, transportElement);

            //now it is possible to have the https endpoint (and not only http one)
            EndpointAddress epa = new EndpointAddress(new Uri(anUrl), EndpointIdentity.CreateDnsIdentity(aGcServiceSubject), (AddressHeaderCollection)null);

            //create an instance of our service reference which is of course the webservice:
            ServicePortClient webService = new ServicePortClient(binding, epa);

            //specify both client certificate to sign the soap message and to provide transport level of security
            webService.ClientCredentials.ClientCertificate.SetCertificate(
                StoreLocation.LocalMachine,
                StoreName.My,
                X509FindType.FindBySubjectName,
                aClientCertSubject);

            //this is to check the server certificate in two-directional authentication:
            webService.ClientCredentials.ServiceCertificate.SetDefaultCertificate(
                StoreLocation.LocalMachine,
                StoreName.TrustedPublisher,//NOTE certificate will searched be in this storage just for the test purposes!
                X509FindType.FindBySubjectName,
                aGcServiceSubject);
            webService.ClientCredentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            //disable in contract the crypt of the message - CFT needs the message to be just signed, not crypted:
            ContractDescription contractDescription = webService.Endpoint.Contract;
            contractDescription.ProtectionLevel = ProtectionLevel.Sign;
            _logger.PublishInformation("Finished getting GC endpoint.");
            return webService;
        }


        /// <summary>
        /// Convert a Golden Crown Funds Object into a 
        /// service MoneyModel object.
        /// </summary>
        /// <param name="funds"></param>
        /// <returns></returns>
        public  MoneyModel S_CreateMoneyModelFromGoldenCrownFunds(Funds funds)
        {
            try
            {
                decimal amount;
                try
                {
                    amount = Decimal.Parse(funds.Amount);
                }
                catch (Exception)
                {
                    throw new InvalidMoneyException(Messages.S_GetMessage("ErrorMoneyAmountInvalid"));
                }
                double precision = Double.Parse(funds.Exp);
                int isoCurNum;
                try
                {
                    isoCurNum = Int32.Parse(funds.Cur);
                }
                catch (Exception)
                {
                    throw new InvalidMoneyException(Messages.S_GetMessage("ErrorMoneyCurrencyCode"));
                }

                //CurrencyCode cc = new CurrencyCode(isoCurNum);

                CurrencyCodeModel cc = m_dataHelper.GetCurrencyCode(isoCurNum);
                decimal power = Decimal.Parse(Math.Pow(10, precision).ToString());
                decimal moneyAmt = amount / power;
                return new MoneyModel(moneyAmt, cc.IsoCodeText);
            }
            catch (Exception)
            {
                throw new InvalidMoneyException(Messages.S_GetMessage("ErrorMoneyFailedToCreate") + "(on convert GC value)");
            }
        }

  
        private SenderInfoModel SetSenderInfo(Person person)
        {
            var senderInfo = new SenderInfoModel();
            //SENDER:
            senderInfo.Name = person.FullName;
            string[] names = person.FullName.Split(' ');
            int numNames = names.Length;
            //Set names to empty stings in case they don't get populated below:
            senderInfo.FirstName = "";
            senderInfo.MiddleName = "";
            senderInfo.LastName1 = "";
            senderInfo.LastName2 = "";
            if (numNames > 0)
            {
                senderInfo.LastName1 = names[0];
            }
            if (numNames > 1)
            {
                senderInfo.FirstName = names[1];
            }
            if (numNames > 2)
            {
                senderInfo.MiddleName = names[2];
            }
            if (numNames > 3)
            {
                senderInfo.LastName2 = names[3];
            }

            if (person.Registry != null)
            {
                foreach (ParameterType r in person.Registry)
                {
                    if (r.PNameID.Equals(REGADDRESS))
                    {
                        senderInfo.Address = r.PValue;
                    }
                    if (r.PNameID.Equals(REGCITY))
                    {
                        senderInfo.City = r.PValue;
                    }
                }
            }
            senderInfo.State = "N/A";
            if ((person.CountryISO != null) && (!person.CountryISO.Equals(GC_EMPTY_COUNTRYCODE)))
            {
                senderInfo.Country = m_countryCode.CreateFrom3CharCode(person.CountryISO, true).Char2CountryCode;
            }
            senderInfo.PhoneNumber = person.Phone;
            senderInfo.EmailAddress = person.Email;
            if (person.PaperCredentials != null)
            {
                IDType sendID = IDType.S_CreateFromGoldenCrownIDType(person.PaperCredentials.CType);
                senderInfo.IDTypeID = sendID.GetRiaIDTypeID();
                senderInfo.IDType = sendID.GetRiaIDTypeDesc();
                senderInfo.IDNumber = person.PaperCredentials.CNumber;
                senderInfo.IDIssuedBy = person.PaperCredentials.Issuer;
                senderInfo.IDSerialNumber = person.PaperCredentials.SerialNumber;
            }
            else
            {
                senderInfo.IDTypeID = -1;
                senderInfo.IDType = "";
                senderInfo.IDNumber = "";
                senderInfo.IDIssuedBy = "";
                senderInfo.IDSerialNumber = "";
            }

            return senderInfo;
        }

        private BeneficiaryInfoModel SetBeneficiaryInfo(Person person)
        {
            var beneficiaryInfo = new BeneficiaryInfoModel();

            //RECEIVER:
            beneficiaryInfo.Name = person.FullName;
            string[] names = person.FullName.Split(' ');
            int numNames = names.Length;
            //Set names to empty stings in case they don't get populated below:
            beneficiaryInfo.FirstName = "";
            beneficiaryInfo.MiddleName = "";
            beneficiaryInfo.LastName1 = "";
            beneficiaryInfo.LastName2 = "";
            if (numNames > 0)
            {
                beneficiaryInfo.LastName1 = names[0];
            }
            if (numNames > 1)
            {
                beneficiaryInfo.FirstName = names[1];
            }
            if (numNames > 2)
            {
                beneficiaryInfo.MiddleName = names[2];
            }
            if (numNames > 3)
            {
                beneficiaryInfo.LastName2 = names[3];
            }

            if (person.Registry != null)
            {
                foreach (ParameterType r in person.Registry)
                {
                    if (r.PNameID.Equals(REGADDRESS))
                    {
                        beneficiaryInfo.Address = r.PValue;
                    }
                    if (r.PNameID.Equals(REGCITY))
                    {
                        beneficiaryInfo.City = r.PValue;
                    }
                }
            }
            beneficiaryInfo.State = "N/A";
            if ((person.CountryISO != null) && (!person.CountryISO.Equals(GC_EMPTY_COUNTRYCODE)))
            {
                beneficiaryInfo.Country = m_countryCode.CreateFrom3CharCode(person.CountryISO, true).Char2CountryCode;
            }
            beneficiaryInfo.PhoneNumber = person.Phone;
            beneficiaryInfo.EmailAddress = person.Email;
            if (person.PaperCredentials != null)
            {
                IDType recID = IDType.S_CreateFromGoldenCrownIDType(person.PaperCredentials.CType);
                beneficiaryInfo.IDTypeID = recID.GetRiaIDTypeID();
                beneficiaryInfo.IDType = recID.GetRiaIDTypeDesc();
                beneficiaryInfo.IDNumber = person.PaperCredentials.CNumber;
                beneficiaryInfo.IDIssuer = person.PaperCredentials.Issuer;
                beneficiaryInfo.IDSerialNumber = person.PaperCredentials.SerialNumber;
            }
            else
            {
                beneficiaryInfo.IDTypeID = -1;
                beneficiaryInfo.IDType = "";
                beneficiaryInfo.IDNumber = "";
                beneficiaryInfo.IDIssuer = "";
                beneficiaryInfo.IDSerialNumber = "";
            }
            return beneficiaryInfo;
        }

    }
}
