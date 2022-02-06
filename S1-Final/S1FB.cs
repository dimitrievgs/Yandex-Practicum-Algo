using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class S1FB
{
    private static TextReader reader;
    private static TextWriter writer;
    private const int matrixSize = 4;
    private const int parsedDot = 0;
    private const int minSymbol = 1, maxSymbol = 9;
    private const int numbersCount = maxSymbol - minSymbol + 1; //1..9
    private const int playersCount = 2;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int k = ReadInt();
        int[,] matrix = ReadMatrix();

        int[] symbolsCount = new int[numbersCount];
        for (int j = 0; j < matrixSize; j++)
        {
            for (int i = 0; i < matrixSize; i++)
            {
                int symbol = matrix[j, i];
                if (symbol > parsedDot)
                    symbolsCount[symbol - minSymbol]++;
            }
        }

        int pointsNr = 0;
        int maxPressedButtons = playersCount * k;
        for (int f = 0; f < numbersCount; f++)
        {
            int symbolCount = symbolsCount[f];
            if (symbolCount > 0 && symbolCount <= maxPressedButtons)
                pointsNr++;
        }

        writer.WriteLine(pointsNr);

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static int[] ReadArray()
    {
        char[] cNumbers = reader.ReadLine().ToCharArray();
        int numbersCount = cNumbers.Length;
        int[] numbers = new int[numbersCount];
        for (int i = 0; i < numbersCount; i++)
            int.TryParse(cNumbers[i].ToString(), out numbers[i]);
        return numbers;
    }

    private static int[,] ReadMatrix()
    {
        int[,] matrix = new int[matrixSize, matrixSize];
        for (int j = 0; j < matrixSize; j++)
        {
            int[] numbers = ReadArray();
            for (int i = 0; i < matrixSize; i++)
                matrix[j, i] = numbers[i];
        }
        return matrix;
    }
}