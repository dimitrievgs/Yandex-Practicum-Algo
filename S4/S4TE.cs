using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4TE
{
    class Solution
    {
        public static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int sLength = s.Length;

            int maxString = 0;
            for (int i = 0; i < sLength; i++)
            {
                Dictionary<char, int> dict = new Dictionary<char, int>();
                for (int j = i; j < sLength; j++)
                {
                    if (!dict.ContainsKey(s[j]))
                        dict.Add(s[j], 1);
                    else
                    {
                        break;
                    }
                }
                maxString = Math.Max(maxString, dict.Count);
            }
            Console.WriteLine(maxString);
        }
    }
}
