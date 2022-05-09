/*
ID 68248851
отчёт https://contest.yandex.ru/contest/26133/run-report/68248851/
задача https://contest.yandex.ru/contest/26133/problems/B/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи на вход подаётся строка, вероятно, состоящая из некоторого набора слов, 
не разделённых пробелами.
И набор слов, лексикон, из которых, как предполагается, состоит входная строка.
Для хранения лексикона используется префиксное дерево, реализованное в TrieNode
с дочерними узлами, хранящимися в hashmap/словаре как <значение, узел> (для доступа к подузлу за O(1)).
Алгоритм проверки возможности замостить входную строку словами из лексикона
реализован к PaveWithWords(...). 
Поскольку узел, будучи терминальным, может содержать дочерние узлы, то,
достигнув терминального узла, у нас есть выбор либо продолжить проверку следующего символа 
во входной строке, начав подбирать новое слово, исходящее из корня лексикона; либо 
игнорировать флаг узла "терминальный" и продолжить пытаться подставлять следующие дочерние символы.
Поскольку здесь имеется ветвление, то для хранения ветвления используется куча
Queue<(TrieNode IntermNode, int Pos)> queue, содержащяя позицию входной строки
и промежуточный узел лексикона, с которого нужно начать проверку.
Во внешнем цикле происходит переход между ветвлениями (исходящими из узлов лексикона 
с флагом "терминальный" и подузлами), во внутреннем цикле мы проходим входную строку 
от стартовой позиции ветви до конца строки, либо момента, когда окажется, что к 
данному символу строки мы уже прикладывали какой-то терминальный узел, значит, 
дальше входную строку мы уже проверяли в одной из предыдущих ветвей 
(тогда прекращаем обработку ветви). Для контроля этого используется массив bool[] milestones.
Если в какой-то момент окажется, что мы приложили терминальный узел к последнему символу 
входной строки, значит, замостить её словами из лексикона возможно, иначе - нет.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Использование milestones возможно, поскольку, если мы уже прикладывали к данному символу
входной строки терминальный узел, значит, сделав это повторно, мы просто нашли мощение до данного
символа другой последовательностью слов из лексикона. Значит, до этой позиции мощение эквивалентно
мощению из одной из предыдущих ветвей и дальше можно ветвь не обрабатывать (нам нужно найти любое
мощение строки).
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
В худшем случае, если входная строка text имеет длину text.Length = N, а лексикон состоит из слов вида
text2 = text, text2[text.Length - 1] = other_symbol
text2.Substring(0,1)
text2.Substring(0,2)
...
text2.Substring(0,N)
И в итоге замостить не удалось, мы получим терминальный узел с дочерними узлами на каждом индексе
входной строки, т.е. ветвление произойдёт на каждом символе, т.е. N раз.
Временная сложность составит O(N ^ 2). На каждом шаге поиск следующего узла в лексиконе по символу
составит O(1).
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Хранение слов в лексиконе (префиксном дереве) составляет O(sum_of_words_length).
Хранение точек ветвлений в очереди составляет в худшем случае O(N).
Хранение массива milestones занимает O(N).
Итого пространственная сложность O(sum_of_words_length + N).
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