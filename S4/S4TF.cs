using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4TF
{
    class Solution
    {
        public static void Main(string[] args)
        {
            int n = ReadInt();
            string[] strings = ReadStrings();
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();

            for (int i = 0; i < n; i++)
            {
                char[] cA = strings[i].ToCharArray();
                Array.Sort(cA);
                string s = string.Concat(cA);
                List<int> numbers = null;
                dict.TryGetValue(s, out numbers);
                if (numbers != null)
                    numbers.Add(i);
                else
                    dict.Add(s, new List<int> { i });
            }
            foreach (var el in dict)
            {
                string g = string.Join(' ', el.Value);
                Console.WriteLine(g);
            }
        }

        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        private static string[] ReadStrings()
        {
            return Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
        }
    }
}
