using System.Collections.ObjectModel;

namespace CES.CoreApi.Logging.Models
{
	public class ExceptionLogItem
	{
		public ExceptionLogItem()
		{
			ChildItems = new Collection<ExceptionLogItem>();
		}
		
		public string ItemName { get; set; }

		public string ItemValue { get; set; }

		public Collection<ExceptionLogItem> ChildItems { get; private set; }

	}
}