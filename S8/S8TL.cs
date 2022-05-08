using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S8TL
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            string s = reader.ReadLine();
            int[] pi = GetPrefixFunction(s);
            writer.Write(string.Join(' ', pi));

            writer.Close();
            reader.Close();
        }

        private static int[] GetPrefixFunction(string s)
        {
            int[] pi = new int[s.Length];
            for (int i = 1; i < s.Length; i++)
            {
                int k = pi[i - 1];
                while (k > 0 && s[k] != s[i])
                    k = pi[k - 1];
                if (s[k] == s[i])
                    k++;
                pi[i] = k;
            }
            return pi;
        }
    }
}