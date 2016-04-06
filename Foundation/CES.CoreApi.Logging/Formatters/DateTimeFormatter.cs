using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
	public class DateTimeFormatter : IDateTimeFormatter
	{
		private const string StartTime = "yyyy-MM-dd HH:mm:ss.fff";
		
		public string Format(DateTime date)
		{
			return date.ToString(StartTime, CultureInfo.InvariantCulture);
		}
	}
}