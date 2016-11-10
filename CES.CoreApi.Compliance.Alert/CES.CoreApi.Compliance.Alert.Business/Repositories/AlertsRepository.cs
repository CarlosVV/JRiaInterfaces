using CES.CoreApi.Compliance.Alert.Business.Interfaces;
using CES.CoreApi.Compliance.Alert.Business.Models;
using CES.Data.Sql;
using System.Text;

namespace CES.CoreApi.Compliance.Alert.Business.Repositories
{
	public class AlertsRepository: IAlertsRepository
	{
		public const string ExternalAlertStatusUpdate = "compl_sp_External_Alert_Status_Update";
		private const int CoreApiApplicationId = 7260;
		private const int AlertsServiceAppObjectId = 0;
		private SqlMapper sqlMapper = DatabaseName.CreateSqlMapper();
		
		public ReviewAlertResponse ReviewIssueClear(ReviewAlertRequest reviewAlertRequest)
		{
			var result = null as  ReviewAlertResponse;
		
			using (var cmdQry = sqlMapper.CreateQuery(DatabaseName.Main, ExternalAlertStatusUpdate))
			{
			
				cmdQry.AddParam("@lAppID", CoreApiApplicationId);
				cmdQry.AddParam("@lAppObjectID", AlertsServiceAppObjectId);
				cmdQry.AddParam("@lProviderID", 201);
				cmdQry.AddParam("@AlertID", reviewAlertRequest.AlertId);
				cmdQry.AddParam("@lServiceID", reviewAlertRequest.ServiceId);
				cmdQry.AddParam("@lTransactionID", reviewAlertRequest.TransactionId);
				cmdQry.AddParam("@lPartyID", reviewAlertRequest.PartyId);
				cmdQry.AddParam("@AlertStatusID", reviewAlertRequest.AlertStatusId);
				cmdQry.AddParam("@AlertStatusName", reviewAlertRequest.AlertStatusName);
				cmdQry.AddParam("@Notes", string.Empty);
				cmdQry.AddParam("@UserName", reviewAlertRequest.UserName);
				cmdQry.AddParam("@RequestDateTime", reviewAlertRequest.RequestDateTime);

				result= cmdQry.QueryComplexOne<ReviewAlertResponse>();

				
			}
			return result;
		}

	}
}
