using System.Collections.ObjectModel;

namespace CES.CoreApi.Logging.Models
{
	public class ExceptionLogItem
	{
		#region Core 
		
		public ExceptionLogItem()
		{
			ChildItems = new Collection<ExceptionLogItem>();
		}

		#endregion

		#region Public properties

		public string ItemName { get; set; }

		public string ItemValue { get; set; }


		public Collection<ExceptionLogItem> ChildItems { get; private set; }

		#endregion //Public properties
	}
}