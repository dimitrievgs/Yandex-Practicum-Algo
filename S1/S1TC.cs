using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace S1TC
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            var n = ReadInt(); //строки
            var m = ReadInt(); //столбцы
            var matrix = ReadMatrix(n, m);
            var elY = ReadInt();
            var elX = ReadInt();

            int nNumber = 0;
            var neighbours = GetNeighbours(n, m, matrix, elY, elX, out nNumber);
            Array.Sort(neighbours);
            string sOutput = "";
            for (int ni = 0; ni < nNumber; ni++)
            {
                if (ni > 0)
                    sOutput += " ";
                sOutput += neighbours[ni];
            }
            writer.Write(sOutput);

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static List<int> ReadList()
        {
            return reader.ReadLine()
            .Split(new char[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        }

        private static int[,] ReadMatrix(int n, int m)
        {
            int[,] matrix = new int[n, m];
            for (int j = 0; j < n; j++)
            {
                List<int> numbers = ReadList();
                for (int i = 0; i < m; i++)
                {
                    matrix[j, i] = numbers[i];
                }
            }
            return matrix;
        }

        private static int[] GetNeighbours(int n, int m, int[,] matrix, int elY, int elX, out int nNumber)
        {
            int[] neighbours = new int[4];
            nNumber = 0;
            int leftX = elX - 1;
            int rightX = elX + 1;
            int topY = elY - 1;
            int bottomY = elY + 1;
            if (leftX >= 0)
            {
                neighbours[nNumber] = matrix[elY, leftX];
                nNumber++;
            }
            if (rightX <= m - 1)
            {
                neighbours[nNumber] = matrix[elY, rightX];
                nNumber++;
            }
            if (topY >= 0)
            {
                neighbours[nNumber] = matrix[topY, elX];
                nNumber++;
            }
            if (bottomY <= n - 1)
            {
                neighbours[nNumber] = matrix[bottomY, elX];
                nNumber++;
            }
            neighbours = neighbours.Take(nNumber).ToArray();
            return neighbours;
        }
    }
}