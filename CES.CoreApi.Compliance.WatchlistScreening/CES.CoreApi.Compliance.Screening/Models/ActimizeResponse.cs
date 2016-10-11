using System.Collections.Generic;

namespace CES.CoreApi.Compliance.Screening.Models
{
	public class ActimizeResponse
	{
		public int? ReturnCode { get; set; }
		public string Message { get; set; }
		public int Score { get; set; }
		public bool IsAlerted { get; set; }
		public bool HasHits { get; set; }	
		public List<Hit> Hits { get; set; }	
        public string AlertId { get; set; }
	}
}