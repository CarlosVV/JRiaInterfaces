using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solution
{
    class Solution
    {
        static void Main(string[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT */
            var input = string.Empty;
            var wordsfilter = Console.ReadLine();
            var wordsfilterList = wordsfilter.Split(' ');
            var numberHotels = int.Parse(Console.ReadLine());
            var mentions = new Dictionary<int, int>();

            for (var hotelIndex = 0; hotelIndex < numberHotels; hotelIndex++)
            {
                var hotelId = int.Parse(Console.ReadLine());
                var hotelComments = Console.ReadLine();
                var hotelCommentsWords = wordsfilter.
                    Replace(".", "").Replace(",", "").Split(' ').
                    Select(x => x.ToLower()).Where(m => wordsfilterList.Any(p => p.Equals(m))).ToList();


                if (!mentions.Any(m => m.Key == hotelId))
                {
                    mentions.Add(hotelId, 0);
                }

                mentions[hotelId] = mentions[hotelId] + hotelCommentsWords.Count();

            }

            var orderedResults = string.Join(" ", mentions.OrderBy(m => m.Value).Select(r => r.Key.ToString()).ToArray());

            Console.WriteLine(orderedResults);
            Console.ReadLine();
        }
    }
}