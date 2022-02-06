using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class S3TE
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int[] nK = ReadInts();
        int n = nK[0]; //number of houses
        int k = nK[1]; //total budget
        int[] housesCosts = ReadInts(); //costs of houses

        Array.Sort(housesCosts);
        int houseIndex = 0;
        int remainedBudget = k;
        while (houseIndex < n)
        {
            int houseCost = housesCosts[houseIndex];
            if (houseCost <= remainedBudget)
            {
                remainedBudget -= houseCost;
                houseIndex++;
            }
            else
                break;
        }
        writer.WriteLine(houseIndex);

        reader.Close();
        writer.Close();
    }

    private static int[] ReadInts()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}