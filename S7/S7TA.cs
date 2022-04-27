using System;
using System.Linq;
using System.IO;

namespace S7TA
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
            int[] stock = ReadInts();

            int maxGain = MaxGain(stock);
            writer.Write(maxGain);

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

        private static int MaxGain(int[] stock)
        {
            int k = 0, low = 0, high = 0, maxGain = 0;
            while (k + 1 < stock.Length)
            {
                while (k + 1 < stock.Length && stock[k] >= stock[k + 1])
                    k++;
                low = stock[k];
                while (k + 1 < stock.Length && stock[k] <= stock[k + 1])
                    k++;
                high = stock[k];
                maxGain += high - low;
            }
            return maxGain;
        }
    }
}