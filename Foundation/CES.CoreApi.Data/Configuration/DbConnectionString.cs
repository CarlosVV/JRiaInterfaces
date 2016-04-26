using System.Configuration;

namespace CES.CoreApi.Data.Configuration
{
	public class DbConnectionString
	{
		public static string Main
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
			}
		}
		public static string FrontEnd
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["FrontEnd"].ConnectionString;
			}
		}
		public static string Readonly
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["Readonly"].ConnectionString;
			}
		}

		public static string ReadOnlyTransactional
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["ReadOnlyTransactional"].ConnectionString;
			}
		}
		public static string ReadOnlyNonTransactional
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["ReadOnlyNonTransactional"].ConnectionString;
			}
		}


		public static string ReadOnlyArchive
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["ReadOnlyArchive"].ConnectionString;
			}
		}
		public static string Image
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["Image"].ConnectionString;
			}
		}

	}
}
