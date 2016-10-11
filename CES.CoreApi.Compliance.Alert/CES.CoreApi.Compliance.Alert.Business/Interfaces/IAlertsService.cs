
using CES.CoreApi.Compliance.Alert.Business.Models;

namespace CES.CoreApi.Compliance.Alert.Business.Interfaces
{
	public interface IAlertsService
	{
		ReviewAlertResponse ReviewIssueClear(ReviewAlertRequest reviewAlertRequest);
	}
}
