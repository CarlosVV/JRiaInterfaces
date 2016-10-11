using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace CES.CoreApi.Compliance.Alert.Tests.InitTests
{
	public class WebApiCustomization: ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize<HttpRequestContext>(c => c.Without(x => x.ClientCertificate));
		}
	}
}
