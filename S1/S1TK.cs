using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int xL = ReadInt();
        int[] X = ReadArray();
        int K = ReadInt();
        int xNumber = ArrayToInt(X, xL);
        int sum = xNumber + K;
        writer.WriteLine(string.Join(" ", IntToList(sum)));

        reader.Close();
        writer.Close();
    }

    private static int ArrayToInt(int[] array, int arrayLength)
    {
        int sum = 0;
        for (int i = 0; i < arrayLength; i++)
        {
            sum += (int)Math.Pow(10, i) * array[arrayLength - i - 1];
        }
        return sum;
    }

    private static List<int> IntToList(int number)
    {
        List<int> digits = new List<int>();
        int pool = number;
        int rem, digit;
        int i = 1;
        while (pool > 0)
        {
            rem = pool % (int)Math.Pow(10, i);
            digit = rem / (int)Math.Pow(10, i - 1);
            digits.Insert(0, digit);
            pool = pool - rem;
            i++;
        }
        return digits;
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
