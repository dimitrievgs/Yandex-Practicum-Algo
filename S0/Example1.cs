/*
 * Здесь мы привели решения задачи A+B
 */

using System;
using System.IO;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var a = ReadInt();
        var b = ReadInt();
        writer.WriteLine(a + b);

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }
}