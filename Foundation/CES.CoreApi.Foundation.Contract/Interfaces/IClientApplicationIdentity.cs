using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
	public interface IClientApplicationIdentity
	{
		int ApplicationId { get; }

		string Name { get; }

		string AuthenticationType { get; }

		bool IsAuthenticated { get; }

		string OperationName { get; }

		DateTime Timestamp { get; }

		string ApplicationSessionId { get; }

		string ReferenceNumber { get; }

		string ReferenceNumberType { get; }

		string CorrelationId { get; }
	}
}
