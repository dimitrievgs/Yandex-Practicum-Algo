/*
ID 
отчёт 
задача https://contest.yandex.ru/contest/26133/problems/B/

-- ПРИНЦИП РАБОТЫ --

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --

*/

using System;
using System.Collections.Generic;
using System.IO;

namespace S8FB
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
            int n = ReadInt();
            TrieNode root = new TrieNode('.');
            for (int i = 0; i < n; i++)
            {
                string word = reader.ReadLine();
                root.AddWord(word);
            }

            bool result = Process(ref text, root, root, 0);
            writer.WriteLine(result ? "YES" : "NO");

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static bool Process(ref string text, TrieNode root, TrieNode subNode, int pos)
        {
            int i = pos;
            TrieNode node = subNode;
            bool reset = false;
            while (i < text.Length)
            {
                char c = text[i];
                if (reset)
                {
                    node = root;
                    reset = false;
                }
                node = node.GetChild(c);
                if (node != null)
                {
                    if (node.Terminal == true)
                    {
                        if (node.Childs.Count > 0) //тут продолжаем идти в подузлы без ресета (спускаем в рекурсию); точка бифуркации, слов гипотетически подходит > 1
                        {
                            bool result = Process(ref text, root, node, i + 1);
                            if (result == true)
                                return true;
                        }
                        reset = true; //иначе идёи дальше
                    }
                }
                else
                    return false;
                i++;
            }
            return node != null && node.Terminal == true;
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

        public TrieNode GetChild(char value)
        {
            return Childs.Find(o => o.Value == value);
        }

        public List<TrieNode> GetChilds(char value)
        {
            return Childs.FindAll(o => o.Value == value);
        }
    }
}