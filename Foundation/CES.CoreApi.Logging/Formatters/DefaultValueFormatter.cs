using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
	public class DefaultValueFormatter : IDefaultValueFormatter
	{
		public string Format(object value)
		{
			return value?.ToString() ?? string.Empty;
		}
	}
}