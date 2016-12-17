using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers.Bing.JsonModels
{
	public class Point
	{
		public string type { get; set; }
		public double[] coordinates { get; set; }
	}
}
