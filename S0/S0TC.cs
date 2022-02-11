using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace S0TC
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            var n = ReadInt();
            var numbers = ReadList();
            var k = ReadInt();
            double dk = (double)k;

            double msum = 0;
            for (int j = 0; j < k; j++)
            {
                msum += numbers[j];
            }
            writer.Write(msum / dk);
            for (int i = 1; i < n - k + 1; i++)
            {
                msum -= numbers[i - 1];
                msum += numbers[i + k - 1];
                writer.Write(" " + msum / dk);
            }

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static List<int> ReadList()
        {
            return reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }
}