using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3FB
{
    class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }
}
