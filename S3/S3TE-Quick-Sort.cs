using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S3TE
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            List<int> nK = ReadInts();
            int n = nK[0]; //number of houses
            int k = nK[1]; //total budget
            List<int> housesCosts = ReadInts(); //costs of houses
            //List<int> housesCosts = TestHousesCost(n, k);
            housesCosts = QuickSort(housesCosts);
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

        private static List<int> ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }

        private static List<int> TestHousesCost(int n, int houseCost)
        {
            List<int> list = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < n; i++)
                list.Add(rand.Next(1, houseCost * 3));
            return list;
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