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
            var r = mergeArrays(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 });
            Console.ReadLine();
        }
        static int[] mergeArrays(int[] a, int[] b)
        {
            var total = a.Count() + b.Count();
            var newarray = new int[total];
            a.CopyTo(newarray, 0);
            b.CopyTo(newarray, b.Count());
            Array.Sort(newarray);
            return newarray;
        }

    }
}
