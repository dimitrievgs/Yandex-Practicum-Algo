﻿/*
ID 
отчёт 
задача https://contest.yandex.ru/contest/25597/problems/A/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи нужно посчитать расстояние Левенштейна для двух входных строк.
Решаем через двумерную динамику. Вводоим двумерный массив N x M, int [,], где N = sOne.Length,
M = sTwo.Length.
Значения по рядам i от 0 до N соответствуют рассмотрению подстроки sOne длины i.
Значения по столбцам j от 0 до M соответствуют рассмотрению подстроки sTwo длины j.
Например, в ячейке dp[i, j] будет мин-е кол-во операций (вставка, удаление, замена),
чтобы превратить sOne.Substring(0, i) в sTwo.Substring(0, j).
Базовый случай:
Если одна из подстрок пустая, расстояние по Левенштейну равно длине другой строки (последовательные вставки).
Поэтому, если обе подстроки пустые, то никаких операций не требуется, расст-е по Левенштейну = 0.
Переход динамики:
Проходим по ячейкам i = 1..N, j = 1..M.
Значение в ячейке [i, j] считается на основе значений в ячейках:
- левее: dp[i - 1, j] + 1, удаление из sOne.
- выше: dp[i, j - 1] + 1, вставка в sOne.
- левее и выше по диагонали: dp[i - 1, j - 1] + charChangeOrNot, где charChangeOrNot == 1, 
если замена последнего символа.
Поскольку нужно миним-е кол-во операций, берётся минимальное из 3ёх значений.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Задачу можно решить через двумерную динамику, поскольку она естественным образом
разбивается на подзадачи, и на основе кол-ва операций для превращения меньших подстрок
одну в другую мы можем посчитать кол-во операций для превращения подстрок, размер которых
увеличился на 1.
База динамики:
Очевидно, что расстояние по Левенштейну между двумя пустыми подстроками равно 0.
Также, для превращения пустой строки в строку длины j, нужно совершить последовательно 
j операций вставки. Для превращения строки длины i в пустую строку, нужно совершить
последовательно i операций удаления.
Переход динамики: обе подстроки не пусты.
Сначала покажем, что в оптимальной последовательности операций превращения одной строки в другую
операции можно произвольно менять местами. Это справедливо, т.к. для двух произвольных
последовательных операций:
- две замены одного символа - не оптимально, т.к. можно обойтись одной заменой
- вставка символа и последующее стирание - не оптимально, их можно не делать
- вставка символа и последующая замена - не оптимально, можно обойтись заменой
- замена символа с последующим стиранием - не оптимально, замена лишняя
- 2 замены разных символов можно менять местами
- 2 стирания или 2 вставки можно менять местами
- стирание и вставку разных символов можно менять местами
- вставку и замену разных символов можно менять местами
- стирание и замену разных символов можно менять местами
Тогда можно показать, что:
- Последний символ строки sOne был удалён. Можем вынести эту операцию последней.
Тогда мы можем сначала превратить sOne.Substring(0, i - 1) в sTwo (стоимость dp[i - 1, j]), 
затем совершить удаление символа (стоимость 1). Итого стоимость стирания последнего символа
в sOne = dp[i - 1, j] + 1.
- Последний символ sTwo был добавлен в sOne. Можем вынести операцию последней.
Сначала превратим sOne в sTwo.Substring(0, j - 1), стоимость dp[i, j - 1]. Затем добавим символ,
стоимость операции 1. Итого стоимость добавления последнего символа в sOne = dp[i, j - 1] + 1.
- Пусть последний символ в sOne был в какой-то момент изменён, но не через вставку. 
Тогда остаётся замена последнего символа. Тогда операции можно свести к превращению
sOne.Substring(0, i - 1) в sTwo.Substring(0, j - 1), стоимость этой операции dp[i - 1, j - 1],
и последующая замена последнего символа, если sOne[i] != sOne[j]. Тогда общая стоимость 
составит dp[i - 1, j - 1] + Convert.ToInt32(sOne[i] != sOne[j]).
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Временная сложность O(N x M) соотвествует последовательному вычислению каждой ячейки
матрицы N x M (каждая ячейка при i > 0, j > 0 сравнивается с тремя предыдущими).
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Пространственная сложность определяется хранением двумерного массива, поэтому
составляет O(N x M).
*/

using System;
using System.IO;

namespace S7FA_nm
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            string sOne = reader.ReadLine();
            string sTwo = reader.ReadLine();

            int levensteinDistance = GetLevenshteinDistance(sOne, sTwo);
            writer.Write(levensteinDistance);

            writer.Close();
            reader.Close();
        }

        public static int GetLevenshteinDistance(string sOne, string sTwo)
        {
            // Решение через двумерную динамику
            // значения по рядам i от 0 до N соответствуют рассмотрению части строки sOne длины i
            // значения по столбцам j от 0 до M соответствуют рассмотрению части строки sTwo длины j
            // например, в ячейке dp[i, j] будет мин-е кол-во операций (вставка, удаление, замена),
            // чтобы превратить sOne.Substring(0, i) в sTwo.Substring(0, j)
            int n = sOne.Length;
            int m = sTwo.Length;
            // в массив N x M вводим дополнительный ряд снизу (i = 0) и слева (j = 0)
            // значения там - базовый случай: одна из строк пустая, или обе пустые
            int[,] dp = new int[n + 1, m + 1];

            // заносим значения для базового случая
            dp[0, 0] = 0; //i == 0 && j == 0
            for (int i = 1; i <= n; i++) //j == 0 && i > 0
                dp[i, 0] = i;
            for (int j = 1; j <= m; j++) //i == 0 && j > 0
                dp[0, j] = j;

            // теперь переход динамики: считаем для i > 0, j > 0, т.е., когда обе подстроки не пусты
            for (int i = 1; i <= n; i++)
            {
                Console.Write(dp[i, 0] + " ");
                for (int j = 1; j <= m; j++)
                {
                    int iStep = dp[i - 1, j] + 1; // удаление из sOne
                    int jStep = dp[i, j - 1] + 1; // вставка в sOne
                    // в dp добавлены дополнительные ряды для учёта случая пустых подстрок,
                    // поэтому последние символы в подстроках здесь будут sOne[i - 1], sTwo[j - 1] 
                    int charChangeOrNot = Convert.ToInt32(sOne[i - 1] != sTwo[j - 1]);
                    int ijStep = dp[i - 1, j - 1] + charChangeOrNot; // замена символа или отсутствие изменений
                    dp[i, j] = Math.Min(Math.Min(iStep, jStep), ijStep); // сравнение с элементами <-, ^, <- и ^ по массиву dp
                    Console.Write(dp[i, j] + " ");
                }
                Console.WriteLine();
            }
            return dp[n, m];
        }
    }
}