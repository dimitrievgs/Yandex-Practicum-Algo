/*
ID 
отчёт 
задача https://contest.yandex.ru/contest/25597/problems/A/

-- ПРИНЦИП РАБОТЫ --

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --

*/

using System;
using System.IO;

namespace S7FA
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
            //решение через двумерную динамику
            //значения по рядам i от 0 до N соответствуют рассмотрению части строки sOne длины i
            //значения по столбцам j от 0 до M соответствуют рассмотрению части строки sTwo длины j
            //например, в ячейке dp[i, j] будет мин-е кол-во операций (вставка, удаление, замена),
            //чтобы превратить sOne.Substring(0, i) в sTwo.Substring(0, j)
            int N = sOne.Length;
            int M = sTwo.Length;
            //в массив N x M вводим дополнительный ряд снизу (i = 0) и слева (j = 0)
            //значения там - базовый случай: одна из строк пустая, или обе пустые
            int[,] dp = new int[N + 1, M + 1];

            //заносим значения для базового случая
            dp[0, 0] = 0; //i == 0 && j == 0
            for (int i = 1; i <= N; i++) //j == 0 && i > 0
                dp[i, 0] = i;
            for (int j = 1; j <= M; j++) //i == 0 && j > 0
                dp[0, j] = j;

            //теперь переход динамики: считаем для i > 0, j > 0, т.е., когда обе подстроки не пусты
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= M; j++)
                {
                    int iStep = dp[i - 1, j] + 1; //удаление из sOne
                    int jStep = dp[i, j - 1] + 1; //вставка в sOne
                    int m = (sOne[i - 1] == sTwo[j - 1]) ? 0 : 1;
                    int ijStep = dp[i - 1, j - 1] + m; //замена символа или отсутствие изменений
                    dp[i, j] = Math.Min(Math.Min(iStep, jStep), ijStep); //сравнение с элементами <-, ^, <- и ^ по массиву dp
                }
            }
            return dp[N, M];
        }
    }
}