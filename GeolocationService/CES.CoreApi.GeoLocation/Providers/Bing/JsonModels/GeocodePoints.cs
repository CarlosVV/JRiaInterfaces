using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers.Bing.JsonModels
{
	public class GeocodePoint
	{
		public string type { get; set; }
		public double[] coordinates { get; set; }
		public string calculationMethod { get; set; }
		public string[] usageTypes { get; set; }
	}
	
}
