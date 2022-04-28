using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S7TE
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int x = ReadInt();
            int k = ReadInt();
            int[] denominations = ReadInts();
            Array.Sort(denominations, new Comparison<int>((i1, i2) => -1 * i1.CompareTo(i2)));

            var minBanknotesNumber = GetMinBanknotesNumber(x, denominations);
            writer.Write(minBanknotesNumber);

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

        private static int GetMinBanknotesNumber(int moneyAmount, int[] denominations)
        {
            int leftMoneyAmount = moneyAmount;
            int denominationsIndex = 0;
            int banknotesNumber = 0;
            while (leftMoneyAmount > 0 && denominationsIndex < denominations.Length)
            {
                int extraBanknotes = leftMoneyAmount / denominations[denominationsIndex];
                leftMoneyAmount = leftMoneyAmount % denominations[denominationsIndex];
                banknotesNumber += extraBanknotes;
                denominationsIndex += 1;
            }
            return banknotesNumber;
        }
    }
}