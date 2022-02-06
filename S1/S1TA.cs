//https://contest.yandex.ru/contest/22449/problems/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class S1TA
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var nlist = ReadList();
        //a, x, b, c
        var a = nlist[0];
        var x = nlist[1];
        var b = nlist[2];
        var c = nlist[3];

        var y = a * Math.Pow(x, 2) + b * x + c;
        writer.WriteLine(y);


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