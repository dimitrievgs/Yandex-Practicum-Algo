using System;
using System.IO;
using System.Linq;

namespace S3TJ
{
    class Solution
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
}