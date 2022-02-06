using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class S2TK
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var n = ReadInt();
        writer.WriteLine(RecursiveFibonacci(n));

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static int RecursiveFibonacci(int n)
    {
        if (n == 0 || n == 1)
            return 1;
        else
            return RecursiveFibonacci(n - 1) + RecursiveFibonacci(n - 2);
    }
}