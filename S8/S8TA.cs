using System;
using System.Linq;
using System.IO;

namespace S8TA
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            var words = reader.ReadLine().Split(new char[] { ' ' });
            for (int i = words.Length - 1; i >= 0; i--)
            {
                writer.Write(words[i]);
                if (i > 0)
                    writer.Write(' ');
            }

            writer.Close();
            reader.Close();
        }
    }
}