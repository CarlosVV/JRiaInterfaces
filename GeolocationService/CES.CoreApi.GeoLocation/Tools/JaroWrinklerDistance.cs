using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Tools
{
	public class JaroWrinklerDistance
	{
		private readonly int PREFIX_LENGTH_LIMIT = 0;
		/**
		 * Represents a failed index search.
		 */
		public readonly int INDEX_NOT_FOUND = -1;

		public double Apply(string left, string right, bool byPart)
		{
			string[] lefts = left.Split(' ');
			string[] rights = right.Split(' ');

			double sum = 0;
			List<double> counts = new List<double>();
			foreach (var l in lefts)
			{
				foreach (var r in rights)
				{
					Console.WriteLine($"{l} vs {r}");
					var d = Apply(l, r);
					if (d != 1)
					{
						counts.Add(d);
						sum += d;

					}
					Console.WriteLine(d);

				}
			}
			Console.WriteLine(sum / counts.Count());
			Console.WriteLine("END");
			return 0;

		}
		/**
	* Find the Jaro Winkler Distance which indicates the similarity score
	* between two CharSequences.
	*
	* <pre>
	* distance.apply(null, null)          = IllegalArgumentException
	* distance.apply("","")               = 0.0
	* distance.apply("","a")              = 0.0
	* distance.apply("aaapppp", "")       = 0.0
	* distance.apply("frog", "fog")       = 0.93
	* distance.apply("fly", "ant")        = 0.0
	* distance.apply("elephant", "hippo") = 0.44
	* distance.apply("hippo", "elephant") = 0.44
	* distance.apply("hippo", "zzzzzzzz") = 0.0
	* distance.apply("hello", "hallo")    = 0.88
	* distance.apply("ABC Corporation", "ABC Corp") = 0.91
	* distance.apply("D N H Enterprises Inc", "D &amp; H Enterprises, Inc.") = 0.93
	* distance.apply("My Gym Children's Fitness Center", "My Gym. Childrens Fitness") = 0.94
	* distance.apply("PENNSYLVANIA", "PENNCISYLVNIA")    = 0.9
	* </pre>
	*
	* @param left the first String, must not be null
	* @param right the second String, must not be null
	* @return result distance
	* @throws IllegalArgumentException if either String input {@code null}
	*/
		public double Apply(string left, string right)
		{
			double defaultScalingFactor = 0.1;
			double percentageRoundValue = 100.0;

			if (left == null || right == null)
			{
				throw new Exception("Strings must not be null");
			}

			double jaro = Score(left, right);
			int cl = CommonPrefixLength(left, right);
			double matchScore = Math.Round((jaro + defaultScalingFactor
				* cl * (1.0 - jaro)) * percentageRoundValue) / percentageRoundValue;

			return matchScore;
		}
		/**
     * Calculates the number of characters from the beginning of the strings
     * that match exactly one-to-one, up to a maximum of four (4) characters.
     *
     * @param first The first string.
     * @param second The second string.
     * @return A number between 0 and 4.
     */
		private int CommonPrefixLength(string first, string second)
		{
			int result = GetCommonPrefix(first.ToString(), second.ToString())
				   .Length;

			// Limit the result to 4.
			return result > PREFIX_LENGTH_LIMIT ? PREFIX_LENGTH_LIMIT : result;
		}
		/**
	* Compares all Strings in an array and returns the initial sequence of
	* characters that is common to all of them.
	*
	* <p>
	* For example,
	* <code>getCommonPrefix(new String[] {"i am a machine", "i am a robot"}) -&gt; "i am a "</code>
	* </p>
	*
	* <pre>
	* GetCommonPrefix(null) = ""
	* GetCommonPrefix(new String[] {}) = ""
	* GetCommonPrefix(new String[] {"abc"}) = "abc"
	* GetCommonPrefix(new String[] {null, null}) = ""
	* GetCommonPrefix(new String[] {"", ""}) = ""
	* GetCommonPrefix(new String[] {"", null}) = ""
	* GetCommonPrefix(new String[] {"abc", null, null}) = ""
	* GetCommonPrefix(new String[] {null, null, "abc"}) = ""
	* GetCommonPrefix(new String[] {"", "abc"}) = ""
	* GetCommonPrefix(new String[] {"abc", ""}) = ""
	* GetCommonPrefix(new String[] {"abc", "abc"}) = "abc"
	* GetCommonPrefix(new String[] {"abc", "a"}) = "a"
	* GetCommonPrefix(new String[] {"ab", "abxyz"}) = "ab"
	* GetCommonPrefix(new String[] {"abcde", "abxyz"}) = "ab"
	* GetCommonPrefix(new String[] {"abcde", "xyz"}) = ""
	* GetCommonPrefix(new String[] {"xyz", "abcde"}) = ""
	* GetCommonPrefix(new String[] {"i am a machine", "i am a robot"}) = "i am a "
	* </pre>
	*
	* @param strs array of String objects, entries may be null
	* @return the initial sequence of characters that are common to all Strings
	*         in the array; empty String if the array is null, the elements are
	*         all null or if there is no common prefix.
	*/
		public string GetCommonPrefix(params string[] strs)
		{
			if (strs == null || strs.Length == 0)
			{
				return "";
			}
			int smallestIndexOfDiff = IndexOfDifference(strs);
			if (smallestIndexOfDiff == INDEX_NOT_FOUND)
			{
				// all strings were identical
				if (strs[0] == null)
				{
					return "";
				}
				return strs[0];
			}
			else if (smallestIndexOfDiff == 0)
			{
				// there were no common initial characters
				return "";
			}
			else
			{
				// we found a common initial character sequence
				return strs[0].Substring(0, smallestIndexOfDiff);
			}
		}

		/**
     * This method returns the Jaro-Winkler score for string matching.
     *
     * @param first the first string to be matched
     * @param second the second string to be machted
     * @return matching score without scaling factor impact
     */
		double Score(string first,
			  string second)
		{
			String shorter;
			String longer;

			// Determine which String is longer.
			if (first.Length > second.Length)
			{
				longer = first.ToString().ToLower();
				shorter = second.ToString().ToLower();
			}
			else
			{
				longer = second.ToString().ToLower();
				shorter = first.ToString().ToLower();
			}

			// Calculate the half length() distance of the shorter String.
			int halflength = shorter.Length / 2 + 1;

			// Find the set of matching characters between the shorter and longer
			// strings. Note that
			// the set of matching characters may be different depending on the
			// order of the strings.
			string m1 = GetSetOfMatchingCharacterWithin(shorter, longer,
				   halflength);



			string m2 = GetSetOfMatchingCharacterWithin(longer, shorter,
				   halflength);

			// If one or both of the sets of common characters is empty, then
			// there is no similarity between the two strings.
			if (m1.Length == 0 || m2.Length == 0)
			{
				return 0.0;
			}

			// If the set of common characters is not the same size, then
			// there is no similarity between the two strings, either.
			if (m1.Length != m2.Length)
			{
				return 0.0;
			}

			// Calculate the number of transposition between the two sets
			// of common characters.
			int transpositions = Granspositions(m1, m2);



			double defaultDenominator = 3.0;

			// Calculate the distance.
			// Calculate the distance.
			double dist = (m1.Length / ((double)shorter.Length)
				   + m2.Length / ((double)longer.Length) + (m1.Length - transpositions)
				   / ((double)m1.Length)) / defaultDenominator;



			return dist;
		}
		/**
* Calculates the number of transposition between two strings.
*
* @param first The first string.
* @param second The second string.
* @return The number of transposition between the two strings.
*/
		int Granspositions(string first, string second)
		{
			int transpositions = 0;
			for (int i = 0; i < first.Length; i++)
			{
				if (first[i] != second[i])
				{
					transpositions++;
				}
			}
			return transpositions / 2;
		}


		/**
	* Compares all CharSequences in an array and returns the index at which the
	* CharSequences begin to differ.
	*
	* <p>
	* For example,
	* <code>indexOfDifference(new String[] {"i am a machine", "i am a robot"}) -&gt; 7</code>
	* </p>
	*
	* <pre>
	* distance.indexOfDifference(null) = -1
	* distance.indexOfDifference(new String[] {}) = -1
	* distance.indexOfDifference(new String[] {"abc"}) = -1
	* distance.indexOfDifference(new String[] {null, null}) = -1
	* distance.indexOfDifference(new String[] {"", ""}) = -1
	* distance.indexOfDifference(new String[] {"", null}) = 0
	* distance.indexOfDifference(new String[] {"abc", null, null}) = 0
	* distance.indexOfDifference(new String[] {null, null, "abc"}) = 0
	* distance.indexOfDifference(new String[] {"", "abc"}) = 0
	* distance.indexOfDifference(new String[] {"abc", ""}) = 0
	* distance.indexOfDifference(new String[] {"abc", "abc"}) = -1
	* distance.indexOfDifference(new String[] {"abc", "a"}) = 1
	* distance.indexOfDifference(new String[] {"ab", "abxyz"}) = 2
	* distance.indexOfDifference(new String[] {"abcde", "abxyz"}) = 2
	* distance.indexOfDifference(new String[] {"abcde", "xyz"}) = 0
	* distance.indexOfDifference(new String[] {"xyz", "abcde"}) = 0
	* distance.indexOfDifference(new String[] {"i am a machine", "i am a robot"}) = 7
	* </pre>
	*
	* @param css array of CharSequences, entries may be null
	* @return the index where the strings begin to differ; -1 if they are all
	*         equal
	*/
		public int IndexOfDifference(params string[] css)
		{
			if (css == null || css.Length <= 1)
			{
				return INDEX_NOT_FOUND;
			}
			bool anyStringNull = false;
			bool allStringsNull = true;
			int arrayLen = css.Length;
			int shortestStrLen = int.MaxValue;
			int longestStrLen = 0;

			// find the min and max string lengths; this avoids checking to make
			// sure we are not exceeding the length of the string each time through
			// the bottom loop.
			for (int i = 0; i < arrayLen; i++)
			{
				if (css[i] == null)
				{
					anyStringNull = true;
					shortestStrLen = 0;
				}
				else
				{
					allStringsNull = false;
					shortestStrLen = Math.Min(css[i].Length, shortestStrLen);
					longestStrLen = Math.Max(css[i].Length, longestStrLen);
				}
			}

			// handle lists containing all nulls or all empty strings
			if (allStringsNull || longestStrLen == 0 && !anyStringNull)
			{
				return INDEX_NOT_FOUND;
			}

			// handle lists containing some nulls or some empty strings
			if (shortestStrLen == 0)
			{
				return 0;
			}

			// find the position with the first difference across all strings
			int firstDiff = -1;
			for (int stringPos = 0; stringPos < shortestStrLen; stringPos++)
			{
				char comparisonChar = css[0][stringPos];
				for (int arrayPos = 1; arrayPos < arrayLen; arrayPos++)
				{
					if (css[arrayPos][stringPos] != comparisonChar)
					{
						firstDiff = stringPos;
						break;
					}
				}
				if (firstDiff != -1)
				{
					break;
				}
			}

			if (firstDiff == -1 && shortestStrLen != longestStrLen)
			{
				// we compared all of the characters up to the length of the
				// shortest string and didn't find a match, but the string lengths
				// vary, so return the length of the shortest string.
				return shortestStrLen;
			}
			return firstDiff;
		}

		/**
     * Gets a set of matching characters between two strings.
     *
     * <p>
     * Two characters from the first string and the second string are
     * considered matching if the character's respective positions are no
     * farther than the limit value.
     * </p>
     *
     * @param first The first string.
     * @param second The second string.
     * @param limit The maximum distance to consider.
     * @return A string contain the set of common characters.
     */
		string GetSetOfMatchingCharacterWithin(string first, string second, int limit)
		{
			StringBuilder common = new StringBuilder();
			StringBuilder copy = new StringBuilder(second);

			for (int i = 0; i < first.Length; i++)
			{
				char ch = first[i];
				bool found = false;

				// See if the character is within the limit positions away from the
				// original position of that character.
				for (int j = Math.Max(0, i - limit); !found
						&& j < Math.Min(i + limit, second.Length); j++)
				{
					if (copy[j] == ch)
					{
						found = true;
						common.Append(ch);
						copy[j] = '*';
					}
				}
			}
			return common.ToString();
		}

	}
}
