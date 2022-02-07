using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class S3TJ
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();
        int[] array = ReadInts();
        array = BubbleSort<int>.Sort(array, writer);

        SugarProperty = 7;
        int testSugar = SugarProperty;

        reader.Close();
        writer.Close();
    }

    public static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    public static int[] ReadInts()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }

    private static int sugarProp;

    public static int SugarProperty
    {
        get => sugarProp;
        set => sugarProp = value;
    }

}

public class BubbleSort<T> where T : IComparable
{
    public static T[] Sort(T[] array, TextWriter writer)
    {
        int arrayLength = array.Length;
        for (int i = 0; i < arrayLength - 1; i++)
        {
            int swapsNr = 0;
            for (int j = 0; j < arrayLength - 1 - i; j++)
            {
                if (array[j].CompareTo(array[j + 1]) > 0)
                {
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                    swapsNr++;
                }
            }
            if (i == 0 || swapsNr > 0)
                writer.WriteLine(string.Join(" ", array));
        }
        return array;
    }
}
