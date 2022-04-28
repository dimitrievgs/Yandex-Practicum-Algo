using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S7TD
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
            long fibonacci = CalculateFibonacci(n);
            writer.Write(fibonacci);

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static long divider = 1_000_000_007;

        private static long CalculateFibonacci(int n)
        {
            long[] array = new long[n + 1];
            if (n > 0)
                array[0] = 1;
            if (n > 1)
                array[1] = 1;
            for (int i = 2; i <= n; i++)
                array[i] = (array[i - 1] + array[i - 2]) % divider;
            return array[n];
        }
    }
}