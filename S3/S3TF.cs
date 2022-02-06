using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class S3TF
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();
        int[] segmentsLengths = ReadInts();

        segmentsLengths = segmentsLengths.OrderByDescending(o => o).ToArray();
        writer.WriteLine(FindMaxPerimeter(segmentsLengths, n));

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static int[] ReadInts()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }

    private static int FindMaxPerimeter(int[] segmentsLengths, int n)
    {
        int maxPerimeter = int.MinValue;
        for (int i = 0; i < n - 2; i++)
        {
            int c = segmentsLengths[i];
            for (int j = i + 1; j < n - 1; j++)
            {
                int a = segmentsLengths[j];
                for (int k = j + 1; k < n; k++)
                {
                    int b = segmentsLengths[k];
                    if (c < a + b)
                    {
                        maxPerimeter = a + b + c;
                        return maxPerimeter;
                    }
                }
            }
        }
        return 0;
    }
}