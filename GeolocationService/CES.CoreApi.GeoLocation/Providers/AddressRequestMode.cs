using CES.CoreApi.GeoLocation.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers
{
	public class AddressRequestMode
	{
		public AddressRequest RequestModifed { get; set; }
		public string StateShort { get; set; }
		public string Url { get; set; }
	}
}
