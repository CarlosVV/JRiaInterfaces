using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
	public class DateTimeFormatter : IDateTimeFormatter
	{
		private const string StartTime = "yyyy-MM-dd HH:mm:ss.fff";

		#region Public methods

		/// <summary>
		/// Returns execution start time formatted
		/// </summary>
		public string Format(DateTime date)
		{
			return date.ToString(StartTime, CultureInfo.InvariantCulture);
		}

		#endregion //Public methods
	}
}