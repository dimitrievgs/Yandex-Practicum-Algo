//https://contest.yandex.ru/contest/22779/problems/A/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace S2TA
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int y = ReadInt();
            int x = ReadInt();
            int[,] matrix = ReadMatrix(y, x);
            int[,] tMatrix = Transpose(matrix, y, x);
            WriteMatrix(tMatrix, x, y);

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadArray()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        private static int[,] ReadMatrix(int y, int x)
        {
            int[,] matrix = new int[y, x];
            for (int j = 0; j < y; j++)
            {
                int[] array = ReadArray();
                for (int i = 0; i < x; i++)
                {
                    matrix[j, i] = array[i];
                }
            }
            return matrix;
        }

        private static int[,] Transpose(int[,] matrix, int y, int x)
        {
            int[,] tMatrix = new int[x, y];
            for (int j = 0; j < y; j++)
                for (int i = 0; i < x; i++)
                    tMatrix[i, j] = matrix[j, i];
            return tMatrix;
        }

        private static void WriteMatrix(int[,] matrix, int y, int x)
        {
            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                    writer.Write("{0} ", matrix[j, i]);
                writer.WriteLine();
            }
        }
    }
}