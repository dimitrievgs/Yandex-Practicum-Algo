using System;
using System.Linq;
using System.IO;
using System.Text;

namespace S8TK
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            var nameOne = reader.ReadLine();
            var nameTwo = reader.ReadLine();

            var nameOneEven = RemoveOddLetters(nameOne);
            var nameTwoEven = RemoveOddLetters(nameTwo);
            var comparison = Compare(nameOneEven, nameTwoEven);
            writer.WriteLine(comparison);

            writer.Close();
            reader.Close();
        }

        private static string RemoveOddLetters(string s)
        {
            StringBuilder sB = new StringBuilder();
            foreach (char c in s)
            {
                if ((int)c % 2 == 0)
                    sB.Append(c);
            }
            return sB.ToString();
        }

        private static int Compare(string sOne, string sTwo)
        {
            int length = Math.Min(sOne.Length, sTwo.Length);
            for (int i = 0; i < length; i++)
            {
                int cOne = (int)sOne[i];
                int cTwo = (int)sTwo[i];
                if (cOne < cTwo)
                    return -1;
                else if (cOne > cTwo)
                    return +1;
            }
            if (sOne.Length < sTwo.Length)
                return -1;
            else if (sOne.Length > sTwo.Length)
                return +1;
            return 0;
        }
    }
}