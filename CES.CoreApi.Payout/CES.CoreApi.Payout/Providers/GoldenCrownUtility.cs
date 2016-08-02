using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.Utilities;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace CES.CoreApi.Payout.Providers
{
	public static class GoldenCrownUtility
	{
		public static int ToInt(this string value)
		{
			int num;
			int.TryParse(value, out num);
			return num;
		}
		public static string ToOrderStatusString(this string transferStatus)
		{
			if (!string.IsNullOrEmpty(transferStatus) && transferStatus.Contains("."))
				return "Expired";


			switch (transferStatus.ToInt())
			{
				case -1:
					return "Expired";
				case 0:
					return "Canceled";
				case 1:
					return "Paid";
				case 2:
					return "Paid";
				case 3:
					return "Paid";
				case 4:
					return "Ready for Payout";
				case 5:
					return "Requested for Payout";
				case 6:
					return "Refunded";
				case 7:
					return "Paid Out";
				case 8:
					return "Refunded";
				case 99:
					return "Unknown";
				default:
					return "Unknown";
			}
		}



		internal static Amount GetAmount(this Funds funds
			)
		{
			decimal amount;
			double precision;
			decimal.TryParse(funds.Amount, out amount);
			double.TryParse(funds.Exp, out precision);

			decimal power = Decimal.Parse(Math.Pow(10, precision).ToString());
			decimal moneyAmt = amount / power;

			return new Amount { Value = moneyAmt, Currency = funds.Cur };
		}

		internal static void SetSender(this Person person, Transaction transaction)
		{

			var name = person.FullName.Split(' ');
			for (int i = 0; i < name.Length; i++)
			{
				if (i == 0)
					transaction.CustomerNameLast1 = name[i];
				else if (i == 1)
					transaction.CustomerNameFirst = name[i];
				else if (i == 2)
					transaction.CustomerNameMid = name[i];
				else if (i == 3)
					transaction.CustomerNameLast2 = name[i];
			}
			if (person.Registry != null)
			{
				foreach (ParameterType r in person.Registry)
				{
					if (r.PNameID.Equals("REGADDRESS"))
					{
						transaction.CustAddress = r.PValue;
					}
					if (r.PNameID.Equals("REGCITY"))
					{
						transaction.CustCity = r.PValue;
					}
				}
			}
			transaction.CustState = "N/A";
			transaction.CustomerTelNo = person.Phone;


		}
		internal static void SetBeneficiary(this Person person, Transaction transaction)
		{

			var name = person.FullName.Split(' ');
			for (int i = 0; i < name.Length; i++)
			{
				if (i == 0)
					transaction.BeneficiaryNameLast1 = name[i];
				else if (i == 1)
					transaction.BeneficiaryNameFirst = name[i];
				else if (i == 2)
					transaction.BeneficiaryNameMid = name[i];
				else if (i == 3)
					transaction.BeneficiaryNameLast2 = name[i];
			}
			if (person.Registry != null)
			{
				foreach (ParameterType r in person.Registry)
				{
					if (r.PNameID.Equals("REGADDRESS"))
					{
						transaction.BenAddress = r.PValue;
					}
					if (r.PNameID.Equals("REGCITY"))
					{
						transaction.BenCity = r.PValue;
					}
				}
			}
			transaction.BenState = "N/A";
			transaction.BenTelNo = person.Phone;
		}

		#region Config
		internal static ServicePortClient CreateServicePortClient()
		{
			HttpsTransportBindingElement transportElement = null;
			transportElement = new HttpsTransportBindingElement();

			//the settings for this transport element are primarily default:
			transportElement.AllowCookies = false;

			//this was changed from default:
			transportElement.AuthenticationScheme = AuthenticationSchemes.Negotiate;

			transportElement.BypassProxyOnLocal = false;
			transportElement.DecompressionEnabled = true;
			transportElement.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
			transportElement.KeepAliveEnabled = true;
			transportElement.ManualAddressing = false;
			transportElement.MaxBufferPoolSize = 524288;
			transportElement.MaxBufferSize = 65536;
			transportElement.MaxReceivedMessageSize = 65536;

			//this was changed from default: - to enable two-way authentication by certificates:
			transportElement.RequireClientCertificate = true;

			transportElement.TransferMode = TransferMode.Buffered;
			transportElement.UnsafeConnectionNtlmAuthentication = false;
			transportElement.UseDefaultWebProxy = true;


			//now we will try to establist correct security: will do this semi-authomatic, like follow:
			//create an empty BasicHttpBinding - it will do Soap11 by default:
			BasicHttpBinding binding1 = new BasicHttpBinding();

			//specify the level of signing for the basic security as Message 
			//(it will sign the body and timestamp - the only appropriate way)
			binding1.Security.Mode = BasicHttpSecurityMode.Message;

			//previous setting will require client credential flag to be set:
			binding1.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;

			//get whole binding collection from this new binding:
			BindingElementCollection elements = binding1.CreateBindingElements();
			//modify the message version to be exactly Soap11 (and no Addressing!) - just to be sure we have exact version information
			TextMessageEncodingBindingElement messageVersion = elements.Find<TextMessageEncodingBindingElement>();
			messageVersion.MessageVersion = MessageVersion.Soap11;

			//get the pre-configured for SOAP11 security:
			AsymmetricSecurityBindingElement security = elements.Find<AsymmetricSecurityBindingElement>();

			//change the preconfiguration as required for the particilar service:
			security.AllowSerializedSigningTokenOnReply = true;
			security.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
			security.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
			security.MessageSecurityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
			security.IncludeTimestamp = true;
			security.EnableUnsecuredResponse = false;
			security.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;
			security.RequireSignatureConfirmation = false;
			security.KeyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
			security.AllowInsecureTransport = false;

			//and finally create the custom binding with collection specified:
			CustomBinding binding = new CustomBinding(security, messageVersion, transportElement);

			//now it is possible to have the https endpoint (and not only http one)
			EndpointAddress epa = new EndpointAddress(new Uri(AppSettings.GoldenCrownServiceUrl),
				EndpointIdentity.CreateDnsIdentity(AppSettings.GoldenCrownServiceSubject), (AddressHeaderCollection)null);

			//create an instance of our service reference which is of course the webservice:
			ServicePortClient webService = new ServicePortClient(binding, epa);

			//specify both client certificate to sign the soap message and to provide transport level of security
			webService.ClientCredentials.ClientCertificate.SetCertificate(
				StoreLocation.LocalMachine,
				StoreName.My,
				X509FindType.FindBySubjectName,
				AppSettings.GoldenCrownClientCertSubject);

			//this is to check the server certificate in two-directional authentication:
			webService.ClientCredentials.ServiceCertificate.SetDefaultCertificate(
				StoreLocation.LocalMachine,
				StoreName.TrustedPublisher,//NOTE certificate will searched be in this storage just for the test purposes!
				X509FindType.FindBySubjectName,
				AppSettings.GoldenCrownServiceSubject);
			webService.ClientCredentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

			//disable in contract the crypt of the message - CFT needs the message to be just signed, not crypted:
			ContractDescription contractDescription = webService.Endpoint.Contract;
			contractDescription.ProtectionLevel = ProtectionLevel.Sign;

			return webService;
		}


		#endregion
	}
}