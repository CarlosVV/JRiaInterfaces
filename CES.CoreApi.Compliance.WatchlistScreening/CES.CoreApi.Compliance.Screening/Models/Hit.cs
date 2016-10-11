using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CES.CoreApi.Compliance.Screening.Models
{
	[DataContract]
	public class Hit
    {
		[DataMember(Name = "DisplayName")]
		public string displayName { get; set; }
		[DataMember(Name = "DeceasedDate")]
		public string deceasedDate { get; set; }
		[DataMember(Name = "EntryId")]
		public string entryId { get; set; }
		[DataMember(Name = "EntryType")]
		public string entryType { get; set; }
		[DataMember(Name = "EntryUpdateDate")]
		public string entryUpdateDate { get; set; }
		[DataMember(Name = "Gender")]
		public string gender { get; set; }
		[DataMember(Name = "IsDeceased")]	
		public bool? isDeceased { get; set; }
		[DataMember(Name = "IsNameBroken")]
		public bool? isNameBroken { get; set; }

		[DataMember(Name = "ListId")]
		public string listId { get; set; }
		[DataMember(Name = "ListUpdateDate")]
		public string listUpdateDate { get; set; }
		[DataMember(Name = "ListVersion")]
		public string listVersion { get; set; }
		[DataMember(Name = "MatchType")]
		public string matchType { get; set; }
		[DataMember(Name = "MatchedName")]
		public string matchedName { get; set; }
		[DataMember(Name = "MessageTag")]
		public string messageTag { get; set; }
		[DataMember(Name = "Position")]
		public string position { get; set; }
		[DataMember(Name = "Title")]
		public string title { get; set; }
        [DataMember(Name = "Category")]
        public dynamic categories { get; set; }
        public List<string> categoriesNames { get; set; }
    }
}