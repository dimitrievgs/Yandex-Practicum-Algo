using System;
using System.Gemerics.Collections;
using System.Linq;
using System.IO;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpemnStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        reader.ReadInt();

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
}