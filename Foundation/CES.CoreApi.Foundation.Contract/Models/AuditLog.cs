namespace CES.CoreApi.Foundation.Contract.Models
{
	public class AuditLog
	{
		public int AppId { get; set; }
		public int AppInstanceId { get; set; }
		public string AppName { get; set; }
		public string Context { get; set; }
		public int DumpType { get; set; }
		public System.Guid Id { get; set; }
		public string JsonContent { get; set; }
		public int Queue { get; set; }
		public int ServiceId { get; set; }
		public string SoapContent { get; set; }
		public int TransactionId { get; set; }
	}
}
