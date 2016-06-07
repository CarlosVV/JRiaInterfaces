using CES.CoreApi.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Security.Interfaces
{
	public interface IApplication
	{
		//[DataMember]
		int Id { get; }

		// [DataMember]
		string Name { get; }

		// [DataMember]
		bool IsActive { get; }

		//[DataMember]
		ICollection<ApplicationConfiguration> Configuration { get; }

		// [DataMember]
		 ICollection<ServiceOperation> Operations { get; }
	}
}
