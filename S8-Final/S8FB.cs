/*
ID 
отчёт 
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
-- ПРАВКИ --
Создал отдельный класс Trie, туда перенёс метод AddWord(...), внутри метода рекурсию заменил на итеративный алгоритм.
Внутри TrieNode поле со значением символа убрал, оставил его в качестве ключа хеш-таблицы.

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
            Trie trie = new Trie();
            for (int i = 0; i < n; i++)
            {
                string word = reader.ReadLine();
                trie.AddWord(word);
            }

            bool result = PaveWithWords(ref text, trie, 0);
            writer.WriteLine(result ? "YES" : "NO");

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static bool PaveWithWords(ref string text, Trie trie, int pos)
        {
            bool[] dp = new bool[text.Length + 1];
            dp[0] = true;
            for (int i = 0; i < text.Length; i++)
            {
                if (dp[i] == true) // смогли ли выстроить словами до предыдущего символа, для 0-го автоматом true
                { 
                    int j = 0;
                    // от каждого индекса, куда до этого смогли проложить слова, кладём буквы, пока кладутся, если
                    // где-то встречаются терминальные узлы, то заносим в dp[i + 1 + j] = true, т.е. туда можно прокинуть ещё слово
                    var current = trie.root; 
                    while (current != null && (i + j) < text.Length)
                    {
                        current = current.GetChild(text[i + j]);
                        if (current != null && current.Terminal == true)
                            dp[i + 1 + j] = true;
                        j++;
                    }
                }
            }
            return dp[text.Length];
        }
    }

    class Trie
    {
        public TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public void AddWord(string s)
        {
            TrieNode current = root, next;
            for (int i = 0; i < s.Length; i++)
            {
                current.Childs.TryGetValue(s[i], out next);
                if (next == null)
                {
                    next = new TrieNode();
                    current.Childs.Add(s[i], next);
                }
                current = next;
            }
            current.Terminal = true;
        }
    }

    class TrieNode
    {
        public Dictionary<char, TrieNode> Childs { get; set; }
        public bool Terminal { get; set; }

        public TrieNode()
        {
            Childs = new Dictionary<char, TrieNode>();
        }

        public TrieNode GetChild(char value)
        {
            Childs.TryGetValue(value, out TrieNode node);
            return node;
        }
    }
}