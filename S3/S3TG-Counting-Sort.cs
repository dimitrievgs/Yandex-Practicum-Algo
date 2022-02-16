using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S3TG
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        private static int[] colors = new int[] { 0, 1, 2 };
        private static int colorsNr = colors.Length;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = ReadInt();
            int[] thingsColors = ReadInts();

            // counting sort - in simplest view, when array to sort has elements
            // with values in range {0...maxValue} without gaps
            int[] thingsCountForEachColor = new int[colorsNr];
            foreach (var el in thingsColors)
                thingsCountForEachColor[el]++;
            for (int i = 0; i < colorsNr; i++)
                for (int j = 0; j < thingsCountForEachColor[i]; j++)
                    writer.Write($"{colors[i]} ");

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