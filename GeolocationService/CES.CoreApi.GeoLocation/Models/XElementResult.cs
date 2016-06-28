using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
