using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Models.Responses
{
	public class AddressValidationResponse
	{
		public object Address { get; set; }
		public object Location { get; set; }

		public object AddressComponent { get; set; }

		public string DataProvider { get; set; }
		public string ResultCodes { get; set; }

		public string ProviderMessage { get; set; }

		public object RowData { get; set; }
	}
}
