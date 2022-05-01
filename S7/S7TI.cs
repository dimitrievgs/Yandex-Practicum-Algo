using System;
using System.Linq;
using System.IO;
using System.Text;

namespace S7TI
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int[] dimensions = ReadInts();
            int rowNr = dimensions[0]; //строки
            int colNr = dimensions[1]; //столбцы
            byte[,] field = ReadField(rowNr, colNr);

            int[,] flowersToPickMatrix = CalculateFlowersToPickMatrix(rowNr, colNr, field);
            int maxFlowersToPick = flowersToPickMatrix[rowNr - 1, colNr - 1];
            string path = GetPath(rowNr, colNr, flowersToPickMatrix);

            writer.WriteLine(maxFlowersToPick);
            writer.WriteLine(path);

            writer.Close();
            reader.Close();
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        private static byte[] ReadRow()
        {
            char[] cDigits = reader.ReadLine().ToCharArray();
            byte[] digits = new byte[cDigits.Length];
            for (int i = 0; i < cDigits.Length; i++)
                digits[i] = byte.Parse(cDigits[i].ToString());
            return digits;
        }

        private static byte[,] ReadField(int rowNr, int colNr)
        {
            byte[,] field = new byte[rowNr, colNr];
            for (int y = rowNr - 1; y >= 0; y--)
            {
                byte[] rowDigits = ReadRow();
                for (int x = 0; x < colNr; x++)
                    field[y, x] = rowDigits[x];
            }
            return field;
        }

        private static int[,] CalculateFlowersToPickMatrix(int rowNr, int colNr, byte[,] field)
        {
            int[,] flowersToPickMatrix = new int[rowNr, colNr];
            for (int x = 0; x < colNr; x++)
                for (int y = 0; y < rowNr; y++)
                {
                    int toBottom = (y > 0) ? flowersToPickMatrix[y - 1, x] : 0;
                    int toLeft = (x > 0) ? flowersToPickMatrix[y, x - 1] : 0;
                    flowersToPickMatrix[y, x] = Math.Max(toBottom, toLeft) + field[y, x];
                }
            return flowersToPickMatrix;
        }

        private static string GetPath(int rowNr, int colNr, int[,] flowersToPickMatrix)
        {
            char[] path = new char[rowNr + colNr - 2];
            int y = rowNr - 1;
            int x = colNr - 1;
            // (colNr - 1) шагов по горизонтали, (rowNr - 1) шагов по вертикали, и ещё -1 т.к. индексация с нуля
            int stepIndex = rowNr + colNr - 3; 
            while (x > 0 || y > 0)
            {
                int toBottom = (y > 0) ? flowersToPickMatrix[y - 1, x] : -1;
                int toLeft = (x > 0) ? flowersToPickMatrix[y, x - 1] : -1;
                char shift;
                if (toLeft >= toBottom)
                {
                    shift = 'R';
                    x--;
                }
                else
                {
                    shift = 'U';
                    y--;
                }
                path[stepIndex] = shift;
                stepIndex--;
            }
            return new string(path);
        }
    }
}