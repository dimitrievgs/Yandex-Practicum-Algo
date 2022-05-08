using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S8TD
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
            string[] words = new string[n];
            for (int i = 0; i < n; i++)
            {
                words[i] = reader.ReadLine();
            }

            writer.WriteLine(GetMaxPrefix(words, n));

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int GetMaxPrefix(string[] words, int n)
        {
            bool maxPrefixFound = false;
            int prefixLength = 0;
            for (int j = 0; j < words[0].Length; j++) //индексы внутри строки
            {
                if (maxPrefixFound)
                    break;
                char c = '#';
                for (int k = 0; k < n; k++) //по всем словам
                {
                    if (words[k].Length - 1 < j)
                    {
                        return prefixLength;
                    }
                    if (k == 0)
                        c = words[0][j];
                    else if (words[k][j] != c)
                    {
                        return prefixLength;
                    }
                }
                prefixLength++;
            }
            return prefixLength;
        }
    }
}