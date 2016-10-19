using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Models
{
    public class AddressModel
    {
        public string FormattedAddress { get; set; }



		public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Country { get; set; }
		public string CountryName { get; set; }

		public string AdministrativeArea { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string UnitOrApartment { get; set; }

        public List<string> UnitsOrApartments { get; set; }

	
    }

	//public class AddressComponent
	//{
	//	public string FormattedAddress { get; set; }
	//	public string StreetNumber { get; set; }
	//	public string Street { get; set; }
	//	public string StreetLongName { get; set; }
	//	public string Locality { get; set; }
	//	public string LocalityLongName { get; set; }
	//	public string AdministrativeArea { get; set; }
	//	public string AdministrativeAreaLongName { get; set; }
	//	public string Country { get; set; }
	//	public string CountryName { get; set; }
	//	public string PostalCode { get; set; }
	//}
}
