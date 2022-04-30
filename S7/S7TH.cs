using System;
using System.Linq;
using System.IO;

namespace S7TH
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

            int maxFlowersToPick = CalculateMaxFlowersToPick(rowNr, colNr, field);
            writer.Write(maxFlowersToPick);

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
                {
                    field[y, x] = rowDigits[x];
                }
            }
            return field;
        }

        private static int CalculateMaxFlowersToPick(int rowNr, int colNr, byte[,] field)
        {
            int[,] flowersToPick = new int[rowNr, colNr];
            for (int x = 0; x < colNr; x++)
                for (int y = 0; y < rowNr; y++)
                {
                    int toBottom = (y > 0) ? flowersToPick[y - 1, x] : 0;
                    int toLeft = (x > 0) ? flowersToPick[y, x - 1] : 0;
                    flowersToPick[y, x] = Math.Max(toBottom, toLeft) + field[y, x];
                }
            return flowersToPick[rowNr - 1, colNr - 1];
        }
    }
}