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
            int numberSquares = 0;
            int numberRectangles = 0;
            int numberOtherPolygons = 0;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                var sidesString = input.Split(' ');
                var sidesValues = sidesString.Select(m => int.Parse(m)).ToList();
                if (sidesValues.Count() == 4)
                {
                    var a = sidesValues[0];
                    var b = sidesValues[1];
                    var c = sidesValues[2];
                    var d = sidesValues[3];

                    if (a > 0 && b > 0 && c > 0 & d > 0)
                    {
                        if (a == b && b == c && c == d)
                        {
                            numberSquares++;
                        }
                        else if (a == c && b == d)
                        {
                            numberRectangles++;
                        }
                        else
                        {
                            numberOtherPolygons++;
                        }
                    }
                    else
                    {
                        numberOtherPolygons++;
                    }
                }
                else
                {
                    numberOtherPolygons++;
                }
            }
            while (true);
            Console.WriteLine($"{numberSquares} {numberRectangles} {numberOtherPolygons}");
            Console.ReadLine();
        }
    }
}