using System.Runtime.Serialization;
using CES.CoreApi.Compliance.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Compliance.Service.Contract.Models
{
	[DataContract(Namespace = Namespaces.ComplianceServiceDataContractNamespace)]
    public class CheckPayoutRequest : BaseRequest
    {
    }
}

/*
        @fAppID = -1
		@fAppObjectID = 12
		@lUserNameID = 204992211
		@lRetVal = @lRv out
		@sRetMsg = @sRm Out
		@fServiceID = 111
		@fTransactionID = 1106749152
		@bOverride =0
		@fPayingAgentID = 323814
		@fPayingAgentLocID = 409709
		@fPayout_MethodID = 1
		@fPmtAgentNo = 'TST001'
		@fPmtAgentLocAddress = 'Test Street'
		@fPmtAgentLocCity = 'Test City'
		@fPmtAgentLocState = 'Test State'
		@fPmtAgentLocPostalCode = 'Test Postal Code'
		@fPmtAgentLocCountry = 'US'
		@fPmtBenAddress = 'Ben Address'
		@fPmtBenCity = 'Ben City'
		@fPmtBenState = 'Ben State'
		@fPmtBenCountry = 'Ben Country'
		@fPmtBenPostalCode = 'Ben Postal Code'
		@fBenTaxID = 'TaxID'
		@fBenIDTypeID = 10
		@fBenIDNo = 'TestID001'
*/
