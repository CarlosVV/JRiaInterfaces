namespace CES.CoreApi.Payout.Service.Business.Logic.Utilities
{
	public static class SafeCast
	{
		public static string ToSafeDbString(this string value, string defaultValue= "")
		{
			if (string.IsNullOrEmpty(value))
				return defaultValue;
			return value.Trim();
		}
	}
}
