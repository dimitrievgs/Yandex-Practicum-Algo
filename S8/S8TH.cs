using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace S8TH
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            string text = reader.ReadLine();
            string pattern = reader.ReadLine();
            string insert = reader.ReadLine();

            List<int> occurences = GetOccurences(text, pattern);
            string mergeString = MergeStrings(text, pattern, insert, occurences);
            writer.WriteLine(mergeString);

            writer.Close();
            reader.Close();
        }

        private static List<int> GetOccurences(string text, string pattern)
        {
            List<int> occurences = new List<int>();
            string joinedString = pattern + "#" + text;
            int[] pi = new int[pattern.Length];
            int pi_prev = 0;

            for (int i = 1; i < joinedString.Length; i++)
            {
                int k = pi_prev;
                while (k > 0 && joinedString[k] != joinedString[i])
                    k = pi[k - 1];
                if (joinedString[k] == joinedString[i])
                    k++;
                if (i < pattern.Length)
                    pi[i] = k;
                pi_prev = k;
                if (k == pattern.Length)
                    occurences.Add(i - 2 * pattern.Length);
            }
            return occurences;
        }

        private static string MergeStrings(string text, string pattern, 
            string insert, List<int> positions)
        {
            StringBuilder sB = new StringBuilder(text);
            for (int i = positions.Count - 1; i >= 0; i--)
            {
                sB.Remove(positions[i], pattern.Length);
                sB.Insert(positions[i], insert);
            }
            return sB.ToString();
        }
    }
}