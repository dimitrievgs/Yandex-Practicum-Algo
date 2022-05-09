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
            bool success = false;
            Stack<(TrieNode SubNode, int Pos)> stack = new Stack<(TrieNode SubNode, int Pos)>();
            stack.Push((subNode, pos));
            while (stack.Count > 0)
            {
                var branch = stack.Pop();
                int i = branch.Pos;
                TrieNode node = branch.SubNode;
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
                                stack.Push((node, i + 1));
                            }
                            reset = true; //иначе идём дальше
                        }
                    }
                    else
                    {
                        break; //не смогли замостить дальше словами
                    }
                    i++;
                }
                success = node != null && node.Terminal == true;
                if (success)
                    break; //если сюда дошли, значит, замостили словами полностью
            }
            return success;
        }
    }

    class TrieNode
    {
        public char Value { get; set; }
        public Dictionary<char, TrieNode> Childs { get; set; }
        public bool Terminal { get; set; }

        public TrieNode(char value)
        {
            Value = value;
            Childs = new Dictionary<char, TrieNode>();
        }

        public void AddWord(string s)
        {
            TrieNode node;
            Childs.TryGetValue(s[0], out node); //Childs.FindIndex(o => o.Value == s[0]);
            if (node == null)
            {
                node = new TrieNode(s[0]);
                Childs.Add(s[0], node); //.Add(new TrieNode(s[0]));
                //childIndex = Childs.Count - 1;
            }

            if (s.Length > 1)
            {
                node.AddWord(s.Substring(1));
            }
            else
            {
                node.Terminal = true;
            }
        }

        public TrieNode GetChild(char value)
        {
            TrieNode node;
            Childs.TryGetValue(value, out node);
            return node; // Childs.Find(o => o.Value == value);
        }
    }
}