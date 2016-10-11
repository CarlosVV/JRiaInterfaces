using CES.Security.CoreApi.Models;
using System.Threading;
using System.Linq;
namespace CES.CoreApi.PushNotifications.Utilities
{
	/// <summary>
	/// Client  Identity: Don't change it.
	/// To get applicationID, application session and other info from header 
	/// </summary>
	public class Client
	{

		public static ClientApplicationIdentity Identity
		{
			get
			{

				return Thread.CurrentPrincipal.Identity as ClientApplicationIdentity;
			}
		}

		public static string ResponseId
		{
			get
			{

				var ctx = System.Web.HttpContext.Current;				
				var data = ctx.Request.Headers.GetValues("ApplicationId");
				if (data != null)
				{
					return data.FirstOrDefault();
				}
				return null;
			}
		}
	}
}