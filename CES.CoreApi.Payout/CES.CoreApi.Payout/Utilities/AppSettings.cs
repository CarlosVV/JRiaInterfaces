using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CES.CoreApi.Payout.Utilities
{
	public class AppSettings
	{
		public static int AppId
		{
			get
			{
				int appId;
				int.TryParse(ConfigurationManager.AppSettings["ApplicationId"], out appId);
				return appId;
			}
		}
		public static int AppObjectId
		{
			get
			{
				int appObjectId;
				int.TryParse(ConfigurationManager.AppSettings["AppObjectId"], out appObjectId);
				return appObjectId;
			}
		}

		public static int RiaProviderId
		{
			get
			{
				int riaProviderId;
				int.TryParse(ConfigurationManager.AppSettings["riaProviderId"], out riaProviderId);
				return riaProviderId;
			}
		}
		public static int GoldenCrownProviderId
		{
			get
			{
				int goldenCrownProviderId;
				int.TryParse(ConfigurationManager.AppSettings["goldenCrownProviderId"], out goldenCrownProviderId);
				return goldenCrownProviderId;
			}
		}
		public static float GoldenCrownInterfaceVersion
		{
			get
			{
				float version;
				float.TryParse(ConfigurationManager.AppSettings["goldenCrownInterfaceVersion"], out version);
				return version;
			}
		}

		public static string GoldenCrownServiceUrl
		{
			get
			{

				return ConfigurationManager.AppSettings["goldenCrownServiceUrl"];
		
			}
		}

		public static string GoldenCrownClientCertSubject
		{
			get
			{
			
				return ConfigurationManager.AppSettings["goldenCrownClientCertSubject"];
				
			}
		}
		public static string GoldenCrownServiceSubject
		{
			get
			{

				return ConfigurationManager.AppSettings["goldenCrownServiceSubject"];
			
			}
		}
		public static string GoldenCrownPinRegex
		{
			get
			{

				return ConfigurationManager.AppSettings["goldenCrownPinRegex"];

			}
		}
		public static string RiaPinRegex
		{
			get
			{

				return ConfigurationManager.AppSettings["riaPinRegexp"];

			}
		}

		public static  string XML()
		{
			//			<? xml version = "1.0" ?>< Requirements xmlns:xsi = "http://www.w3.org/2001/XMLSchema-instance" xmlns: xsd = "http://www.w3.org/2001/XMLSchema" >

			//	  < Field >

			//		< FieldName > BeneficiaryNameLast1 </ FieldName >

			//		< RequirementType > Required </ RequirementType >

			//	  </ Field >

			//	  < Field >

			//		< FieldName > BeneficiaryNameFirst </ FieldName >

			//		< RequirementType > Required </ RequirementType >

			//	  </ Field >
			//	</ Requirements >
			return null;
		}
	}
}