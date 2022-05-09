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

            bool result = PaveWithWords(ref text, root, 0);
            writer.WriteLine(result ? "YES" : "NO");

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static bool PaveWithWords(ref string text, TrieNode root, int pos)
        {
            bool result = false;
            Queue<(TrieNode IntermNode, int Pos)> queue = new Queue<(TrieNode SubNode, int Pos)>();
            queue.Enqueue((root, pos));
            bool[] milestones = new bool[text.Length];
            while (queue.Count > 0)
            {
                var branch = queue.Dequeue();
                int i = branch.Pos;
                TrieNode node = branch.IntermNode;
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
                            if (node.Childs.Count > 0) //точка ветвления, слов гипотетически подходит > 1, потому создаём новую ветвь для проверки следующих терминальных узлов
                                queue.Enqueue((node, i + 1));
                            if (milestones[i] == false)
                            {
                                milestones[i] = true; //до сюда есть решение, как минумум одно
                                reset = true; //остаёмся в этой ветви, идём дальше прикладывать следующее слово
                            }
                            else //отсюда уже искали решение
                            {
                                //дальше смысла смотреть эту ветвь нет, от этой позиции уже смотрели
                                //переходим к следующей ветви
                                break; 
                            }
                        }
                    }
                    else
                        break; //не смогли замостить дальше словами
                    i++;
                }
                //дошли до последней позиции в строке и успешно сопоставили терминальному узлу
                result = i == text.Length && node != null && node.Terminal == true; 
                if (result)
                    break; //если сюда дошли, значит, замостили словами полностью
            }
            return result;
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
            Childs.TryGetValue(s[0], out node);
            if (node == null)
            {
                node = new TrieNode(s[0]);
                Childs.Add(s[0], node);
            }
            if (s.Length > 1)
                node.AddWord(s.Substring(1));
            else
                node.Terminal = true;
        }

        public TrieNode GetChild(char value)
        {
            TrieNode node;
            Childs.TryGetValue(value, out node);
            return node;
        }
    }
}