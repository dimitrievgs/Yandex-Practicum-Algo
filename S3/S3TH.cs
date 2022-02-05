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
        string[] strNumbers = ReadStrNumbers();
        Array.Sort(strNumbers, new ItemComparer());
        foreach (string s in strNumbers)
            writer.Write(s);

        reader.Close();
        writer.Close();
    }

    private class ItemComparer : IComparer<string>
    {
        public int Compare(string strX, string strY)
        {
            int xPy = int.Parse(strX + strY);
            int yPx = int.Parse(strY + strX);
            return yPx.CompareTo(xPy); //descending
        }
    }


    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static string[] ReadStrNumbers()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();
    }
}