using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S8TD_trie1
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
            TrieNode root = new TrieNode('.');
            for (int i = 0; i < n; i++)
            {
                root.AddWord(reader.ReadLine());
            }

            int prefixLength = 0;
            TrieNode node = root;
            while (node.Childs.Count == 1 && node.Terminal == false)
            {
                prefixLength++;
                node = node.Childs[0];
            }
            writer.WriteLine(prefixLength);

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }
    }

    class TrieNode
    {
        public char Value { get; set; }
        public List<TrieNode> Childs { get; set; }
        public bool Terminal { get; set; }

        public TrieNode(char value)
        {
            Value = value;
            Childs = new List<TrieNode>();
        }

        public void AddWord(string s)
        {
            int childIndex = Childs.FindIndex(o => o.Value == s[0]);
            if (childIndex == -1)
            {
                Childs.Add(new TrieNode(s[0]));
                childIndex = Childs.Count - 1;
            }

            if (s.Length > 1)
            {
                Childs[childIndex].AddWord(s.Substring(1));
            }
            else
            {
                Childs[childIndex].Terminal = true;
            }
        }
    }
}