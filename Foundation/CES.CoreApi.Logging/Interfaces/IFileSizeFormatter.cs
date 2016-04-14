using System;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IFileSizeFormatter
	{
		string Format(long fileSize);

		string Format(string format, object arg, IFormatProvider formatProvider);
	}
}