using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Logging.Models
{
	public class ExceptionLogItemGroup
	{
		public ExceptionLogItemGroup()
		{
			Items = new Collection<ExceptionLogItem>();
		}

		public string Title { get; set; }

		public Collection<ExceptionLogItem> Items { get; private set; }

		public void AddItem(string itemName, Func<object> method)
		{
			try
			{
				var itemValue = method();
				AddItem(itemName, itemValue);
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch
			// ReSharper restore EmptyGeneralCatchClause
			{
			}
		}

		public void AddItem(string itemName, object itemValue)
		{
			var item = Items.FirstOrDefault(p => p.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase));
			if (item != null)
				return;

			item = new ExceptionLogItem
			{
				ItemName = itemName == null
					? string.Empty
					: HttpUtility.HtmlDecode(itemName),
				ItemValue = itemValue == null
					? string.Empty
					: HttpUtility.HtmlDecode(itemValue.ToString())
			};

			Items.Add(item);
		}

		public void AddSubItem(string itemName, string subItemName, object subItemValue)
		{
			var item = Items.FirstOrDefault(p => p.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase));
			if (item == null)
				throw new ApplicationException(string.Format(CultureInfo.InvariantCulture,
					"The exception log item '{0}' was not found in the grop '{1}' item collection.",
					itemName, Title));

			var subItem = item.ChildItems.FirstOrDefault(p => p.ItemName.Equals(subItemName, StringComparison.OrdinalIgnoreCase));
			if (subItem != null)
				throw new ApplicationException(string.Format(CultureInfo.InvariantCulture,
					"The exception log sub item '{0}' for item '{1}' already exists.",
					subItemName, itemName));

			subItem = new ExceptionLogItem
			{
				ItemName = subItemName == null
					? string.Empty
					: HttpUtility.HtmlDecode(subItemName),
				ItemValue = subItemValue == null
					? string.Empty
					: HttpUtility.HtmlDecode(subItemValue.ToString())
			};

			item.ChildItems.Add(subItem);
		}
	}
}