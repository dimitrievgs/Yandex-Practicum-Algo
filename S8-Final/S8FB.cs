/*
ID 68336973
отчёт https://contest.yandex.ru/contest/26133/run-report/68336973/
задача https://contest.yandex.ru/contest/26133/problems/B/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи на вход подаётся строка, вероятно, состоящая из некоторого набора слов, 
не разделённых пробелами.
И набор слов, лексикон, из которых, как предполагается, состоит входная строка.
Для хранения лексикона используется префиксное дерево, реализованное в Trie и TrieNode
с дочерними узлами, хранящимися в hashmap/словаре как <значение, узел> (для доступа к подузлу за O(1)).
Алгоритм проверки возможности замостить входную строку словами из лексикона
реализован в PaveWithWords(...). 
Поскольку узел, будучи терминальным, может содержать дочерние узлы, то,
достигнув терминального узла, у нас есть выбор либо продолжить проверку следующего символа 
во входной строке, начав подбирать новое слово, исходящее из корня лексикона; либо 
игнорировать флаг узла "терминальный" и продолжить пытаться подставлять следующие дочерние символы.
Решение происходит через динамическое программирование. Вводится одномерный массив bool[] dp = new bool[N + 1],
где N = text.Length, а i соответствует префиксу text длины i. Для i = 0 префикс - пустая строка, 
потому dp[0] = true. Значение dp[i] = true означает, что смогли выстроить словами до предыдущего 
символа, т.е. text до (i - 1) индекса, т.к. в dp нумерация смещена на +1.
На каждом шаге цикла смотрится, смогли ли до предыдущего индекса воходной строки text проложить
до этого слова, если да, то дальше кладутся слова из лексикона (префиксного дерева), пока кладётся,
при этом, если встречаются терминальные узлы, то соотв. позиция в dp приравнивается true, до туда
можем проложить слова. За счёт этого и учитывается ветвление, поскольку от каждой позиции, куда
можно попасть мощением слов дальше пробрасываются все возможные слова и результат фиксируется в массиве.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
База динамики dp[0] = 0.
Для префикса длины 0 мостить словами ничего не надо, т.к. пустая строка, пишем true.
Переходи динамики, i > 0.
Если добрались словами до i-го индекса, то ветвление на i-й позиции можно учесть, проложив 
все возможные слова дальше из этой позиции и установив в соотв. позициях dp[i] = true. 
Можно сравнить с задачей на прыжки через ступеньки лестницы, только решая в обратном направлении 
(смотрим, не откуда прыгаем, а куда впринципе можем прыгнуть, красим ступеньки в другой цвет). 
0-я ступень достижима всегда. 
Из i-й ступеньки (индекса) мы можем прыгнуть дальше k способами, эти способы описаны в лексиконе.
Если последняя ступенька покрашена, значит, она достижима, значит, мы можем весь входной текст замостить словами.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
На каждом шаге поиск следующего узла в лексиконе по символу составит O(1). Поэтому заполнение
лексикона составит O(sum_of_words_length).
Заполнение динамического массива составит O(N x max_word_length), если слово максимальной длины
сможем прикладывать на каждом шаге и каждый узел Trie будет терминальным.
Итого временная сложность O(sum_of_words_length + N x max_word_length).
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Хранение слов в лексиконе (префиксном дереве) составляет O(sum_of_words_length).
Хранение массива dp занимает O(N).
Итого пространственная сложность O(sum_of_words_length + N).
-- ПРАВКИ --
Создал отдельный класс Trie, туда перенёс метод AddWord(...), внутри метода рекурсию заменил на итеративный алгоритм.
Внутри TrieNode поле со значением символа убрал, оставил его в качестве ключа хеш-таблицы.
Метод PaveWithWords(...) переписал через динамическое программирование.
Поправил обоснование алгоритма выше.
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