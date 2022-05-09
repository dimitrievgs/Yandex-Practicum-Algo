/*
ID 
отчёт 
задача https://contest.yandex.ru/contest/26133/problems/A/

-- ПРИНЦИП РАБОТЫ --

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --

*/

using System;
using System.IO;
using System.Text;

namespace S8FA
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
                words[i] = ReadPackedString(reader.ReadLine(), 0).Str.ToString();
            }

            string maxCommonPrefix = words[0].Substring(0, GetMaxPrefix(words, n));
            writer.WriteLine(maxCommonPrefix);

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static (StringBuilder Str, int Pos) ReadPackedString(string s, int pos)
        {
            StringBuilder sOut = new StringBuilder();

            int i = pos;
            int mult = 0;
            while (i < s.Length)
            {
                if (char.IsDigit(s[i]))
                {
                    StringBuilder sNumber = new StringBuilder();
                    while (i < s.Length && char.IsDigit(s[i]))
                    {
                        sNumber.Append(s[i]);
                        i++;
                    }
                    mult = int.Parse(sNumber.ToString());
                }
                else if (mult > 0 && s[i] == '[')
                {
                    StringBuilder subSB;
                    (subSB, i) = ReadPackedString(s, i + 1);
                    for (int j = 0; j < mult; j++)
                        sOut.Append(subSB);
                }
                else if (char.IsLetter(s[i]))
                {
                    while (i < s.Length && char.IsLetter(s[i]))
                    {
                        sOut.Append(s[i]);
                        i++;
                    }
                }
                else if (s[i] == ']')
                {
                    i++;
                    break;
                }
            }
            return (sOut, i);
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