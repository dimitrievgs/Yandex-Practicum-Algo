using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace S4TC
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            Console.ForegroundColor = ConsoleColor.White;
            int q = ReadInt();
            int m = ReadInt();
            string s = reader.ReadLine();
            int t = ReadInt();
            (int I, int J)[] indeces = new (int I, int J)[t];
            for (int i = 0; i < t; i++)
            {
                int[] leftRight = ReadInts();
                indeces[i] = new(leftRight[0] - 1, leftRight[1] - 1); //во входных данных нумерация с 1
            }

            long[] hashI = CalcHashes(s, q, m);
            long[] powers = CalcModPowers(q, m, s.Length);

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < t; i++)
            {
                var iJ = indeces[i];
                long prev = (iJ.I - 1 >= 0) ? Mod(Mod(hashI[iJ.I - 1], m) * Mod(powers[iJ.J - iJ.I + 1], m), m) : 0;
                long hashOfSubString = Mod(hashI[iJ.J] - prev, m);
                writer.WriteLine(hashOfSubString);
            }

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

        static long Mod(long a, long b)
        {
            return a - b * (long)Math.Floor(a / (double)b);
        }

        private static long[] CalcHashes(string s, int q, int m)
        {
            int sLength = s.Length;
            long[] hashI = new long[sLength];
            long hash = 0;
            for (int k = 0; k < sLength; k++)
            {
                hash = Mod(Mod(hash * q, m) + Mod((int)s[k], m), m);
                hashI[k] = hash;
            }
            return hashI;
        }

        private static long[] CalcModPowers(int q, int m, int i)
        {
            long[] powers = new long[i + 1];
            powers[0] = 1;
            for (int t = 1; t <= i; t++)
                powers[t] = Mod(powers[t - 1] * q, m);
            return powers;
        }
    }
}