using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S3TD
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = ReadInt();
            List<int> greedFactors = ReadInts();
            int m = ReadInt();
            List<int> cookieSizes = ReadInts();

            greedFactors = QuickSort(greedFactors);
            cookieSizes = QuickSort(cookieSizes);
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

        private static List<int> ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }

        private static Random rand = new Random();

        private static List<int> QuickSort(List<int> array)
        {
            if (array.Count < 2)
                return array; //recursion base
            else
            {
                int pivot = array[rand.Next(0, array.Count)];
                var result = Partition(array, pivot);
                List<int> rearranged = QuickSort(result.Left);
                rearranged.AddRange(result.Center);
                rearranged.AddRange(QuickSort(result.Right));
                return rearranged;
            }
        }

        private static (List<int> Left, List<int> Center, List<int> Right) Partition(List<int> array, int pivot)
        {
            List<int> left = new List<int>();
            List<int> center = new List<int>();
            List<int> right = new List<int>();
            int arrayNr = array.Count;
            for (int i = 0; i < arrayNr; i++)
            {
                if (array[i] < pivot)
                    left.Add(array[i]);
                else if (array[i] == pivot)
                    center.Add(array[i]);
                else if (array[i] > pivot)
                    right.Add(array[i]);
            }
            return (left, center, right);
        }
    }
}