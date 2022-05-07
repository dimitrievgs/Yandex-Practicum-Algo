// Видимо, лучше переделать через строп
// https://en.wikipedia.org/wiki/Rope_(data_structure)

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace S8TE
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            StringBuilder word = new StringBuilder(reader.ReadLine());
            int n = ReadInt();
            List<List<char>> extraWords = new List<List<char>>(new List<char>[word.Length]);
            for (int j = 0; j < word.Length; j++)
            {
                extraWords[j] = new List<char> { word[j] };
            }
            for (int i = 0; i < n; i++)
            {
                var str = reader.ReadLine().Split(new char[] { ' ' });
                int index = int.Parse(str[1]);
                bool toEnd = index > word.Length - 1;
                List<char> wordCell = extraWords[Math.Min(word.Length - 1, index)];
                if (!toEnd)
                    wordCell.InsertRange(0, str[0]);
                else
                    wordCell.AddRange(str[0]);
            }
            foreach(var w in extraWords)
            {
                foreach (var c in w)
                {
                    writer.Write(c);
                }
            }

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }
    }
}