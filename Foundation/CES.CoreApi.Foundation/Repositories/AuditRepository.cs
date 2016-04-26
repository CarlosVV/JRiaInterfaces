
using CES.CoreApi.Data.Base;
using CES.CoreApi.Data.Enumerations;
using CES.CoreApi.Data.Models;
using CES.CoreApi.Foundation.Contract.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CES.CoreApi.Foundation.Repositories
{
	public class AuditRepository : BaseGenericRepository
	{
		public static void SetAuditAsync(AuditLog auditLog)
		{
			AuditRepository auditRepo = new AuditRepository();
			auditRepo.SetLogAsync(auditLog);
		}
		public static void SetAudit(AuditLog auditLog)
		{
			AuditRepository auditRepo = new AuditRepository();
			auditRepo.SetLog(auditLog);
		}

		public  async void SetLogAsync(AuditLog auditLog)
		{
			var request = new DatabaseRequest<AuditLog>
			{
				ProcedureName = "dbo.sys_sp_LogSOAPCalls",
				IsCacheable = false,
				DatabaseType = DatabaseType.Main,
				Parameters = new Collection<SqlParameter>
				{
					new SqlParameter("@lAppID", auditLog.AppId),
					new SqlParameter("@lAppInstanceID", auditLog.AppInstanceId),
					new SqlParameter("@sAppName", auditLog.AppName),
					new SqlParameter("@lQueue", auditLog.Queue),
					new SqlParameter("@lTransactionID", auditLog.TransactionId),
					new SqlParameter("@lServiceID", auditLog.ServiceId),
					new SqlParameter("@sContext", auditLog.Context),
					new SqlParameter("@lDumpType", auditLog.DumpType),
					new SqlParameter("@xmlSOAPContent", auditLog.SoapContent),
					new SqlParameter("@sTextContent", auditLog.JsonContent),
				}
				
			};

			 await Task.Run(() => Update(request));
		}

		public  void SetLog(AuditLog auditLog)
		{
			var request = new DatabaseRequest<AuditLog>
			{
				ProcedureName = "dbo.sys_sp_LogSOAPCalls",
				IsCacheable = false,
				DatabaseType = DatabaseType.Main,
				Parameters = new Collection<SqlParameter>
				{
					new SqlParameter("@lAppID", auditLog.AppId),
					new SqlParameter("@lAppInstanceID", auditLog.AppInstanceId),
					new SqlParameter("@sAppName", auditLog.AppName),
					new SqlParameter("@lQueue", auditLog.Queue),
					new SqlParameter("@lTransactionID", auditLog.TransactionId),
					new SqlParameter("@lServiceID", auditLog.ServiceId),
					new SqlParameter("@sContext", auditLog.Context),
					new SqlParameter("@lDumpType", auditLog.DumpType),
					new SqlParameter("@xmlSOAPContent", auditLog.SoapContent),
					new SqlParameter("@sTextContent", auditLog.JsonContent),
					new SqlParameter("@gContextGUI", System.Guid.NewGuid()),
					new SqlParameter("@gOwnerContextGUI", auditLog.Id),
				}

			};

			Update(request);
		}
	}
}
