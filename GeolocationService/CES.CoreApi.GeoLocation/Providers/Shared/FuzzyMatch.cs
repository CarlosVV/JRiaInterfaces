using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers.Shared
{

	internal static class FuzzyMatch
	{
		public static int GetGrade(string geoLocationValue, string requestMode)
		{

			if (string.IsNullOrEmpty(geoLocationValue) && string.IsNullOrEmpty(requestMode))
				return 100;

			if (!string.IsNullOrEmpty(geoLocationValue) && string.IsNullOrEmpty(requestMode))
				return 110;



			if (string.IsNullOrEmpty(geoLocationValue))
				return 0;

			if (string.IsNullOrEmpty(requestMode))
				return 0;

			if (geoLocationValue.Trim().Equals(requestMode.Trim(), StringComparison.OrdinalIgnoreCase))
				return 100;

			JaroWrinklerDistance distance = new JaroWrinklerDistance();
			var f = distance.Apply(RemoveDiacritics(geoLocationValue).ToLower().Trim(),RemoveDiacritics(requestMode).ToLower().Trim());

			return Convert.ToInt16(f * 100);


		}

		public static string RemoveLastDot(string value)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			if (value.EndsWith("."))
				return value.Replace(".", "");
			return value;
		}

		public static string RemoveDiacritics(string text)
		{
			var normalizedString = text.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (var c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}

		public static string ZipCodeValidation(string zip)
		{
			if (string.IsNullOrEmpty(zip))
				return string.Empty;
			string result = "";
			int count = 0;
			foreach (var item in zip)
			{
				if (item != '0')
				{
					result = zip.Substring(count);
					return result;
				}
				count++;
			}


			return string.Empty;
		}

		public static AddressPick CorePick(List<SeeAlso> all, AddressRequest addressRequest)
		{
			if (all == null || all.Count < 1)
				return null;

			if (all.Count == 1)
				return new AddressPick { MainPick = all.FirstOrDefault() };

			var pick = new AddressPick { Alternates = new List<SeeAlso>() };
			var q = null as List<SeeAlso>;

		
			 q = (from p in all  select p).OrderByDescending(o => o.AddressComponents.TotalAddressAndZipDistance).ThenBy(n => n.AddressComponents.PostalCodeDistance).ToList();

		

			if (q != null)
			{
				pick.MainPick = q.FirstOrDefault();
				pick.Alternates = q.Skip(1).ToList();
			}

			return pick;
		}


		public static int Compute(this string s, string t)
		{
			int n = s.Length;
			int m = t.Length;
			int[,] d = new int[n + 1, m + 1];

			// Step 1
			if (n == 0)
			{
				return m;
			}

			if (m == 0)
			{
				return n;
			}

			// Step 2
			for (int i = 0; i <= n; d[i, 0] = i++)
			{
			}

			for (int j = 0; j <= m; d[0, j] = j++)
			{
			}

			// Step 3
			for (int i = 1; i <= n; i++)
			{
				//Step 4
				for (int j = 1; j <= m; j++)
				{
					// Step 5
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

					// Step 6
					d[i, j] = Math.Min(
						Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						d[i - 1, j - 1] + cost);
				}
			}
			// Step 7
			return d[n, m];
		}

	}
}
