using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S8TG
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = ReadInt();
            int[] measurements = ReadInts();
            int m = ReadInt();
            int[] pattern = ReadInts();

            var occurences = FindOccurences(measurements, n, pattern, m);
            writer.WriteLine(string.Join(' ', occurences));

            writer.Close();
            reader.Close();
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

        private static List<int> FindOccurences(int[] measurements, int n,
            int[] pattern, int m)
        {
            List<int> occurences = new List<int>();
            int i = 0;
            while (i < n)
            {
                int shift = measurements[i] - pattern[0];
                int j = 1;
                while (j < m && i + j < n &&
                    measurements[i + j] == pattern[j] + shift)
                {
                    j++;
                }
                bool hasPattern = (j == m);
                if (hasPattern)
                    occurences.Add(i + 1);
                i++;
            }
            return occurences;
        }
    }
}