/*
ID 68246223
отчёт https://contest.yandex.ru/contest/26133/run-report/68246223/
задача https://contest.yandex.ru/contest/26133/problems/A/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи на вход подаются строки в запакованном виде. Пакуется строка
через запись "число_повторений[строка]", последовательно записанные выражения
и символы означают конкатенацию.
Сначала последовательно строки распакуются через рекурсивный вызов ReadPackedString(...).
Затем происходит поиск максимального общего префикса через GetMaxPrefixLength(...) путём
последовательного сравнения для каждой позиции j внутри строк символов всех строк на 
этой позиции. Если в какой-то момент на j-ой позиции в какой-то строке символ отличен,
или одна из строк закончилась, поиск максимального общего префикса окончен.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Запакованный формат записи строк подразумевает сам по себе рекурсивную запись, поэтому содержимое 
внутри '[' и ']' можно подавать в новый вызов рекурсии. Выше, в пределах одного уровня простая конкатенация,
поэтому мы можем последовательно считывать символы и выражения "число_повторений[строка]" и объединять их.
Для склейки используется структура StringBuilder, которая позволяет быстро добавлять и удалять символы.
Поиск максимального общего префикса происходит последовательно для всех строк на каждой позиции.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Распаковка занимает O(words.Length x max_word.Length) из-за последовательного считывания и обработки символов.
Поиск максимального префикса занимает в худшем случае O(words.Length x min_word.Length),
если первые j символов (где j - длина самого короткого слова) всех слов состоят из одной и той же
последовательности символов.
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Пространственная сложность при распаковке занимает O(words.Length x max_word.Length) 
за счёт кратной (т.е. x константа) распаковки содержимого внутри скобок '[' и ']' и 
хранения распакованных строк в массиве, дополнительно рекурсия на каждой распаковке 
для максимальной степени вложенности также потребует дополнительно O(max_word.Length) памяти.
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
                string word = reader.ReadLine();
                words[i] = ReadPackedString(ref word, 0).Str.ToString();
            }

            string maxCommonPrefix = words[0].Substring(0, GetMaxPrefixLength(words, n));
            writer.WriteLine(maxCommonPrefix);

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static (StringBuilder Str, int Pos) ReadPackedString(ref string s, int pos)
        {
            StringBuilder sOut = new StringBuilder();

            int i = pos;
            int mult = 0;
            while (i < s.Length)
            {
                if (char.IsDigit(s[i])) //число повторений для '[' и ']'
                {
                    StringBuilder sNumber = new StringBuilder();
                    while (i < s.Length && char.IsDigit(s[i]))
                    {
                        sNumber.Append(s[i]);
                        i++;
                    }
                    mult = int.Parse(sNumber.ToString());
                }
                else if (mult > 0 && s[i] == '[') //шаг рекурсии, считываем содержимое внутри '[' и ']' рекурсивно
                {
                    StringBuilder subSB;
                    (subSB, i) = ReadPackedString(ref s, i + 1);
                    for (int j = 0; j < mult; j++)
                        sOut.Append(subSB);
                }
                else if (char.IsLetter(s[i])) //считываем символы для конкатенации
                {
                    while (i < s.Length && char.IsLetter(s[i]))
                    {
                        sOut.Append(s[i]);
                        i++;
                    }
                }
                else if (s[i] == ']') //окончание строки или достижение символа ']' означает конец данного уровня вложенности/шага рекурсии
                {
                    i++;
                    break;
                }
            }
            return (sOut, i);
        }

        private static int GetMaxPrefixLength(string[] words, int n)
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