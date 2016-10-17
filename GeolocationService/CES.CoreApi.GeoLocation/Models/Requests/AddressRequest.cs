using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Models.Requests
{
	public class AddressRequest
	{
		public string Address1 { get; set; }

		public string Address2 { get; set; }

		public string City { get; set; }
		public string AdministrativeArea { get; set; }

		public string PostalCode { get; set; }

		public string Country { get; set; }


	}
}
