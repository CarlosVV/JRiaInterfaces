using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models.Responses.Validate;
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

		public AddressComponent AddressComponent{ get; set; }
		public string ResultCodes { get; set; }

		public int Weight { get; set; }
		public DataProviderType DataProvider { get; set; }
		public string DataProviderName { get; set; }
		public string ProviderMessage { get; set; }
		public List<SeeAlso> @SeeAlso { get; set; }
		public object RowData { get; set; }

	}

	public class AddressPick
	{
		public SeeAlso MainPick { get; set; }
		public List<SeeAlso> Alternates { get; set; }
	}

	public class SeeAlso
	{
		public AddressComponent AddressComponents { get; set; }
		public LocationModel Location
		{
			get; set;
		}
		public string Types { get; set; }
		public string ResultCodes { get; set; }

		public int Weight { get; set; }
	}
}
