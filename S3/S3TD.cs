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

        int n = ReadInt();
        int[] greedFactors = ReadInts();
        int m = ReadInt();
        int[] cookieSizes = ReadInts();

        Array.Sort(greedFactors);
        Array.Sort(cookieSizes);
        int nCursor = 0;
        int mCursor = 0;
        while (nCursor < n && mCursor < m)
        {
            if (cookieSizes[mCursor] >= greedFactors[nCursor])
            {
                nCursor++;
            }
            mCursor++;
        }
        writer.WriteLine(nCursor);

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
}