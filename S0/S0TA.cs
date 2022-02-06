//https://contest.yandex.ru/contest/26365/problems/A/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class S0TA
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var n1 = ReadInt();
        var n2 = ReadInt();

        int n3 = n1 + n2;

        writer.Write(n3);

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