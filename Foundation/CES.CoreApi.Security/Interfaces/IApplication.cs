using CES.CoreApi.Security.Models;
using System.Collections.Generic;

namespace CES.CoreApi.Security.Interfaces
{
	public interface IApplication
	{
		
		int Id { get; }	
		string Name { get; }
		bool IsActive { get; }
		ICollection<ApplicationConfiguration> Configuration { get; }	
		 ICollection<ServiceOperation> Operations { get; }
	}
}
