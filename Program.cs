using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var array = ReadArray();
        var n = array[0];
        var k = array[1];
        var d = (int)Math.Pow(10, k);
        var fib = Fibonacci(n, d);
        writer.WriteLine(fib);

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static int[] ReadArray()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }

    private static int Fibonacci(int n, int d)
    {
        int a = 1;
        int b = 1;
        for (int i = 0; i < n; i++)
        {
            int a2 = b;
            int b2 = a + b;
            a = a2 % d;
            b = b2 % d;
        }
        return a;
    }
}