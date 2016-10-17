using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers.Google.JsonModels
{
	internal class Geometry
	{
		public GeometryLocation location { get; set; }
		public string location_type { get; set; }
	}
}
