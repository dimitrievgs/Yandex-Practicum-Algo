using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4TD
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            int n = ReadInt();
            Dictionary<int, string> hobbyGroups = new Dictionary<int, string>();

            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                string s = Console.ReadLine();
                if (!hobbyGroups.ContainsValue(s))
                {
                    hobbyGroups.Add(counter, s);
                    counter++;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            for (int k = 0; k < counter; k++)
            {
                if (hobbyGroups.TryGetValue(k, out string sValue))
                    Console.WriteLine(sValue);
            }
        }

        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }
    }
}
