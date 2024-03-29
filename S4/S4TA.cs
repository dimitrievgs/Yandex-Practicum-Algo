﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace S4TA
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int q = ReadInt(); 
            int m = ReadInt();
            string s = reader.ReadLine();

            int hash = CalcHash(s, q, m);
            writer.WriteLine(hash);

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int CalcHash(string s, int q, int m)
        {
            long hash = 0;
            int sLength = s.Length;
            for (int i = 0; i < sLength; i++)
            {
                hash = ((hash * q) % m + ((int)s[i]) % m) % m;
                //hash = (((hash % m) * (q % m)) % m + (int)s[i] % m) % m;
                //hash = hash * q + (int)s[i];
            }
            return (int)hash;
        }
    }
}