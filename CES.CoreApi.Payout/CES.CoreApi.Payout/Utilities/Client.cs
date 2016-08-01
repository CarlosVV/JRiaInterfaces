using CES.Security.CoreApi.Models;

namespace CES.CoreApi.Payout.Utilities
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
				return System.Threading.Thread.CurrentPrincipal.Identity as ClientApplicationIdentity;
			}
		}
	}
}