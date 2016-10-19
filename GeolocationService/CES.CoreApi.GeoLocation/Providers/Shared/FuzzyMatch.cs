using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers.Shared
{
	internal static class FuzzyMatch
	{
	
		public static AddressPick CorePick(List<SeeAlso> all, AddressRequest addressRequest)
		{
			if (all == null || all.Count < 1)
				return null;

			if (all.Count == 1)
				return new AddressPick { MainPick = all.FirstOrDefault() };

			var pick = new AddressPick { Alternates = new List<SeeAlso>() };


			var q = (from p in all select p).OrderBy(o => o.Weight);

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
