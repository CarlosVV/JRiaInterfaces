using System;
using CES.CoreApi.Logging.Interfaces;
using static System.String;

namespace CES.CoreApi.Logging.Formatters
{
	public class FileSizeFormatter : IFormatProvider, ICustomFormatter, IFileSizeFormatter
	{
		#region Core

		private const string FileSizeFormat = "fs";
		private const decimal OneKiloByte = 1024M;
		private const decimal OneMegaByte = OneKiloByte * 1024M;
		private const decimal OneGigaByte = OneMegaByte * 1024M;

		#endregion

		#region Public methods

		/// <summary>
		/// Returns an object that provides formatting services for the specified type.
		/// </summary>
		/// <param name="formatType"></param>
		/// <returns></returns>
		public object GetFormat(Type formatType)
		{
			return formatType == typeof(ICustomFormatter) ? this : null;
		}

		/// <summary>
		/// Formats file size
		/// </summary>
		/// <param name="fileSize">File size in bytes</param>
		/// <returns></returns>
		public string Format(long fileSize)
		{
			return string.Format(this, "{0:fs}", fileSize);
		}

		/// <summary>
		/// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="arg"></param>
		/// <param name="formatProvider"></param>
		/// <returns></returns>
		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (format == null || !format.StartsWith(FileSizeFormat))
			{
				return DefaultFormat(format, arg, formatProvider);
			}

			if (arg is string)
			{
				return DefaultFormat(format, arg, formatProvider);
			}

			decimal size;
			try
			{
				size = Convert.ToDecimal(arg);
			}
			catch (InvalidCastException)
			{
				return DefaultFormat(format, arg, formatProvider);
			}

			string suffix;
			if (size > OneGigaByte)
			{
				size /= OneGigaByte;
				suffix = " GB";
			}
			else if (size > OneMegaByte)
			{
				size /= OneMegaByte;
				suffix = " MB";
			}
			else if (size > OneKiloByte)
			{
				size /= OneKiloByte;
				suffix = " kB";
			}
			else
			{
				suffix = " B";
			}

			var precision = format.Substring(2);
			if (IsNullOrEmpty(precision))
				precision = "2";
			return string.Format("{0:N" + precision + "}{1}", size, suffix);
		}

		#endregion

		#region Private methods

		private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
		{
			var formattableArg = arg as IFormattable;
			return formattableArg?.ToString(format, formatProvider) ?? arg.ToString();
		}

		#endregion
	}
}