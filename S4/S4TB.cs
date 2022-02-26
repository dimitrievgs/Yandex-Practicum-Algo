// не решена

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace S4TB
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int q = 1000;
            int m = 123_987_123;

            int n = ReadInt();
            string s = RandomString(n);
            int hash = CalcHash(s, q, m);
            writer.WriteLine(s);
            writer.WriteLine(hash);

            //int q = ReadInt(); 
            //int m = ReadInt();
            //string s = reader.ReadLine();

            //int hash = CalcHash(s, q, m);
            //writer.WriteLine(hash);

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

        private static string RandomString(int n)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                sb.Append(RandomChar());
            }
            return sb.ToString();
        }

        private static Random random = new Random();

        private static char RandomChar()
        {
            int l1 = (int)'a';
            int l2 = (int)'z';
            char c = (char)random.Next(l1, l2 + 1);
            return c;
        }


    }
}