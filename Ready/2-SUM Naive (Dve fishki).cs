using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var n = ReadInt();
        var numbers = ReadList();
        var k = ReadInt();

        int i1 = 0, i2 = 0;
        bool success = false;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (numbers[i] + numbers[j] == k)
                {
                    i1 = i;
                    i2 = j;
                    success = true;
                    break;
                }
            }
            if (success)
                break;
        }
        if (success)
            writer.Write(numbers[i1] + " " + numbers[i2]);
        else
            writer.Write("None");

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
            .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}