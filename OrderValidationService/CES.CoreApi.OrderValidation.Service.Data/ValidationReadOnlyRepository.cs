using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Data
{
    public class ValidationReadOnlyRepository : BaseGenericRepository, IValidationReadOnlyRepository
    {
        public ValidationReadOnlyRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        }

        public bool IsOfacWatchListMatched(OfacValidationRequestModel input)
        {
            var request = new DatabaseRequest<bool>
            {
                ProcedureName = "coreapi_sp_WatchListMatchNameCheckWrapper",
                IsCacheable = false,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fAppID", 0)
                    .Add("@fAppObjectID", 0)
                    .Add("@fCustomerID", input.CustomerId)
                    .Add("@sFirstName", input.FirstName)
                    .Add("@sMiddleName", input.MiddleName)
                    .Add("@sLastName1", input.LastName1)
                    .Add("@sLastName2", input.LastName2),
                Shaper = reader => reader.ReadValue<bool>("IsMatched")
            };

            return Get(request);
        }

        public SarValidationResponseModel ValidateSar(SarValidationRequestModel input)
        {
            var request = new DatabaseRequest<SarValidationResponseModel>
            {
                ProcedureName = "compl_sp_Filter_Transaction_Check_Entry_Wrapper",
                IsCacheable = false,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@RecAgentID", input.ReceivingAgentId)
                    .Add("@RecAgentLocID", input.ReceivingAgentLocationId)
                    .Add("@CorrespID", input.Correspondent.Id)
                    .Add("@CorrespLocID", input.Correspondent.LocationId)
                    .Add("@EnteredByID", input.EnteredById)
                    .Add("@CurrencyFrom", input.Amount.CurrencyFrom)
                    .Add("@CurrencyTo", input.Amount.CurrencyTo)
                    .Add("@Amount_From", input.Amount.From)
                    .Add("@Amount_To", input.Amount.To)
                    .Add("@TotalAmount", input.Amount.Total)
                    .Add("@CustomerID", input.CustomerId)
                    .Add("@BenID", input.Beneficiary.Id)
                    .Add("@BenNameFirst", input.Beneficiary.FirstName)
                    .Add("@BenNameMid", input.Beneficiary.MiddleName)
                    .Add("@BenNameLast1", input.Beneficiary.LastName1)
                    .Add("@BenNameLast2", input.Beneficiary.LastName2)
                    .Add("@BenAddress_Street", input.Beneficiary.StreetAddress)
                    .Add("@BenCity", input.Beneficiary.City)
                    .Add("@BenState", input.Beneficiary.State)
                    .Add("@BenCountry", input.Beneficiary.Country)
                    .Add("@BenZip", input.Beneficiary.Zip)
                    .Add("@BenTelNo", input.Beneficiary.Telephone)
                    .Add("@BenAcct_RoutingNo", input.Beneficiary.AccountRoutingNumber)
                    .Add("@BenAcct_No", input.Beneficiary.AccountNumber)
                    .Add("@BenTaxID", input.Beneficiary.TaxId)
                    .Add("@BenIDType", input.Beneficiary.IdType)
                    .Add("@BenIDNo", input.Beneficiary.IdNumber)
                    .Add("@DelivMethod", input.DeliveryMethod)
                    .Add("@EntryMethod", input.EntryMethod)
                    .Add("@StateTo", input.Correspondent.State)
                    .Add("@TransferReasonID", input.TransferReasonId)
                    .Add("@ShowReqdFields", input.ShowRequiredFields)
                    .Add("@UserLanguage", input.UserLanguage)
                    .Add("@BankCountryId", input.BankCountryId)
                    .Add("@TransDate", input.OrderDate)
                    .AddVarCharOut("@Msg", 255)
                    .AddVarCharOut("@Title", 255)
                    .AddVarCharOut("@Action", 50)
                    .AddVarCharOut("@MsgOp", 255)
                    .AddVarCharOut("@MsgCust", 1000)
                    .AddIntOut("@IssueID")
                    .AddBitOut("@ShowMessage")
                    .AddBitOut("@GetFunds")
                    .AddBitOut("@GetIDs1")
                    .AddBitOut("@GetIDs2")
                    .AddBitOut("@CheckRanOK")
                    .AddBitOut("@GotFunds")
                    .AddBitOut("@GotID1")
                    .AddBitOut("@GotID2"),
                Shaper = reader => new SarValidationResponseModel(),
                OutputShaper = (parameters, entity) =>
                {
                    entity.Action = parameters.ReadValue<string>("@Action");
                    entity.CustomerMessage = parameters.ReadValue<string>("@MsgCust");
                    entity.GotIdLevel1 = parameters.ReadValue<bool>("@GotID1");
                    entity.GotIdLevel2 = parameters.ReadValue<bool>("@GotID2");
                    entity.GotSourceOfFunds = parameters.ReadValue<bool>("@GotFunds");
                    entity.IssueId = parameters.ReadValue<int>("@IssueID");
                    entity.RequireIdLevel1 = parameters.ReadValue<bool>("@GetIDs1");
                    entity.RequireIdLevel2 = parameters.ReadValue<bool>("@GetIDs2");
                    entity.RequireSourceOfFunds = parameters.ReadValue<bool>("@GetFunds");
                    entity.ShowCustomerMessage = parameters.ReadValue<bool>("@ShowMessage");
                    entity.ResponseType = entity.Action.GetEnumValueFromDescription<SarResponseType>();
                }
            };
            
            return Get(request);
        }

        public bool IsBeneficiaryBlocked(int beneficiaryId, int correspondentId)
        {
            var request = new DatabaseRequest<bool>
            {
                ProcedureName = "cust_sp_Beneficiary_Blocked_Status",
                IsCacheable = false,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fBenNameID", beneficiaryId)
                    .Add("@fPayAgentID", correspondentId),
                Shaper = reader => reader.ReadValue<bool>("blocked")
            };

            return Get(request);
        }
    }
}