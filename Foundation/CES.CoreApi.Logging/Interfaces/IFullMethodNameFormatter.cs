namespace CES.CoreApi.Logging.Interfaces
{
	public interface IFullMethodNameFormatter
	{
		string Format(string fullTypeName, string methodName);
	}
}