using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace S1TD
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
            var days = ReadList();

            int chaoticDays = 0;
            int daysNr = days.Length;
            for (int i = 0; i < daysNr; i++)
            {
                int day = days[i];
                bool higherThanPrev = (i == 0 || day > days[i - 1]);
                bool higherThanNext = (i == daysNr - 1 || day > days[i + 1]);
                if (higherThanPrev && higherThanNext)
                    chaoticDays++;
            }
            writer.WriteLine(chaoticDays);

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadList()
        {
            return reader.ReadLine()
            .Split(new char[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        }
    }
}