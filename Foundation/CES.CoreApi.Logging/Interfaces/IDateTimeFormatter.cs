using System;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IDateTimeFormatter
	{
		string Format(DateTime date);
	}
}