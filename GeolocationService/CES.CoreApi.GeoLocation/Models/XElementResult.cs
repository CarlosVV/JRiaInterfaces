using System.Xml.Linq;

namespace CES.CoreApi.GeoLocation.Models
{
	public class XElementResult
	{
		public XElement Result { get; set; }
		public string  Message { get; set; }

		public bool IsFailed{ get; set; }
	}
}
