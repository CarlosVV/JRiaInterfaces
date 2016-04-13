using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Foundation.Data.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Foundation.Data.Configuration;
using System.Configuration;

namespace CES.CoreApi.Foundation.Data.Factory.Tests
{
	[TestClass()]
	public class CoreDatabaseFactoryTests
	{
		[TestMethod()]
		public void CreateTest()
		{


			var x = ConfigurationManager.GetSection("dataAccessConfiguration") as DataAccessConfiguration;

			var ty = x.ServerGroups;
			var f = CoreDatabaseFactory.Create("Main");
			
			Assert.Fail();
		}
	}
}