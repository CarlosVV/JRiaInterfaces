using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Enumerations;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Data.Repositories
{
    public class TransactionRepository : BaseGenericRepository, ITransactionRepository
    {
        #region Core

        private static readonly DateTime DefaultDate = Convert.ToDateTime("1900-01-01 00:00:00.000"); //Bad somebody made to have that as default

        public TransactionRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        } 

        #endregion

        #region ITransactionRepository implementation

        public TransactionDetailsModel GetDetails(int orderId, int databaseId = 0)
        {
            var request = new DatabaseRequest<TransactionDetailsModel>
            {
                ProcedureName = "ol_sp_oltblOrdersToPost_GetByOrderID",
                IsCacheable = true,
                DatabaseType = databaseId != 0
                    ? DatabaseType.FrontEnd
                    : DatabaseType.Main,
                DatabaseId = databaseId,
                Parameters = new Collection<SqlParameter>()
                    .Add("@orderId", orderId),
                Shaper = reader => GetTransactionDetails(reader)
            };

            return Get(request);
        }
        
        #endregion

        #region Private methods

        private TransactionDetailsModel GetTransactionDetails(IDataReader reader)
        {
            return new TransactionDetailsModel
            {
                Id = reader.ReadValue<int>("fOrderID"),
                TransactionDate = reader.ReadValue<DateTime>("fDate"),
                TransactionNumber = reader.ReadValue<string>("fOrderNo"),
                Status = GetStatus(reader),
                Customer = GetCustomerDetails(reader),
                ComplianceInformation = GetComplianceDetails(reader),
                Beneficiary = GetBeneficiaryDetails(reader),
                MoneyTransferDetails = GetMoneyTransferDetails(reader),
                ProcessingInformation = GetProcessingInformation(reader),
                PayingAgent = GetPayingAgentDetails(reader),
                ReceivingAgent = GetReceivingAgentDetails(reader),
                TransactionStatus = GetTransactionStatusDetails(reader)
            };
        }

        private static TransactionStatusModel GetTransactionStatusDetails(IDataReader reader)
        {
            return new TransactionStatusModel
            {
                NeedsApproval = reader.ReadValue<bool>("fNeedsApproval"),
                EnteredBy = reader.ReadValue<int>("fEnteredBy"),
                EnteredTime = reader.ReadValue<DateTime>("fEnteredTime"),
                EntryType = reader.ReadValue<string>("fEntryType"),
                ConfirmedBy = reader.ReadValue<int>("fConfirmedBy"),
                ConfirmedTime = reader.ReadValue<DateTime>("fConfirmedTime"),
                CancelledBy = reader.ReadValue<int>("fCanceledBy"),
                CancelledTime = reader.ReadValue<DateTime>("fCanceledTime"),
                Posted = reader.ReadValue<bool>("fPosted"),
                OnHold = reader.ReadValue<int>("fOnHold"),
                ReadyForPosting = reader.ReadValue<bool>("fReadyforPosting"),
                LegalHold = reader.ReadValue<bool>("fLegalHold")
            };
        }

        private static AgentModel GetReceivingAgentDetails(IDataReader reader)
        {
            return new AgentModel
            {
                Id = reader.ReadValue<int>("fRecAgentID"),
                DepartmentId = reader.ReadValue<int>("fDepartmentID"),
                Name = reader.ReadValue<string>("RecAgent"),
                Number = reader.ReadValue<string>("AgentNo"),
                Location = new AgentLocationModel
                {
                    Id = reader.ReadValue<int>("fRecAgentLocID")
                }
            };
        }

        private static AgentModel GetPayingAgentDetails(IDataReader reader)
        {
            return new AgentModel
            {
                Id = reader.ReadValue<int>("fPayAgentID"),
                Name = reader.ReadValue<string>("PayingAgent"),
                Location = new AgentLocationModel
                {
                    Id = reader.ReadValue<int>("fPayAgentLocID"),
                    Name = reader.ReadValue<string>("AgentLocation"),
                    Address = new AddressModel
                    {
                        Address1 = reader.ReadValue<string>("PayAgentLocAddress")
                    }
                }
            };
        }

        private static ProcessingInformationModel GetProcessingInformation(IDataReader reader)
        {
            return new ProcessingInformationModel
            {
                DatabaseId = reader.ReadValue<int>("fDatabaseID"),
                DelayMinutes = reader.ReadValue<int>("fDelayMins"),
                EnteredByLoginId = reader.ReadValue<int>("fEnteredBy_LoginID"),
                IsIndefinite = reader.ReadValue<bool>("fIndefinite"),
                TerminalId = reader.ReadValue<int>("fTerminalID")
            };
        }

        private static MoneyTransferDetailsModel GetMoneyTransferDetails(IDataReader reader)
        {
            return new MoneyTransferDetailsModel
            {
                PreId = reader.ReadValue<int>("fTransID"),
                DeliveryMethod = reader.ReadValue<DeliveryMethodType>("fDelivMethod"),
                PaymentAvailableDate = reader.ReadValue<DateTime>("fDate_Avail_Payout"),
                IsOpenPayment = reader.ReadValue<bool>("fOpenPaymentNoLocationSelected"),
                Pin = reader.ReadValue<string>("fPIN"),
                ReceivingAgentSequentialId = reader.ReadValue<int>("fSeqIDRA"),
                PayingAgentSequentialId = reader.ReadValue<int>("fSeqIDPA"),
                Deposit = GetDepositDetails(reader),
                MonetaryInformation = GetMonetaryInformation(reader)
            };
        }

        private static MonetaryInformationModel GetMonetaryInformation(IDataReader reader)
        {
            return new MonetaryInformationModel
            {
                CountryFrom = reader.ReadValue<string>("fCountryFrom"),
                CountryTo = reader.ReadValue<string>("fCountryTo"),
                Surcharge = reader.ReadValue<decimal>("fSurcharge"),
                ProgramId = reader.ReadValue<int>("fProgramID"),
                AmountDetails = new AmountDetailsModel
                {
                    OrderAmount = reader.ReadValue<decimal>("OrderAmount"),
                    LocalAmount = reader.ReadValue<decimal>("fAmountLocal"),
                    TransferAmount = reader.ReadValue<decimal>("fTransferAmount"),
                    TaxAmount = reader.ReadValue<decimal>("fTaxAmount"),
                    TotalAmount = reader.ReadValue<decimal>("TotalAmount"),
                },
                CurrencyDetails = new CurrencyDetailsModel
                {
                    Currency = reader.ReadValue<string>("fCurrency"),
                    PaymentCurrency = reader.ReadValue<string>("fPaymentCurrency"),
                    BaseCurrency = reader.ReadValue<string>("fCurrency"),
                    CommissionReceivingAgentCurrency = reader.ReadValue<string>("fCommRACur"),
                },
                CommissionDetails = new CommissionDetailsModel
                {
                    Commission = reader.ReadValue<decimal>("fCommission"),
                    CommissionReceivingAgentLocal = reader.ReadValue<decimal>("fCommRA"),
                    CommissionReceivingAgentForeign = reader.ReadValue<decimal>("fCommRAF"),
                    CommissionCustomerDiff = reader.ReadValue<decimal>("fCommCustDiff"),
                    SpecialCommission = reader.ReadValue<bool>("fSpecialComm"),
                    ManualCommission = reader.ReadValue<int>("fManualComm"),
                },
                RateDetails = new RateDetailsModel
                {
                    PaymentRate = reader.ReadValue<decimal>("fPaymentRate"),
                    PaymentBuyRate = reader.ReadValue<decimal>("fPaymentBuyRate"),
                    PaymentRateInverted = reader.ReadValue<bool>("fPaymentRateInv"),
                    Rate = reader.ReadValue<decimal>("fRate"),
                    Inverted = reader.ReadValue<bool>("fInverted"),
                    RateLevel = reader.ReadValue<int>("fRateLevel"),
                    RateFrom = reader.ReadValue<decimal>("fRateFrom"),
                    RateTo = reader.ReadValue<decimal>("fRateTo"),
                    BuyRateFrom = reader.ReadValue<decimal>("fBuyRateFrom"),
                    BuyRateTo = reader.ReadValue<decimal>("fBuyRateTo"),
                    BuyRate = reader.ReadValue<decimal>("fBuyRate"),
                    BaseRate = reader.ReadValue<decimal>("fBaseRate")
                }
            };
        }

        private static BankDepositModel GetDepositDetails(IDataReader reader)
        {
            var bankId = reader.ReadValue<int>("fBankID");
            if (bankId == 0)
                return null;

            return new BankDepositModel
            {
                Bank = new BankModel
                {
                    Id = bankId,
                    BranchName = reader.ReadValue<string>("fBankBranchName"),
                    BranchNumber = reader.ReadValue<string>("fBankBranchNumber"),
                    SwiftCode = reader.ReadValue<string>("fBIC_SWIFT"),
                    RoutingCode = reader.ReadValue<string>("fRoutingCode"),
                    RoutingType = reader.ReadValue<int>("fRoutingType"),
                    ServiceLevelId = reader.ReadValue<int>("fValueTypeID"),
                    LocationId = reader.ReadValue<int>("fBankLocationID"),
                    Name = reader.ReadValue<string>("fBankName")
                },
                Account = new BankAccountModel
                {
                    AccountNumber = reader.ReadValue<string>("fBankAccountNo"),
                    AccountType = reader.ReadValue<int>("fBankAccountType"),
                    AccountTypeName = reader.ReadValue<string>("fBankAcType"),
                    UnitaryAccountNumber = reader.ReadValue<string>("fBankAccountNo_Unitary"),
                    UnitaryAccountType = reader.ReadValue<string>("fBankAccountNo_Unitary_Type")
                        .GetEnumValueFromDescription<UnitaryAccountType>()
                },
                FulfillmentDate = reader.ReadValue<DateTime>("fValueDate"),
                ProviderMapId = reader.ReadValue<int>("fProviderMapID")
            };
        }
        
        private static ContactModel GetContactDetails(IDataReader reader, string homePhoneFieldName, string cellPhoneFieldName)
        {
            var homePhone = reader.ReadValue<string>(homePhoneFieldName);
            var cellPhone = reader.ReadValue<string>(cellPhoneFieldName);

            if (string.IsNullOrEmpty(homePhone) && string.IsNullOrEmpty(cellPhone))
                return null;

            var response = new ContactModel();

            if (!string.IsNullOrEmpty(homePhone))
                response.PhoneList.Add(new TelephoneModel { Number = homePhone, PhoneType = PhoneType.Home });

            if (!string.IsNullOrEmpty(cellPhone))
                response.PhoneList.Add(new TelephoneModel { Number = cellPhone, PhoneType = PhoneType.Cell });

            return response;
        }

        private static ComplianceInformationModel GetComplianceDetails(IDataReader reader)
        {
            return new ComplianceInformationModel
            {
                SourceOfFunds = reader.ReadValue<string>("fSourceOfFunds"),
                Answer = reader.ReadValue<string>("fAnswer"),
                Question = reader.ReadValue<string>("fQuestion"),
                ComplianceLines = reader.ReadValue<string>("fComplianceLines"),
                QuestionAskedOnBehalfOf = reader.ReadValue<bool>("fQstAskedOnBehalfOf"),
                TransferReason = reader.ReadValue<string>("fTransferReason"),
                TransferReasonId = reader.ReadValue<int>("fTransferReasonID")
            };
        }

        #region Beneficiary related methods

        private static BeneficiaryModel GetBeneficiaryDetails(IDataReader reader)
        {
            return new BeneficiaryModel
            {
                Id = reader.ReadValue<int>("fBenID"),
                Relationship = reader.ReadValue<string>("fCustBenRelationship"),
                Name = GetBeneficiaryNameDetails(reader),
                Address = GetBeneficiaryAddressDetails(reader),
                BirthInformation = GetBeneficiaryBirthDetails(reader),
                Contact = GetContactDetails(reader, "fBenTelNo", "fBenCellNo"),
                Message = reader.ReadValue<string>("fMessageforBen"),
                Identification = GetBeneficiaryIdentificationDetails(reader)
            };
        }

        private static ICollection<IdentificationModel> GetBeneficiaryIdentificationDetails(IDataReader reader)
        {
            var idNumber = reader.ReadValue<string>("fBenIDNo");

            if (string.IsNullOrEmpty(idNumber))
                return null;

            return new Collection<IdentificationModel>
            {
                new IdentificationModel
                {
                    IdNumber = idNumber,
                    IdType = reader.ReadValue<string>("fBenIDType")
                }
            };
        }

        private static BirthInformationModel GetBeneficiaryBirthDetails(IDataReader reader)
        {
            var dateOfBirth = reader.ReadValue<DateTime>("fBenDOB");

            if (dateOfBirth <= DefaultDate)
                return null;

            return new BirthInformationModel
            {
                DateOfBirth = dateOfBirth
            };
        }

        private static AddressModel GetBeneficiaryAddressDetails(IDataReader reader)
        {
            return new AddressModel
            {
                Address1 = reader.ReadValue<string>("fBenAddress"),
                City = reader.ReadValue<string>("fBenCity"),
                State = reader.ReadValue<string>("fBenState"),
                PostalCode = reader.ReadValue<string>("fBenZip"),
                Country = reader.ReadValue<string>("fBenCountry")
            };
        }

        private static NameModel GetBeneficiaryNameDetails(IDataReader reader)
        {
            return new NameModel
            {
                FirstName = reader.ReadValue<string>("BeneficiaryNameFirst"),
                LastName1 = reader.ReadValue<string>("BeneficiaryNameLast1"),
                LastName2 = reader.ReadValue<string>("BeneficiaryNameLast2"),
                FullName = reader.ReadValue<string>("BeneficiaryName")
            };
        }

        #endregion
        
        #region Customer related methods

        private static CustomerModel GetCustomerDetails(IDataReader reader)
        {
            return new CustomerModel
            {
                IsChanged = reader.ReadValue<bool>("fCustInfoChanged"),
                Id = reader.ReadValue<int>("fNameIDCustomer"),
                Name = GetCustomerNameDetails(reader),
                Address = GetCustomerAddressDetails(reader),
                Contact = GetContactDetails(reader, "CustomerTelNo", "fCustCellNo"),
                TaxInformation = GetTaxInformation(reader),
                Identification = GetCustomerIdentificationDetails(reader),
                BirthInformation = GetCustomerBirthDetails(reader),
                Gender = reader.ReadValue<string>("fCust_Gender").GetEnumValueFromDescription<GenderEnum>()
            };
        }

        private static BirthInformationModel GetCustomerBirthDetails(IDataReader reader)
        {
            var dateOfBirth = reader.ReadValue<DateTime>("fCustIDDOB");
            var countryOfBirth = reader.ReadValue<string>("fCust_CountryOfBirth");

            if (string.IsNullOrEmpty(countryOfBirth) && dateOfBirth <= DefaultDate)
                return null;

            return new BirthInformationModel
            {
                DateOfBirth = dateOfBirth,
                CountryOfBirth = countryOfBirth,
                PlaceOfBirth = reader.ReadValue<string>("fCust_PlaceOfBirth"),
                StateOfBirth = reader.ReadValue<string>("fCust_StateOfBirth")
            };
        }

        private static TaxInformationModel GetTaxInformation(IDataReader reader)
        {
            var taxId = reader.ReadValue<string>("fTaxID");
            var ssn = reader.ReadValue<string>("fCustIDSSN");

            if (string.IsNullOrEmpty(taxId) && string.IsNullOrEmpty(ssn))
                return null;

            return new TaxInformationModel
            {
                TaxId = taxId,
                TaxCountry = reader.ReadValue<string>("fTaxCountry"),
                Ssn = ssn
            };
        }

        private static ICollection<IdentificationModel> GetCustomerIdentificationDetails(IDataReader reader)
        {
            var result = new Collection<IdentificationModel>
            {
                new IdentificationModel
                {
                    IdNumber = reader.ReadValue<string>("fCustIDNo"),
                    ExpirationDate = reader.ReadValue<DateTime>("fCustIDExp"),
                    IdTaxCountry = reader.ReadValue<string>("fCustIDTaxCountry"),
                    IdType = reader.ReadValue<string>("fCustIDType"),
                    IsChanged = reader.ReadValue<bool>("CustIdChanged"),
                    IsPrimaryId = true,
                    IssuedBy = reader.ReadValue<string>("fCustIDBy"),
                    IssuedCountry = reader.ReadValue<string>("fCustIDIssuedByCountry"),
                    IssuedDate = reader.ReadValue<DateTime>("fIDIssuedDate"),
                    IssuedState = reader.ReadValue<string>("fCustIDIssuedByState")
                }
            };

            var secondIdNumber = reader.ReadValue<string>("fCustID2No");
            if (!string.IsNullOrEmpty(secondIdNumber))
            {
                var secondId = new IdentificationModel
                {
                    IdNumber = secondIdNumber,
                    ExpirationDate = reader.ReadValue<DateTime>("fCustID2Exp"),
                    IdType = reader.ReadValue<string>("fCustID2Type"),
                    IssuedBy = reader.ReadValue<string>("fCustID2By"),
                    IssuedCountry = reader.ReadValue<string>("fCustID2IssuedByCountry"),
                    IssuedDate = reader.ReadValue<DateTime>("fID2IssuedDate"),
                    IssuedState = reader.ReadValue<string>("fCustID2IssuedByState")
                };
                result.Add(secondId);
            }

            return result;
        }

        private static NameModel GetCustomerNameDetails(IDataReader reader)
        {
            return new NameModel
            {
                FirstName = reader.ReadValue<string>("CustomerNameFirst"),
                LastName1 = reader.ReadValue<string>("CustomerNameLast1"),
                LastName2 = reader.ReadValue<string>("CustomerNameLast2"),
                FullName = reader.ReadValue<string>("CustomerName")
            };
        }

        private static AddressModel GetCustomerAddressDetails(IDataReader reader)
        {
            return new AddressModel
            {
                Address1 = reader.ReadValue<string>("fAddress"),
                City = reader.ReadValue<string>("fCity"),
                State = reader.ReadValue<string>("fState"),
                PostalCode = reader.ReadValue<string>("fPostalCode"),
                Country = reader.ReadValue<string>("fCountry")
            };
        }

        private static TransactionStatusEnum GetStatus(IDataReader reader)
        {
            if (reader.ReadValue<int>("fCanceledBy") > 0)
                return TransactionStatusEnum.Canceled;

            return reader.ReadValue<bool>("fPosted")
                ? TransactionStatusEnum.Posted
                : TransactionStatusEnum.Pending;
        }

        #endregion
        
        #endregion
    }
}