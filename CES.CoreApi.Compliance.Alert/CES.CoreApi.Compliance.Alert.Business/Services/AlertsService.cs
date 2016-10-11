
using CES.CoreApi.Compliance.Alert.Business.Models;

namespace CES.CoreApi.Compliance.Alert.Business.Interfaces
{
	public class AlertsService: IAlertsService
	{
		private IAlertsRepository _alertsRepository;

		public AlertsService(IAlertsRepository alertsRepository)
		{
			_alertsRepository = alertsRepository;
		}

		public ReviewAlertResponse ReviewIssueClear(ReviewAlertRequest reviewAlertRequest)
		{
			return _alertsRepository.ReviewIssueClear(reviewAlertRequest);
		}
	}
}
