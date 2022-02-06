using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class S1TB
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());
        List<int> numbers = ReadList();

        int rem1 = Math.Abs(numbers[0] % 2);
        int rem2 = Math.Abs(numbers[1] % 2);
        int rem3 = Math.Abs(numbers[2] % 2);
        bool success = rem1 == rem2 && rem2 == rem3;
        string result = success ? "WIN" : "FAIL";
        writer.WriteLine(result);

        reader.Close();
        writer.Close();
    }

    private static List<int> ReadList()
    {
        return reader.ReadLine()
        .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToList();
    }
}