using System;


namespace CES.CoreApi.PushNotifications.Models
{
	public class BaseResponse
	{
		public object ResponseId
		{
		
			//get
			//{
			//	//if ( Request.Properties["MS_RequestId"])

			//		return ""; // Utilities.Client.Identity.Name;
			//}
			get;set;
		}
		public DateTime ResponseDate
		{
			get { return DateTime.UtcNow; }
		}
		public object RequestId { get; set; }
	}
}